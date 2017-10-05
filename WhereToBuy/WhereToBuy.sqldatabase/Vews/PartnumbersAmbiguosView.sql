/*
	This view compares the deducted Product Codes from Incomplete Quotations and Product Codes already created. Then, when
	they have a match and the entites (quotation vs product) have different brands, it assumes a Partnumber conflit.

	The view shows ambiguous partnumbers, in other words, all partnumbers that on quotations side belongs to a brand 1 and in product table
	belongs to brand 2..

	If that prevention was a fake negative, user must create the product manually and map it manually too.
*/


CREATE VIEW [dbo].[PartnumbersAmbiguosView]
	AS 
	
	SELECT DISTINCT c.Partnumber

	-- link Products to Quotations via Simplified Partnumber on both sides
	FROM [dbo].[Produtos] p
		 INNER JOIN [dbo].[CotacoesIncompletasMapeamentoProdutoView] c
				ON [dbo].[GetCotacaoPartnumberSimplificadoFunction](p.Partnumber) = [dbo].GetCotacaoPartnumberSimplificadoFunction(c.Partnumber)

	-- filter only not existing products and the ones that brand does not fit on the two sides (quotations - products)
	WHERE c.Referencia NOT IN
		(
			SELECT Codigo FROM [dbo].[Produtos]
		)
		AND p.MarcaCodigo <> c.MarcaCodigo