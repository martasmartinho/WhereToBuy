/*
	With this view we want to create a mechanism to simplify product creating (rows creation on Product table)

	This view is a set of quotations, ensuring only 1 row by Referencia.

	Only Quotations belonging to suppliers that are configured to automatic product creation are considered on this set.

*/


CREATE VIEW [dbo].[CotacoesMatrizCriacaoProdutosView]
	AS 

	SELECT *
	FROM
		(
			-- Get all quotations coming from suppliers autorized for automatic product creation, adding a last field with
			-- a numerator that resets every time that Referencia change. (It works like a counter of rows that have the same Referencia)
			SELECT c.*, ROW_NUMBER() OVER (PARTITION BY c.Referencia ORDER BY Referencia ASC) AS Classificacao
			FROM [dbo].[CotacoesIncompletasMapeamentoProdutoView] c
				 INNER JOIN [dbo].Fornecedores f
					   ON c.FornecedorCodigo = f.Codigo
			WHERE f.ProdutosCriacaoAutomatica = CAST('true' as bit)

		) cm1

	-- filter only the first row of each Referencia present in incomplete quotations view
	WHERE cm1.Classificacao = 1
		  -- and make sure that, that Referencia belongs to the group of not existing products 
		  -- (meaning that this Referencia is not a Product already created)
		  AND cm1.Referencia IN 
					(
						SELECT Referencia FROM [dbo].[ProdutosCodigosInexistentesView]
					)
		  -- make sure too, that the Partnumber does not belong to the ambiguous partnumbers set
		  AND cm1.Partnumber NOT IN
					(
						SELECT Partnumber FROM [dbo].[PartnumbersAmbiguosView]
					)