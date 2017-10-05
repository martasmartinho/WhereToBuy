/*
	This view simplifies the process of determining what quotations have price out of the category interval..
*/

CREATE VIEW [dbo].[CotacoesAtuaisPrecosForaIntervaloView]
	AS 

	SELECT c.*,
		   t.PrecoMinimoPermitido, t.PrecoMaximoPermitido

	FROM [dbo].[CotacoesAtuaisView] c
		 INNER JOIN [dbo].[Categorias] t
			   ON c.CategoriaCodigo = t.Codigo
		 INNER JOIN [dbo].[ProdutosMatching] m
			   ON c.FornecedorCodigo = m.FornecedorCodigo
				  AND c.ComplementoCodigo = m.ComplementoCodigo
				  AND c._ProdutoCodigo = m.Codigo

	-- filter all Actual Quotations that the price is out of the interval defined on category,
	-- considering only ProductMatching rules that are configured with DispensaPrevencaoPrecosDesfazados = 'false'
	WHERE (c.PrecoCusto < t.PrecoMinimoPermitido OR c.PrecoCusto > t.PrecoMaximoPermitido)
		  AND m.DispensaPrevencaoPrecosDesfasados = CAST('false' as bit)