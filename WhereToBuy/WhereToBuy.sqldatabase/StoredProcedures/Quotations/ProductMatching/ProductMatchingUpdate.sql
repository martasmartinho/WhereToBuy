CREATE PROCEDURE [dbo].[ProductMatchingUpdate]
	@FornecedorCodigo nvarchar(5),
	@ComplementoCodigo nvarchar(128),
	@Codigo nvarchar(256),
	@HorasValidadeCotacao smallint,
	@StockCodigoSubstituto nvarchar(5),
	@DispensaPrevencaoPrecosDesfasados bit, 
	@DispensaPrevencaoFalsoStock bit,
	@DataReset smalldatetime,
	@Notas nvarchar(256),
	@MapTo nvarchar(40),
	@Inativo bit,
	@Versao datetime
AS

	DECLARE @Erro nvarchar(255)


	-- VALIDAR SE O REGISTO EXISTE
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[ProdutosMatching] WHERE [FornecedorCodigo] = @FornecedorCodigo AND [Codigo] = @Codigo AND [ComplementoCodigo] = @ComplementoCodigo)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U01] ' + 'O registo que pretende alterar não existe!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	-- VALIDAR SE A VERSÃO AINDA É A MESMA
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[ProdutosMatching] WHERE [FornecedorCodigo] = @FornecedorCodigo AND [ComplementoCodigo] = @ComplementoCodigo AND [Codigo] = @Codigo AND [Versao] = @Versao)
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
		UPDATE [dbo].[ProdutosMatching] SET [MapTo] = @MapTo,
									[HorasValidadeCotacao] = @HorasValidadeCotacao,
									[StockCodigoSubstituto] = @StockCodigoSubstituto,
									[DispensaPrevencaoPrecosDesfasados] = @DispensaPrevencaoPrecosDesfasados, 
									[DispensaPrevencaoFalsoStock] = @DispensaPrevencaoFalsoStock,
									[DataReset] = @DataReset,
									[Notas] = @Notas,
									[Inativo] = @Inativo,
									[Versao] = @NovaVersao
								WHERE [FornecedorCodigo] = @FornecedorCodigo AND [Codigo] = @Codigo AND [ComplementoCodigo] = @ComplementoCodigo

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