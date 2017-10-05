CREATE PROCEDURE [dbo].[ProductInsert]
	@Codigo nvarchar(40), 
	@Descricao nvarchar(256), 
	@Partnumber nvarchar(25), 
	@CategoriaCodigo nvarchar(20), 
	@MarcaCodigo nvarchar(5), 
	@ImpostoCodigo nvarchar(5), 
	@FornecedorCodigo nvarchar(5), 
	@PrecoCusto decimal(14,4), 
	@PrecoCusto_Data smalldatetime, 
	@PrecoCusto_U1 decimal(14,4), 
	@PrecoCusto_U1Data smalldatetime, 
	@PrecoCusto_U2 decimal(14,4), 
	@PrecoCusto_U2Data smalldatetime, 
	@PrecoCusto_U3 decimal(14,4), 
	@PrecoCusto_U3Data smalldatetime, 
	@StockCodigo nvarchar(5), 
	@StockCodigo_Data smalldatetime, 
	@StockCodigo_U1 nvarchar(5), 
	@StockCodigo_U1Data smalldatetime, 
	@StockCodigo_U2 nvarchar(5), 
	@StockCodigo_U2Data smalldatetime, 
	@StockCodigo_U3 nvarchar(5), 
	@StockCodigo_U3Data smalldatetime, 
	@Descontinuado bit, 
	@Inativo bit

AS
	
	DECLARE @Criacao smalldatetime, @Versao datetime, @NumeroLinhasAfetadas int, @Erro nvarchar(255)

	SET @Criacao = GETDATE()
	SET @Versao = GETDATE()


	-- INSERÇÃO DO REGISTO
	BEGIN TRY
		 
		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF

		-- INSERIR REGISTO
		INSERT INTO [dbo].[Produtos] ([Codigo], [Descricao], [Partnumber], 
										[CategoriaCodigo], [MarcaCodigo], [ImpostoCodigo], 
										[FornecedorCodigo], [PrecoCusto], [PrecoCusto_Data], 
										[PrecoCusto_U1], [PrecoCusto_U1Data], [PrecoCusto_U2], 
										[PrecoCusto_U2Data], [PrecoCusto_U3], [PrecoCusto_U3Data], 
										[StockCodigo], [StockCodigo_Data], [StockCodigo_U1], 
										[StockCodigo_U1Data], [StockCodigo_U2], [StockCodigo_U2Data], 
										[StockCodigo_U3], [StockCodigo_U3Data], [Descontinuado], 
										[Inativo], [Criacao], [Versao]
									) 
					VALUES (@Codigo, @Descricao, @Partnumber, 
								@CategoriaCodigo, @MarcaCodigo, @ImpostoCodigo, 
								@FornecedorCodigo, @PrecoCusto, @PrecoCusto_Data, 
								@PrecoCusto_U1, @PrecoCusto_U1Data, @PrecoCusto_U2, 
								@PrecoCusto_U2Data, @PrecoCusto_U3, @PrecoCusto_U3Data, 
								@StockCodigo, @StockCodigo_Data, @StockCodigo_U1, 
								@StockCodigo_U1Data, @StockCodigo_U2, @StockCodigo_U2Data, 
								@StockCodigo_U3, @StockCodigo_U3Data, @Descontinuado, 
								@Inativo, @Criacao, @Versao
							);
		
		SET @NumeroLinhasAfetadas = @@ROWCOUNT -- GUARDAR O NUMERO DE LINHAS AFETADAS

		-- ACABA CONTAGEM DE @@ROWCOUNT
		SET NOCOUNT ON

		-- RETORNAR O NUMERO DE LINHAS AFETADAS
		-- Quando usado o ExecuteNonQuery é o @@ROWCOUNT que é lido (SET NOCOUNT OFF/ON),  e não esta variável
		RETURN @NumeroLinhasAfetadas

	END TRY


	-- SE EXISTIRAM ERROS, DESFAZER TRANSACÇÃO
	BEGIN CATCH

		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-I10] ' + ERROR_MESSAGE();
		RAISERROR (@Erro, 15, 1)
		RETURN 0

	END CATCH
