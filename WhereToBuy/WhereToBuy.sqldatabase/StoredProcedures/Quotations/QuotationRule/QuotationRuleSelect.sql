CREATE PROCEDURE [dbo].[QuotationRuleSelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2048), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT cr.[FornecedorCodigo], f.[Nome] as FornecedorNome, 
							cr.[MarcaCodigo],  m.[Descricao] as MarcaDescricao,
							cr.[CategoriaCodigo],  c.[Descricao] as CategoriaDescricao,
							cr.[StockCodigo],  s.[Descricao] as StockDescricao, s.DisponibilidadeNivel,
							cr.[HorasValidade], cr.[StockCodigoSubstituto], s1.[Descricao] as StockDescricaoSubstituto, cr.[DataReset], cr.[Notas], cr.[Versao]				
						FROM [dbo].[CotacoesRegras] cr 
						LEFT JOIN [dbo].[Fornecedores] f
							ON cr.[FornecedorCodigo] = f.[Codigo] AND f.[INATIVO]='+'''FALSE'''+
						'LEFT JOIN [dbo].[Marcas] m
							ON cr.[MarcaCodigo] = m.[Codigo] AND m.[INATIVO]='+'''FALSE'''+
						'LEFT JOIN [dbo].[Categorias] c
							ON cr.[CategoriaCodigo] = c.[Codigo]AND c.[INATIVO]='+'''FALSE'''+
						'LEFT JOIN [dbo].[Stocks] s
							ON cr.[StockCodigo] = s.[Codigo]AND s.[INATIVO]='+'''FALSE'''+
						'LEFT JOIN [dbo].[Stocks] s1
							ON cr.[StockCodigoSubstituto] = s1.[Codigo]AND s1.[INATIVO]='+'''FALSE'''
	

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