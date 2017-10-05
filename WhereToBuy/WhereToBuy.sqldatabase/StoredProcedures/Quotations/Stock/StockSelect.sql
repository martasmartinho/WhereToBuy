CREATE PROCEDURE [dbo].[StockSelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2048), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT stocks.[Codigo], stocks.[Descricao], stocks.[DisponibilidadeNivel],
							stocks.[ValidadeP50_StockCodigo] as ValidadeP50Codigo, 
							stocks.[ValidadeP60_StockCodigo] as ValidadeP60Codigo, 
							stocks.[ValidadeP70_StockCodigo] as ValidadeP70Codigo, 
							stocks.[ValidadeP80_StockCodigo] as ValidadeP80Codigo, 
							stocks.[ValidadeP90_StockCodigo] as ValidadeP90Codigo, 
							stocks.[Notas], stocks.[Inativo], stocks.[Criacao], stocks.[Versao]				
						FROM [dbo].[Stocks] stocks'
							
	

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