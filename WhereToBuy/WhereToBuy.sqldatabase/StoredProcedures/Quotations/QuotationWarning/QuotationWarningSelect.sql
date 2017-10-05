CREATE PROCEDURE [dbo].[QuotationWarningSelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2048), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT c.[Id], c.[Data], c.[_ProdutoCodigo], 
							c.[_ComplementoCodigo], c.[FornecedorCodigo], f.[Nome], f.[Contribuinte],
							c.[AvisoTipoCodigo], av.[Descricao] as AvisoTipoDescricao, av.[Gravidade], c.[Descricao], c.[Criacao]				
						FROM [dbo].[CotacoesAvisos] c 
						INNER JOIN [dbo].[Fornecedores] f
							ON c.[FornecedorCodigo] = f.[Codigo] and f.[Inativo]='+'''FALSE'''+'
						INNER JOIN [dbo].[AvisosTipo] av
							ON c.[AvisoTipoCodigo] = av.[Codigo] and av.[Inativo]='+'''FALSE'''  
	

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