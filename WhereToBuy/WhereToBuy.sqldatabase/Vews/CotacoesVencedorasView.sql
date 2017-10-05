/*
	This view shows all winner quotations. 
	Winner Quotations are all Actual Quotations (one by ProdutoCodigo) that it is the best offer (stock + price).
	It's based on winner quotations that is made the update of price on product record.
*/

CREATE VIEW [dbo].[CotacoesVencedorasView]
	AS 
		
	SELECT *
	FROM
		(

			SELECT cc1.*, ROW_NUMBER() OVER (PARTITION BY cc1.ProdutoCodigo 
													   ORDER BY s3.DisponibilidadeNivel DESC
											) AS Classificacao2
			FROM
				(

					-- subset of Actual Quotations with stock availability
					SELECT c1.*,
						   ROW_NUMBER() OVER (PARTITION BY c1.ProdutoCodigo 
														ORDER BY c1.PrecoCusto ASC,
																 ((f1.ProdutosConfiancaPreco + f1.ProdutosConfiancaDisponibilidade) / 2) DESC
											 ) AS Classificacao1

					FROM [dbo].[CotacoesAtuaisView] c1
						 INNER JOIN [dbo].[Fornecedores] f1
							   ON c1.FornecedorCodigo = f1.Codigo
						 INNER JOIN [dbo].[Stocks] s1
							   ON c1.StockCodigoEfetivo = s1.Codigo

					-- filter only quotations which has availability of stock
					WHERE s1.DisponibilidadeNivel > 0


					UNION


					-- subset of Actual Quotations with no stock availability
					SELECT c2.*,
						   ROW_NUMBER() OVER (PARTITION BY c2.ProdutoCodigo 
														ORDER BY c2.PrecoCusto ASC,
																 ((f2.ProdutosConfiancaPreco + f2.ProdutosConfiancaDisponibilidade) / 2) DESC
											 ) AS Classificacao1

					FROM [dbo].[CotacoesAtuaisView] c2
						 INNER JOIN [dbo].[Fornecedores] f2
							   ON c2.FornecedorCodigo = f2.Codigo
						 INNER JOIN [dbo].[Stocks] s2
							   ON c2.StockCodigoEfetivo = s2.Codigo

					-- filter only quotations which has availability of stock
					WHERE s2.DisponibilidadeNivel <= 0
	
				) cc1
					 INNER JOIN [dbo].[Stocks] s3
						   ON cc1.StockCodigoEfetivo = s3.Codigo
	
			-- filter only
			WHERE cc1.Classificacao1 = 1

		) cc2

	-- only the first quotation of each partition
	WHERE cc2.Classificacao2 = 1