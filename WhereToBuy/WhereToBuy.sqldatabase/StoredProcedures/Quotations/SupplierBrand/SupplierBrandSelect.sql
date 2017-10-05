CREATE PROCEDURE [dbo].[SupplierBrandSelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2048), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT fm.[FornecedorCodigo], fornecedores.[Nome] as FornecedorNome, fm.[MarcaCodigo], marcas.[Descricao] as MarcaDescricao, fm.[Confianca], 
							fm.[Notas], fm.[Versao]				
						FROM [dbo].[FornecedoresMarcas] fm 
						INNER JOIN [dbo].[Fornecedores] fornecedores
							ON fm.[FornecedorCodigo] = fornecedores.[Codigo] AND fornecedores.[INATIVO]='+'''FALSE'''+
						'INNER JOIN [dbo].[Marcas] marcas
							ON fm.[MarcaCodigo] = marcas.[Codigo] AND marcas.[INATIVO]='+'''FALSE'''
	

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