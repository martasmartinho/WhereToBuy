CREATE PROCEDURE [dbo].[StockInsert]
	@Codigo nvarchar(5),
	@Descricao nvarchar(50),
	@DisponibilidadeNivel smallint,
	@ValidadeP50_StockCodigo nvarchar(5),
	@ValidadeP60_StockCodigo nvarchar(5),
	@ValidadeP70_StockCodigo nvarchar(5),
	@ValidadeP80_StockCodigo nvarchar(5),
	@ValidadeP90_StockCodigo nvarchar(5),
	@Notas nvarchar(256),
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
		INSERT INTO [dbo].[Stocks] ([Codigo], [Descricao], [DisponibilidadeNivel], [ValidadeP50_StockCodigo],
						[ValidadeP60_StockCodigo], [ValidadeP70_StockCodigo], [ValidadeP80_StockCodigo], [ValidadeP90_StockCodigo],
						[Notas], [Inativo], [Criacao], [Versao]) 
					VALUES ( @Codigo, @Descricao, @DisponibilidadeNivel, @ValidadeP50_StockCodigo, 
						@ValidadeP60_StockCodigo, @ValidadeP70_StockCodigo, @ValidadeP80_StockCodigo, @ValidadeP90_StockCodigo, 
						@Notas, @Inativo, @Criacao, @Versao);
		
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
