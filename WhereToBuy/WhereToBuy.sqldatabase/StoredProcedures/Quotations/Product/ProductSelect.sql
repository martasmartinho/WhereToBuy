CREATE PROCEDURE [dbo].[ProductSelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2500), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT p.[Codigo], p.[Descricao], p.[Partnumber], 
							p.[CategoriaCodigo], c.[Descricao] as CategoriaDescricao,
							p.[MarcaCodigo], m.[Descricao] as MarcaDescricao,
							p.[ImpostoCodigo], i.[Descricao] as ImpostoDescricao, i.[DesignacaoFiscal] as DesignacaoFiscal,
							p.[FornecedorCodigo], f.[Nome] as FonecedorNome, 
							COALESCE(f.[ProdutosConfiancaPreco],0) as ProdutosConfiancaPreco, 
							COALESCE(f.[ProdutosConfiancaDisponibilidade],0) as ProdutosConfiancaDisponibilidade, 
							ICDFormula,
							pd.[AtualizacaoManualNecessaria] as AtualizacaoAutomatica,  COALESCE(pd.[IndicePreocupacaoConteudo], 0) as IPC,
							ICP, ICPFormula, ICPCE, ICPCEFormula, ICPCT, ICPCTFormula, ICPCF, ICPCFFormula,
							EEP, EEPFormula,
							EED, EEDFormula,
							ICD, ICDFormula, ICDCE, ICDCEFormula, ICDC,ICDCTFormula, ICDCF, ICDCFFormula,							
							COALESCE(p.[PrecoCusto], 0) as PrecoCusto, p.[PrecoCusto_Data],
							COALESCE(p.[PrecoCusto_U1], 0) as PrecoCusto_U1, p.[PrecoCusto_U1Data], 
							COALESCE(p.[PrecoCusto_U2], 0) as PrecoCusto_U2, p.[PrecoCusto_U2Data], 
							COALESCE(p.[PrecoCusto_U3], 0) as PrecoCusto_U3, p.[PrecoCusto_U3Data], 
							p.[StockCodigo], s.[Descricao] as StockDescricao,p.[StockCodigo_Data], 
							p.[StockCodigo_U1], sU1.[Descricao] as StockDescricao_U1, p.[StockCodigo_U1Data], 
							p.[StockCodigo_U2], sU2.[Descricao] as StockDescricao_U2, p.[StockCodigo_U2Data], 
							p.[StockCodigo_U3], sU3.[Descricao] as StockDescricao_U3,p.[StockCodigo_U3Data], 
							p.[Descontinuado], p.[Inativo], 
							p.[Criacao], p.[Versao]
						FROM [dbo].[ProdutosView] p
						INNER JOIN  [dbo].[Fornecedores] f
							ON f.[Codigo] = p.[FornecedorCodigo]
						LEFT JOIN  [dbo].[Categorias] c
							ON c.[Codigo] = p.[CategoriaCodigo]
						LEFT JOIN  [dbo].[Marcas] m
							ON m.[Codigo] = p.[MarcaCodigo]
						LEFT JOIN  [dbo].[Impostos] i
							ON i.[Codigo] = p.[ImpostoCodigo]
						LEFT JOIN  [dbo].[ProdutosDetalhe] pd
							ON pd.[ProdutoCodigo] = p.[Codigo] AND pd.[FornecedorCodigo] = p.[FornecedorCodigo]
						LEFT JOIN  [dbo].[Stocks] s
							ON s.[Codigo] = p.[StockCodigo]
						LEFT JOIN  [dbo].[Stocks] sU1
							ON sU1.[Codigo] = p.[StockCodigo_U1]
						LEFT JOIN  [dbo].[Stocks] sU2
							ON sU2.[Codigo] = p.[StockCodigo_U2]
						LEFT JOIN  [dbo].[Stocks] sU3
							ON sU3.[Codigo] = p.[StockCodigo_U3]'
							 
	

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