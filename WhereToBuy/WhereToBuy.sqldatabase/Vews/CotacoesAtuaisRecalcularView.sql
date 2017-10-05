/*
	On this step, we must be sure that all fields in Actual Quotations (CotacoesAtuais) are correctly fulfilled regarding to actual
	ProdutosMatching and CotacoesRegras rules..

	This view reuses functions implemented to step 2 - normalization.
*/

CREATE VIEW [dbo].[CotacoesAtuaisRecalcularView]
	AS 

	SELECT -- original values from CotacoesAtuaisView
		   c.*,

		   -- calculated values of price
		   cp.PrecoCusto AS [PrecoCustoCalculado],
		   cp.Formula AS [FormulaPrecoCustoCalculado],

		   -- calculated values of expiration date
		   cv.Validade AS [ValidadeCalculada],
		   cv.Formula AS [FormulaValidadeCalculada],

		   -- calculated values of surrogate stock code
		   cs.StockCodigo AS [StockCodigoSubstitutoCalculado],
		   cs.Justificacao AS [JustificacaoStockCodigoSubstitutoCalculado],

		   -- get text fields uniformed
		   [dbo].[GetCotacaoDescricaoFunction](c._Descricao, m.Descricao, c.Partnumber, p.TermoAcrescentar, p.TermosRemover) AS [DescricaoUniformizada],
		   [dbo].[GetCotacaoCaracteristicasFunction](c._Caracteristicas) AS [CaracteristicasUniformizadas],
		   [dbo].[GetCotacaoLinkFunction](c._Link) AS [LinkUniformizado],
		   [dbo].[GetCotacaoImagemFunction](c._Imagem) AS [ImagemUniformizada]

	-- link tables and user functions as needed
	FROM [dbo].[CotacoesAtuaisView] c
		 INNER JOIN [dbo].[CotacoesRegras] cr
			   ON c.FornecedorCodigo = cr.FornecedorCodigo
				  AND c.MarcaCodigo = cr.MarcaCodigo
				  AND c.CategoriaCodigo = cr.CategoriaCodigo
				  AND c.StockCodigo = cr.StockCodigo
		 INNER JOIN [dbo].[ProdutosMatching] pm
			   ON c.FornecedorCodigo = pm.FornecedorCodigo
				  AND c.ComplementoCodigo = pm.ComplementoCodigo
				  AND c._ProdutoCodigo = pm.Codigo
		 INNER JOIN [dbo].[Marcas] m
			   ON c.MarcaCodigo = m.Codigo
		 INNER JOIN [dbo].[Complementos] p
			   ON c.ComplementoCodigo = p.Codigo
		 CROSS APPLY [dbo].[GetCotacaoPrecoFunction](c._Preco, c._OutrosCustos, c._OutrosCustosDescricao) cp
		 CROSS APPLY [dbo].[GetCotacaoValidadeFunction](c.Data, c._Validade, c._ValidadeDescricao, cr.HorasValidade, pm.HorasValidadeCotacao) cv
		 CROSS APPLY [dbo].[GetCotacaoStockSubstitutoFunction](c.StockCodigo, cr.StockCodigoSubstituto, pm.StockCodigoSubstituto) cs

	-- we want to filter only quotations that have data to be updated
	WHERE c.PrecoCusto <> cp.PrecoCusto
		  OR c.PrecoCustoFormula <> cp.Formula
		  OR c.Validade <> cv.Validade
		  OR c.ValidadeFormula <> cv.Formula
		  OR c.Descricao <> [dbo].[GetCotacaoDescricaoFunction](c._Descricao, m.Descricao, c.Partnumber, p.TermoAcrescentar, p.TermosRemover)
		  OR c.Caracteristicas <> [dbo].[GetCotacaoCaracteristicasFunction](c._Caracteristicas)
		  OR COALESCE(c.StockCodigoSubstituto, '') <> cs.StockCodigo				-- comparation of null with something else is always false
		  OR COALESCE(c.StockCodigoSubstitutoJustificacao, '') <> cs.Justificacao	-- " " "
