CREATE PROCEDURE [dbo].[QuotationRuleUpdate]
	@FornecedorCodigo nvarchar(5),
	@MarcaCodigo nvarchar(5),
	@CategoriaCodigo nvarchar(20),
	@StockCodigo nvarchar(5),
	@HorasValidade smallint,
	@StockCodigoSubstituto nvarchar(5),
	@DataReset smalldatetime,
	@Notas nvarchar(256),
	@Versao datetime
AS

	DECLARE @Erro nvarchar(255)


	-- VALIDAR SE O REGISTO EXISTE
	IF NOT EXISTS(SELECT [FornecedorCodigo] FROM [dbo].[CotacoesRegras] WHERE [FornecedorCodigo] = @FornecedorCodigo 
																AND [MarcaCodigo] = @MarcaCodigo
																AND [CategoriaCodigo] = @CategoriaCodigo
																AND [StockCodigo] = @StockCodigo)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U01] ' + 'O registo que pretende alterar não existe!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	-- VALIDAR SE A VERSÃO AINDA É A MESMA
	IF NOT EXISTS(SELECT [FornecedorCodigo] FROM [dbo].[CotacoesRegras] WHERE [FornecedorCodigo] = @FornecedorCodigo 
																AND [MarcaCodigo] = @MarcaCodigo
																AND [CategoriaCodigo] = @CategoriaCodigo
																AND [StockCodigo] = @StockCodigo 
																AND [Versao] = @Versao)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U02] ' + 'O registo que pretende alterar, já foi alterado desde que o abriu!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END


	-- TUDO OK PARA SEGUIR EM FRENTE
	DECLARE @NovaVersao datetime, @NumeroLinhasAfetadas int

	SET @NovaVersao = GETDATE()


	-- ALTERAÇÃO DO REGISTO
	BEGIN TRY

		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF

		-- ALTERAR REGISTO
		UPDATE [dbo].[CotacoesRegras] SET [HorasValidade] = @HorasValidade,
									[StockCodigoSubstituto] = @StockCodigoSubstituto,
									[DataReset] = @DataReset,
									[Notas] = @Notas,
									[Versao] = @NovaVersao
								WHERE [FornecedorCodigo] = @FornecedorCodigo 
									AND [MarcaCodigo] = @MarcaCodigo
									AND [CategoriaCodigo] = @CategoriaCodigo
									AND [StockCodigo] = @StockCodigo

		SET @NumeroLinhasAfetadas = @@ROWCOUNT -- GUARDAR O NUMERO DE LINHAS AFETADAS

		-- ACABA CONTAGEM DE @@ROWCOUNT
		SET NOCOUNT ON

		-- RETORNAR O NUMERO DE LINHAS AFETADAS
		-- Quando usado o ExecuteNonQuery é o @@ROWCOUNT que é lido (SET NOCOUNT OFF/ON), e não esta variável
		RETURN @NumeroLinhasAfetadas

	END TRY


	-- SE EXISTIRAM ERROS
	BEGIN CATCH

		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U10] ' + ERROR_MESSAGE();
		RAISERROR (@Erro, 15, 1)
		RETURN 0

	END CATCH