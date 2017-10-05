CREATE PROCEDURE [dbo].[ProductMatchingSelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2048), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT m.[FornecedorCodigo], f.[Nome] as FornecedorNome, m.[Codigo], m.[ComplementoCodigo],
							m.[Descricao], COALESCE(m.[HorasValidadeCotacao], 0) as HorasValidadeCotacao, m.[StockCodigoSubstituto], s.[Descricao] as StockDescricao,
							m.[DispensaPrevencaoPrecosDesfasados],  m.[DispensaPrevencaoFalsoStock],
							m.[DataReset], m.[MapTo], m.[Notas], p.[Descricao] as ProdutoDescricao, m.[Inativo], m.[Criacao], m.[Versao]				
						FROM [dbo].[ProdutosMatching] m 
						INNER JOIN [dbo].[Fornecedores] f
							ON m.[FornecedorCodigo] = f.[Codigo] AND f.[INATIVO]='+'''FALSE''
						LEFT JOIN [dbo].[Stocks] s
							ON m.[StockCodigoSubstituto] = s.[Codigo] AND s.[INATIVO]='+'''FALSE''
						LEFT JOIN [dbo].[Produtos] p
							ON m.[MapTo] = p.[Codigo] AND p.[INATIVO]='+'''FALSE'''


	

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