/*
	Shows in a single only row per correspondence FornecedorCodigo, ComplementoCodigo, _ProdutoCodigo,
	quotations that does exist in Product table (Product already exists).

	We want to Map all Product Matching rules that correspondent ProdutoCodigo already exists on Product table.
*/


CREATE VIEW [dbo].[CotacoesMatrizPreenchimentoMapToView]
	AS 

	SELECT *
	FROM
		(
				-- Get all quotations coming from suppliers autorized to automatic match rules of Product Matchings.
				-- Add a last field with a numerator that resets every time that change the sequence FornecedorCodigo / ComplementoCodigo / _ProdutoCodigo
				-- The numerator works as a counter of rows with the same FornecedorCodigo / ComplementoCodigo / _ProdutoCodigo
				SELECT c.*, ROW_NUMBER() OVER (
												PARTITION BY c.FornecedorCodigo, c.ComplementoCodigo, c._ProdutoCodigo 
												ORDER BY c.FornecedorCodigo, c.ComplementoCodigo, c._ProdutoCodigo 
											  ) AS [Classificacao]
				FROM [dbo].[CotacoesIncompletasMapeamentoProdutoView] c
					 INNER JOIN [dbo].[Fornecedores] f
						ON c.FornecedorCodigo = f.Codigo
				WHERE f.ProdutosMatchingAutomatico = CAST('true' as bit)
		) cm1

	-- filter only the first occurrence of FornecedorCodigo / ComplementoCodigo / _ProdutoCodigo
	WHERE cm1.Classificacao = 1
		  -- and make sure that only get quotations of already created products
		  AND cm1.Referencia IN
			  (
				   SELECT Codigo FROM [dbo].[Produtos]
			  )
