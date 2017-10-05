/*
	Quotations of this view will be configured as disabled because it's offset price..
*/

CREATE VIEW [dbo].[CotacoesAtuaisAmplitudeIrregularView]
	AS 

	SELECT c.*,
		   ca.AmplitudePercentual, ca.AmplitudeFormulaDetecaoDesfasamento, ca.PrecoDesfasado, ca.PrecoDesfasadoFormulaDecisao

	FROM [dbo].[CotacoesAtuaisAmplitudeExcedidaView] ca
		 INNER JOIN [dbo].[CotacoesAtuaisView] c
			   ON ca.ProdutoCodigo = c.ProdutoCodigo
				  AND ca.PrecoDesfasado = c.PrecoCusto