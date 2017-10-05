-- Este UNIQUE CLUSTERED INDEX existe para oficilizar que a View CotacoesAtivas é materializada

CREATE UNIQUE CLUSTERED INDEX [CotacoesAtivasViewIndex] -- Materialização da View CotacoesAtivas
	ON [dbo].[CotacoesAtivasView]
	(FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo)