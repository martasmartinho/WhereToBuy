CREATE PROCEDURE [dbo].[ProductDetailsSelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2048), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT p.[ProdutoCodigo]
					   ,p.[FornecedorCodigo]
					   ,f.[Nome] as FornecedorNome
					   ,p.[Descricao]
					   ,p.[DescricaoPontuacao]
					   ,p.[DescricaoInativa]
					   ,p.[Caracteristicas]
					   ,p.[CaracteristicasPontuacao]
					   ,p.[CaracteristicasInativas]
					   ,p.[Link]
					   ,p.[LinkPontuacao]
					   ,p.[LinkInativo]
					   ,p.[Imagem]
					   ,p.[ImagemPontuacao]
					   ,p.[ImagemInativa]
					   ,p.[AtualizacaoAutomaticaInativa]
					   ,p.[AtualizacaoManualNecessaria]
					   ,p.[IndicePreocupacaoConteudo]
					   ,p.[Criacao]
					   ,p.[Versao]				
					FROM [dbo].[ProdutosDetalhe] as p
					INNER JOIN [dbo].[Fornecedores] as f ON
						f.Codigo = FornecedorCodigo'
	

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