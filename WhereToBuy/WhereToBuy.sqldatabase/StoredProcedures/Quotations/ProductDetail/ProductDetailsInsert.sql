CREATE PROCEDURE [dbo].[ProductDetailsInsert]
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
    @IndicePreocupacaoConteudo tinyint
AS
	
	DECLARE @Criacao smalldatetime, @Versao datetime, @NumeroLinhasAfetadas int, @Erro nvarchar(255)

	SET @Criacao = GETDATE()
	SET @Versao = GETDATE()


	-- INSERÇÃO DO REGISTO
	BEGIN TRY
		 
		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF

		-- INSERIR REGISTO
		INSERT INTO [dbo].[ProdutosDetalhe]
           ([ProdutoCodigo]
           ,[FornecedorCodigo]
           ,[Descricao]
           ,[DescricaoPontuacao]
           ,[DescricaoInativa]
           ,[Caracteristicas]
           ,[CaracteristicasPontuacao]
           ,[CaracteristicasInativas]
           ,[Link]
           ,[LinkPontuacao]
           ,[LinkInativo]
           ,[Imagem]
           ,[ImagemPontuacao]
           ,[ImagemInativa]
           ,[AtualizacaoAutomaticaInativa]
           ,[AtualizacaoManualNecessaria]
           ,[IndicePreocupacaoConteudo]
           ,[Criacao]
           ,[Versao])
		VALUES
           (@ProdutoCodigo,
			@FornecedorCodigo,
			@Descricao,
			@DescricaoPontuacao,
			@DescricaoInativa,
			@Caracteristicas,
			@CaracteristicasPontuacao,
			@CaracteristicasInativas,
			@Link,
			@LinkPontuacao,
			@LinkInativo,
			@Imagem,
			@ImagemPontuacao,
			@ImagemInativa,
			@AtualizacaoAutomaticaInativa,
			@AtualizacaoManualNecessaria,
			@IndicePreocupacaoConteudo,
            @Criacao,
            @Versao);
		
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
