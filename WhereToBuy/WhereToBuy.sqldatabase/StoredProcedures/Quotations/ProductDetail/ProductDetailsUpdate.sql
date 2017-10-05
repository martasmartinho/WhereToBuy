CREATE PROCEDURE [dbo].[ProductDetailsUpdate]
	@ProdutoCodigo nvarchar(40),
    @FornecedorCodigo nvarchar(5),
    @Descricao nvarchar(256),
    @DescricaoPontuacao smallint,
    @DescricaoInativa bit,
    @Caracteristicas nvarchar(2048),
	@CaracteristicasPontuacao smallint,
    @CaracteristicasInativas bit,
    @Link nvarchar(1024),
    @LinkPontuacao smallint,
    @LinkInativo bit,
    @Imagem nvarchar(1024),
    @ImagemPontuacao smallint,
    @ImagemInativa bit,
    @AtualizacaoAutomaticaInativa bit,
    @AtualizacaoManualNecessaria bit,
    @IndicePreocupacaoConteudo tinyint,
	@Versao datetime
AS

	DECLARE @Erro nvarchar(255)


	-- VALIDAR SE O REGISTO EXISTE
	IF NOT EXISTS(SELECT [ProdutoCodigo] FROM [dbo].[ProdutosDetalhe] WHERE [ProdutoCodigo] = @ProdutoCodigo AND [FornecedorCodigo] = @FornecedorCodigo)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U01] ' + 'O registo que pretende alterar não existe!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	-- VALIDAR SE A VERSÃO AINDA É A MESMA
	IF NOT EXISTS(SELECT [ProdutoCodigo] FROM [dbo].[ProdutosDetalhe] WHERE [ProdutoCodigo] = @ProdutoCodigo AND [FornecedorCodigo] = @FornecedorCodigo AND [Versao] = @Versao)
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
		UPDATE [dbo].[ProdutosDetalhe] SET [Descricao] = @Descricao
			  ,[DescricaoPontuacao] = @DescricaoPontuacao 
			  ,[DescricaoInativa] = @DescricaoInativa 
			  ,[Caracteristicas] = @Caracteristicas
			  ,[CaracteristicasPontuacao] = @CaracteristicasPontuacao
			  ,[CaracteristicasInativas] = @CaracteristicasInativas 
			  ,[Link] = @Link 
			  ,[LinkPontuacao] = @LinkPontuacao
			  ,[LinkInativo] = @LinkInativo 
			  ,[Imagem] = @Imagem
			  ,[ImagemPontuacao] = @ImagemPontuacao
			  ,[ImagemInativa] = @ImagemInativa
			  ,[AtualizacaoAutomaticaInativa] = @AtualizacaoAutomaticaInativa
			  ,[AtualizacaoManualNecessaria] = @AtualizacaoManualNecessaria
			  ,[IndicePreocupacaoConteudo] = @IndicePreocupacaoConteudo
			  ,[Versao] = @NovaVersao
		   
								WHERE [ProdutoCodigo] = @ProdutoCodigo AND [FornecedorCodigo] = @FornecedorCodigo

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