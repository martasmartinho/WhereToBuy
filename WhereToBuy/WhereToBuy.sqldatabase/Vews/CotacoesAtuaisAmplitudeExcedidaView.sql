/*
	This view simplifies the process of determining what quotations do not respect the amplitude defined on it's category.
	The set of quotations represented by this view, are the quotations that have problems with amplitude defined on each category.
*/

CREATE VIEW [dbo].[CotacoesAtuaisAmplitudeExcedidaView]
	AS 

	SELECT c.ProdutoCodigo,


		   -- price amplitude of this product
		   MIN(c.PrecoCusto) / MAX(c.PrecoCusto) AS AmplitudePercentual,
		   
		   
		   -- how the deviation of the amplitude has been calculated (formula)
		   '(min/max) < A  <=>  ' +
		   '(' + CAST(MIN(c.PrecoCusto) as nvarchar) + '/' + CAST(MAX(c.PrecoCusto) as nvarchar) + ')  <  ' + CAST(t.PrecoAmplitudeMax as nvarchar) + '  <=>  ' +
		    CAST(ROUND(MIN(c.PrecoCusto) / MAX(c.PrecoCusto), 2) as nvarchar) + '  <  ' + CAST(t.PrecoAmplitudeMax as nvarchar) 
			AS AmplitudeFormulaDetecaoDesfasamento,


			-- calculate which price (min or max) is offset when compared with the average
			CASE 
				 -- if diference between min and avg is greater then the avg to the max
				 WHEN ABS(MIN(c.PrecoCusto) - AVG(c.PrecoCusto)) > (MAX(c.PrecoCusto) - AVG(c.PrecoCusto)) 
							THEN MIN(c.PrecoCusto)

				 -- if diference between min and avg is less then the avg to the max
				 WHEN ABS(MIN(c.PrecoCusto) - AVG(c.PrecoCusto)) < (MAX(c.PrecoCusto) - AVG(c.PrecoCusto)) 
							THEN MAX(c.PrecoCusto)

				 -- if min and max are the same that means that, probably, there are only 2 quotations causing the average to be the same.
				 -- if that happens, we consider that the wrong price is the lowest (min), because it is the most problematic with customers.
				 ELSE 
					MIN(c.PrecoCusto)
			END AS PrecoDesfasado,

			
			-- detecting the offset price (it is min or max the most distant from the average)
			CASE 
				 -- if diference between min and avg is greater then the avg to the max
				 WHEN ABS(MIN(c.PrecoCusto) - AVG(c.PrecoCusto)) > (MAX(c.PrecoCusto) - AVG(c.PrecoCusto)) 
							THEN '| min-avg | > (max-avg)  <=>  ' +
							     '| ' + CAST(MIN(c.PrecoCusto) as nvarchar) + '-' + CAST(AVG(c.PrecoCusto) as nvarchar) + ' | > (' + CAST(MAX(c.PrecoCusto) as nvarchar) + '-' + CAST(AVG(c.PrecoCusto) as nvarchar) + ')  <=>  ' +
								 CAST(ABS(MIN(c.PrecoCusto) - AVG(c.PrecoCusto)) as nvarchar) +' > ' + CAST((MAX(c.PrecoCusto) - AVG(c.PrecoCusto)) as nvarchar) + ' **preço mais barato é o mais afastado da média!'
								 
				 -- if diference between min and avg is less then the avg to the max
				 WHEN ABS(MIN(c.PrecoCusto) - AVG(c.PrecoCusto)) < (MAX(c.PrecoCusto) - AVG(c.PrecoCusto)) 
							THEN '| min-avg | < (max-avg)  <=>  ' +
							     '| ' + CAST(MIN(c.PrecoCusto) as nvarchar) + '-' + CAST(AVG(c.PrecoCusto) as nvarchar) + ' | < (' + CAST(MAX(c.PrecoCusto) as nvarchar) + '-' + CAST(AVG(c.PrecoCusto) as nvarchar) + ')  <=>  ' +
								 CAST(ABS(MIN(c.PrecoCusto) - AVG(c.PrecoCusto)) as nvarchar) +' < ' + CAST((MAX(c.PrecoCusto) - AVG(c.PrecoCusto)) as nvarchar) + ' **preço mais caro é o mais afastado da média!'
					
				 -- if min and max are the same that means that, probably, there are only 2 quotations causing the average to be the same.
				 -- if that happens, we consider that the wrong price is the lowest (min), because it is the most problematic with customers.
				 ELSE 
					  '| min-avg | = (max-avg) <=> probably there are only 2 quotations => consider the lowest price wrong because it is the most dangerous!'
				 
			END AS PrecoDesfasadoFormulaDecisao
			

	FROM [dbo].[CotacoesAtuaisView] c
		 INNER JOIN [dbo].[Categorias] t
			   ON c.CategoriaCodigo = t.Codigo
		 INNER JOIN [dbo].[ProdutosMatching] m
			   ON c.FornecedorCodigo = m.FornecedorCodigo
			      AND c.ComplementoCodigo = m.ComplementoCodigo
				  AND c._ProdutoCodigo = m.Codigo


	-- we want filter only quotations which ProductMatching rules are defined to not prevent PreçosDesfazados
	WHERE m.DispensaPrevencaoPrecosDesfasados = CAST('false' as bit)


	-- preform a group by each ProdutoCodigo
	GROUP BY c.ProdutoCodigo, t.PrecoAmplitudeMax


	-- filter only groups with irregular amplitude 
	-- and the product has more than one quotations to be compared
	HAVING (MIN(c.PrecoCusto) / MAX(c.PrecoCusto)) < t.PrecoAmplitudeMax
		   AND COUNT('-') >= 2