CREATE PROCEDURE [dbo].[SupplierUpdate]
	@Codigo nvarchar(5), 
	@Nome nvarchar(50), 
	@Morada nvarchar(200), 
	@CodigoPostal nvarchar(15),
	@LocalidadePostal nvarchar(50), 
	@Contribuinte nvarchar(20), 
	@Vendedor nvarchar(50), 
	@Telefone nvarchar(20), 
	@Telemovel nvarchar(15), 
	@SMS nvarchar(15), 
	@Email nvarchar(100), 
	@AcessoOnlineAtivo bit, 
	@Username nvarchar(20), 
	@Password nvarchar(20), 
	@HorasValidadeSugestao smallint, 
	@ProdutosMatchingAutomatico bit, 
	@ProdutosCriacaoAutomatica bit, 
	@DisponibilizaInfoProdutoDetalhe bit, 
	@DescricaoPontuacaoInicial smallint, 
	@CaracteristicasPontuacaoInicial smallint, 
	@LinkPontuacaoInicial smallint, 
	@ImagemPontuacaoInicial smallint, 
	@DescricaoSugereInativo bit, 
	@CaracteristicasSugereInativo bit, 
	@LinkSugereInativo bit, 
	@ImagemSugereInativo bit, 
	@AtualizacaoAutomaticaInativaSugestao bit, 
	@ProdutosConfiancaPreco float, 
	@ProdutosConfiancaDisponibilidade float, 
	@Inativo bit, 
	@Versao datetime 


AS

	DECLARE @Erro nvarchar(255)


	-- VALIDAR SE O REGISTO EXISTE
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Fornecedores] WHERE [Codigo] = @Codigo)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U01] ' + 'O registo que pretende alterar não existe!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	-- VALIDAR SE A VERSÃO AINDA É A MESMA
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Fornecedores] WHERE [Codigo] = @Codigo AND [Versao] = @Versao)
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
		UPDATE [dbo].[Fornecedores] SET [Nome]=@Nome, 
										[Morada]=@Morada, 
										[CodigoPostal]=@CodigoPostal, 
										[LocalidadePostal]=@LocalidadePostal, 
										[Contribuinte]=@Contribuinte, 
										[Vendedor]=@Vendedor, 
										[Telefone]=@Telefone, 
										[Telemovel]=@Telemovel, 
										[SMS]=@SMS, 
										[Email]=@Email, 
										[AcessoOnlineAtivo]=@AcessoOnlineAtivo, 
										[Username]=@Username, 
										[Password]=@Password, 
										[HorasValidadeSugestao]=@HorasValidadeSugestao, 
										[ProdutosMatchingAutomatico]=@ProdutosMatchingAutomatico, 
										[ProdutosCriacaoAutomatica]=@ProdutosCriacaoAutomatica, 
										[DisponibilizaInfoProdutoDetalhe]=@DisponibilizaInfoProdutoDetalhe, 
										[DescricaoPontuacaoInicial]=@DescricaoPontuacaoInicial, 
										[CaracteristicasPontuacaoInicial]=@CaracteristicasPontuacaoInicial, 
										[LinkPontuacaoInicial]=@LinkPontuacaoInicial, 
										[ImagemPontuacaoInicial]=@ImagemPontuacaoInicial, 
										[DescricaoSugereInativo]=@DescricaoSugereInativo, 
										[CaracteristicasSugereInativo]=@CaracteristicasSugereInativo, 
										[LinkSugereInativo]=@LinkSugereInativo, 
										[ImagemSugereInativo]=@ImagemSugereInativo, 
										[AtualizacaoAutomaticaInativaSugestao]=@AtualizacaoAutomaticaInativaSugestao, 
										[ProdutosConfiancaPreco]=@ProdutosConfiancaPreco, 
										[ProdutosConfiancaDisponibilidade]=@ProdutosConfiancaDisponibilidade, 
										[Inativo]=@Inativo,
										[Versao] = @NovaVersao
								WHERE [Codigo] = @Codigo

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