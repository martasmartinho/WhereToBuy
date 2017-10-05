CREATE VIEW [dbo].[ProdutosView]
	AS 
	SELECT p.*, 
		   mp.*,
		   md.*,
		   s.Descricao as StockDescricao,
		   s1.Descricao as StockDescricao_U1,
		   s2.Descricao as StockDescricao_U2,
		   s3.Descricao as StockDescricao_U3,
		   COALESCE(pd.AtualizacaoManualNecessaria, 0) as AtualizacaoAutomatica,
		   COALESCE(f.ProdutosConfiancaPreco, 0) as ProdutosConfiancaPreco,
		   COALESCE(f.ProdutosConfiancaDisponibilidade, 0) as ProdutosConfiancaDisponibilidade,
		   COALESCE
			   (
					(
						SELECT MAX(pd.IndicePreocupacaoConteudo)
						FROM [dbo].[ProdutosDetalhe] pd
						WHERE pd.ProdutoCodigo = p.Codigo
					)
			   , 0) IPC


	FROM [dbo].[Produtos] p
		 INNER JOIN [dbo].[Fornecedores] f
			   ON p.FornecedorCodigo = f.Codigo
		 LEFT JOIN [dbo].[Stocks] s3
			  ON p.StockCodigo_U3 = s3.Codigo
		 LEFT JOIN [dbo].[Stocks] s2
			  ON p.StockCodigo_U2 = s2.Codigo
		 LEFT JOIN [dbo].[Stocks] s1
			  ON p.StockCodigo_U1 = s1.Codigo
		 LEFT JOIN [dbo].[Stocks] s
			  ON p.StockCodigo = s.Codigo
		LEFT JOIN  [dbo].[ProdutosDetalhe] pd
					ON pd.[ProdutoCodigo] = p.[Codigo] AND pd.[FornecedorCodigo] = p.[FornecedorCodigo]

		 CROSS APPLY [dbo].GetProdutoMedidasPrecoFunction( 
															p.PrecoCusto_U3, p.PrecoCusto_U3Data,
															p.PrecoCusto_U2, p.PrecoCusto_U2Data,
															p.PrecoCusto_U1, p.PrecoCusto_U1Data,
															p.PrecoCusto, p.PrecoCusto_Data,
															f.ProdutosConfiancaPreco
														 ) mp

		 CROSS APPLY [dbo].GetProdutoMedidasDisponibilidadeFunction(
																			s3.DisponibilidadeNivel, p.StockCodigo_U3Data,
																			s2.DisponibilidadeNivel, p.StockCodigo_U2Data,
																			s1.DisponibilidadeNivel, p.StockCodigo_U1Data,
																			s.DisponibilidadeNivel, p.StockCodigo_Data,
																			f.ProdutosConfiancaDisponibilidade
																   ) md
