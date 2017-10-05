CREATE PROCEDURE [dbo].[SupplierSelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2048), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT f.[Codigo], f.[Nome], f.[Morada], 
							f.[CodigoPostal], f.[LocalidadePostal], 
							f.[Contribuinte], f.[Vendedor], 
							f.[Telefone], f.[Telemovel], 
							f.[SMS], f.[Email], 
							f.[AcessoOnlineAtivo], 
							f.[Username], f.[Password], 
							f.[HorasValidadeSugestao], 
							f.[ProdutosMatchingAutomatico], 
							f.[ProdutosCriacaoAutomatica], 
							f.[DisponibilizaInfoProdutoDetalhe], 
							f.[DescricaoPontuacaoInicial], 
							f.[CaracteristicasPontuacaoInicial], 
							f.[LinkPontuacaoInicial], 
							f.[ImagemPontuacaoInicial], 
							f.[DescricaoSugereInativo], 
							f.[CaracteristicasSugereInativo], 
							f.[LinkSugereInativo], 
							f.[ImagemSugereInativo], 
							f.[AtualizacaoAutomaticaInativaSugestao], 
							f.[ProdutosConfiancaPreco], 
							f.[ProdutosConfiancaDisponibilidade], 
							f.[Inativo], f.[Criacao], 
							f.[Versao] 
						FROM [dbo].[Fornecedores] f' 
	

	SET @where = LTRIM(RTRIM(@WhereClause))
	IF Len(@where) > 0
	BEGIN
		SET @where = ' WHERE ' + @where
	END


	SET @orderBy = LTRIM(RTRIM(@OrderByClause))
	IF LEN(@orderBy) > 0
	BEGIN
		SET @orderBy = ' ORDER BY ' + @orderBy
	END


	SET @sqlQuery = @select + @where + @orderBy  


	EXEC(@sqlQuery)


RETURN @@ROWCOUNT