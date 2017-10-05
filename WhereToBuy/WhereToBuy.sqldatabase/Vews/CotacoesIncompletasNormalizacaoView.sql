/*
	This view represents all Incomplete Quotations that are ready to last step of fulfillment - Normalization
	This view consumes SQL User Functions to calculate values to be updated on quotations records.
*/

CREATE VIEW [dbo].[CotacoesIncompletasNormalizacaoView]
	AS 

	SELECT -- original values from CotacoesIncompletasView
		   ci.*,

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
		   [dbo].[GetCotacaoDescricaoFunction](ci._Descricao, m.Descricao, ci.Partnumber, p.TermoAcrescentar, p.TermosRemover) AS [DescricaoUniformizada],
		   [dbo].[GetCotacaoCaracteristicasFunction](ci._Caracteristicas) AS [CaracteristicasUniformizadas],
		   [dbo].[GetCotacaoLinkFunction](ci._Link) AS [LinkUniformizado],
		   [dbo].[GetCotacaoImagemFunction](ci._Imagem) AS [ImagemUniformizada]

	-- link tables and user functions as needed
	FROM [dbo].[CotacoesIncompletasView] ci
		 INNER JOIN [dbo].[CotacoesRegras] cr
			   ON ci.FornecedorCodigo = cr.FornecedorCodigo
				  AND ci.MarcaCodigo = cr.MarcaCodigo
				  AND ci.CategoriaCodigo = cr.CategoriaCodigo
				  AND ci.StockCodigo = cr.StockCodigo
		 INNER JOIN [dbo].[ProdutosMatching] pm
			   ON ci.FornecedorCodigo = pm.FornecedorCodigo
				  AND ci.ComplementoCodigo = pm.ComplementoCodigo
				  AND ci._ProdutoCodigo = pm.Codigo
		 INNER JOIN [dbo].[Marcas] m
			   ON ci.MarcaCodigo = m.Codigo
		 INNER JOIN [dbo].[Complementos] p
			   ON ci.ComplementoCodigo = p.Codigo
		 CROSS APPLY [dbo].[GetCotacaoPrecoFunction](ci._Preco, ci._OutrosCustos, ci._OutrosCustosDescricao) cp
		 CROSS APPLY [dbo].[GetCotacaoValidadeFunction](ci.Data, ci._Validade, ci._ValidadeDescricao, cr.HorasValidade, pm.HorasValidadeCotacao) cv
		 CROSS APPLY [dbo].[GetCotacaoStockSubstitutoFunction](ci.StockCodigo, cr.StockCodigoSubstituto, pm.StockCodigoSubstituto) cs

	-- we want to filter only quotations that are fully mapped
	WHERE ci.MarcaCodigo IS NOT NULL
		  AND ci.CategoriaCodigo IS NOT NULL
		  AND ci.StockCodigo IS NOT NULL
		  AND ci.ImpostoCodigo IS NOT NULL
		  AND ci.ComplementoCodigo IS NOT NULL
		  AND ci.EstadoCodigo IS NOT NULL
		  AND ci.ProdutoCodigo IS NOT NULL
