CREATE PROCEDURE [dbo].[SupplierInsert]
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
		INSERT INTO [dbo].[Fornecedores] ([Codigo], [Nome], [Morada], [CodigoPostal], [LocalidadePostal], [Contribuinte], 
											[Vendedor], [Telefone], [Telemovel], [SMS], [Email], [AcessoOnlineAtivo], 
											[Username], [Password], [HorasValidadeSugestao], [ProdutosMatchingAutomatico], 
											[ProdutosCriacaoAutomatica], [DisponibilizaInfoProdutoDetalhe], 
											[DescricaoPontuacaoInicial], [CaracteristicasPontuacaoInicial], 
											[LinkPontuacaoInicial], [ImagemPontuacaoInicial], [DescricaoSugereInativo], 
											[CaracteristicasSugereInativo], [LinkSugereInativo], [ImagemSugereInativo], 
											[AtualizacaoAutomaticaInativaSugestao], [ProdutosConfiancaPreco], 
											[ProdutosConfiancaDisponibilidade], [Inativo], [Criacao], [Versao])
 
					VALUES (@Codigo, @Nome, @Morada, @CodigoPostal, @LocalidadePostal, @Contribuinte, 
											@Vendedor, @Telefone, @Telemovel, @SMS, @Email, @AcessoOnlineAtivo, 
											@Username, @Password, @HorasValidadeSugestao, @ProdutosMatchingAutomatico, 
											@ProdutosCriacaoAutomatica, @DisponibilizaInfoProdutoDetalhe, 
											@DescricaoPontuacaoInicial, @CaracteristicasPontuacaoInicial, 
											@LinkPontuacaoInicial, @ImagemPontuacaoInicial, @DescricaoSugereInativo, 
											@CaracteristicasSugereInativo, @LinkSugereInativo, @ImagemSugereInativo, 
											@AtualizacaoAutomaticaInativaSugestao, @ProdutosConfiancaPreco, 
											@ProdutosConfiancaDisponibilidade, @Inativo, @Criacao, @Versao);

		
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
