-- This UNIQUE CLUSTERED INDEX exists to ofcialize the ProdutosAtivosView as materialized

CREATE UNIQUE CLUSTERED INDEX [ProdutosAtivosViewIndex] -- view to materialize
	ON [dbo].[ProdutosAtivosView]
	(Codigo)
