/*
	This view represents all Incomplete Quotations that are ready to be configured as completed (Completo = True)
*/

CREATE VIEW [dbo].[CotacoesIncompletasFinalizacaoView]
	AS 

	SELECT *
	FROM [dbo].[CotacoesIncompletasView] ci

	-- we want to filter only quotations that are fully mapped
	WHERE ci.MarcaCodigo IS NOT NULL
		  AND ci.CategoriaCodigo IS NOT NULL
		  AND ci.StockCodigo IS NOT NULL
		  AND ci.ImpostoCodigo IS NOT NULL
		  AND ci.ComplementoCodigo IS NOT NULL
		  AND ci.EstadoCodigo IS NOT NULL
		  AND ci.ProdutoCodigo IS NOT NULL
		  AND ci.Descricao IS NOT NULL
		  AND ci.Caracteristicas IS NOT NULL
		  AND ci.Link IS NOT NULL
		  AND ci.Imagem IS NOT NULL
		  AND ci.PrecoCusto IS NOT NULL
		  AND ci.PrecoCustoFormula IS NOT NULL
		  AND ci.Validade IS NOT NULL
		  AND ci.ValidadeFormula IS NOT NULL
