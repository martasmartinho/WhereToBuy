CREATE PROCEDURE [dbo].[QuotationRuleInsert]
	@FornecedorCodigo nvarchar(5),
	@MarcaCodigo nvarchar(5),
	@CategoriaCodigo nvarchar(20),
	@StockCodigo nvarchar(5),
	@HorasValidade smallint,
	@StockCodigoSubstituto nvarchar(5),
	@DataReset smalldatetime,
	@Notas nvarchar(256)
AS
	
	DECLARE @Criacao smalldatetime, @Versao datetime, @NumeroLinhasAfetadas int, @Erro nvarchar(255)

	SET @Criacao = GETDATE()
	SET @Versao = GETDATE()


	-- INSERÇÃO DO REGISTO
	BEGIN TRY
		 
		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF

		-- INSERIR REGISTO
		INSERT INTO [dbo].[CotacoesRegras] ([FornecedorCodigo], [MarcaCodigo], [CategoriaCodigo], [StockCodigo], 
						[HorasValidade], [StockCodigoSubstituto], [DataReset], [Notas], [Versao]) 
					VALUES (@FornecedorCodigo, @MarcaCodigo, @CategoriaCodigo, @StockCodigo, 
						@HorasValidade, @StockCodigoSubstituto, @DataReset, @Notas, @Versao);
		
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
