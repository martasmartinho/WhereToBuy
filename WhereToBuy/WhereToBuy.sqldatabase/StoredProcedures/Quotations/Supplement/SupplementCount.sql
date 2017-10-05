CREATE PROCEDURE [dbo].[SupplementCount]
	@WhereClause nvarchar(512) = ''			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
AS

	DECLARE @select nvarchar(1024), @where nvarchar(512), @sqlQuery nvarchar(2048)

	SET @select = 'SELECT Count([Codigo]) FROM [dbo].[Complementos]' 

	SET @where = LTRIM(RTRIM(@WhereClause))
	IF Len(@where) > 0
	BEGIN
		SET @where = ' WHERE ' + @where
	END


	SET @sqlQuery = @select + @where


	EXEC(@sqlQuery)

RETURN  -- This SP must be used with ExecuteScalar