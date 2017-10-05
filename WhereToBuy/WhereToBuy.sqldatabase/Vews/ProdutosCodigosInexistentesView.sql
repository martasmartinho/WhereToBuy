/*
	This view represents all Product Codes (ProdutoCodigo) that does not exist and must be created.
*/

CREATE VIEW [dbo].[ProdutosCodigosInexistentesView]
	AS 
	
	-- set of Product Codes (ProdutoCodigo) deducted from Incomplete Quotations (missing only ProdutoCodigo mapping)
	SELECT DISTINCT Referencia
	FROM [dbo].[CotacoesIncompletasMapeamentoProdutoView]

	EXCEPT

	-- set of all existing (already) created Product Codes (ProdutoCodigo)
	SELECT Codigo
	FROM Produtos

