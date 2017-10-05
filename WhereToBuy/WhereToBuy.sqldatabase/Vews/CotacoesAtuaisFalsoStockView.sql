/*
	This view simplifies the process of determining fake stock. Creates a view that calculates percentil dates of quotation life,
	to analise which StockCodigo must be used on Stock Substitution. 

	This view uses the GetValidadePercentilFunction to calculate the date inside de specified interval, correspondent to the given percentil
*/

CREATE VIEW [dbo].[CotacoesAtuaisFalsoStockView]
	AS 

	SELECT 

		   -- all fields from atual quotations
		   c.*,
		   
		   -- necessary fields from stocks table
		   s.ValidadeP50_StockCodigo, s.ValidadeP60_StockCodigo, s.ValidadeP70_StockCodigo, s.ValidadeP80_StockCodigo, s.ValidadeP90_StockCodigo,

		   -- percentil data calculations
		   [dbo].[GetValidadePercentilFunction](c.Data, c.Validade, 50) AS P50,
		   [dbo].[GetValidadePercentilFunction](c.Data, c.Validade, 60) AS P60,
		   [dbo].[GetValidadePercentilFunction](c.Data, c.Validade, 70) AS P70,
		   [dbo].[GetValidadePercentilFunction](c.Data, c.Validade, 80) AS P80,
		   [dbo].[GetValidadePercentilFunction](c.Data, c.Validade, 90) AS P90

	FROM [dbo].[CotacoesAtuaisView] c
		 INNER JOIN [dbo].[Stocks] s
			   ON c.StockCodigoEfetivo = s.Codigo
		 INNER JOIN [dbo].[ProdutosMatching] m
			   ON c.FornecedorCodigo = m.FornecedorCodigo
				  AND c.ComplementoCodigo = m.ComplementoCodigo
			      AND c._ProdutoCodigo = m.Codigo

	-- apply to all actual quotations unless it's productmatching rule set it's dispensing
	WHERE m.DispensaPrevencaoFalsoStock = CAST('false' as bit)
		  AND (
				((GETDATE() >= [dbo].[GetValidadePercentilFunction](c.Data, c.Validade, 90)) AND (s.ValidadeP90_StockCodigo IS NOT NULL))
					OR ((GETDATE() >= [dbo].[GetValidadePercentilFunction](c.Data, c.Validade, 80)) AND (s.ValidadeP80_StockCodigo IS NOT NULL))
					OR ((GETDATE() >= [dbo].[GetValidadePercentilFunction](c.Data, c.Validade, 70)) AND (s.ValidadeP70_StockCodigo IS NOT NULL))
					OR ((GETDATE() >= [dbo].[GetValidadePercentilFunction](c.Data, c.Validade, 60)) AND (s.ValidadeP60_StockCodigo IS NOT NULL))
					OR ((GETDATE() >= [dbo].[GetValidadePercentilFunction](c.Data, c.Validade, 50)) AND (s.ValidadeP50_StockCodigo IS NOT NULL))
			  )