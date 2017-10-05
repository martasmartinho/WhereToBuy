/*
	This view represents all Incomplete Quotations that the only map missing it is Produtocodigo.

	This view add Referencia field to CotacoesIncompletasView. 
	This new field represents the Product Code correspondent to Quotations row.
*/

CREATE VIEW [dbo].[CotacoesIncompletasMapeamentoProdutoView]
	AS 
	SELECT *, [dbo].[GetProdutoCodigoFunction](Partnumber, MarcaCodigo, ComplementoCodigo) AS [Referencia]
	FROM [dbo].[CotacoesIncompletasView]
	WHERE MarcaCodigo IS NOT NULL
		  AND CategoriaCodigo IS NOT NULL
		  AND StockCodigo IS NOT NULL
		  AND ImpostoCodigo IS NOT NULL
		  AND ComplementoCodigo IS NOT NULL
		  AND EstadoCodigo IS NOT NULL
		  AND ProdutoCodigo IS NULL
