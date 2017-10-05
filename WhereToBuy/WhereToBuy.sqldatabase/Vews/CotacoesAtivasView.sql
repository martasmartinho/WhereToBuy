/*
	NOTA:
	Esta view materializada, representa todas as cotações inativas = 'false'
		> Incompletas
		> Completas
		> Integradas

	- A view é materializada atendendo ao facto de poder representar um enorme volume de informação
*/


CREATE VIEW [dbo].[CotacoesAtivasView] WITH SchemaBinding -- todos os campos têm que ser especificados pelo facto de ser uma view materializada
	AS 
	SELECT [FornecedorCodigo], [Data], [_ProdutoCodigo], [_ComplementoCodigo], [_ComplementoDescricao],
	       [_Partnumber], [_MarcaCodigo], [_MarcaDescricao], [_CategoriaCodigo], [_CategoriaDescricao], [_StockCodigo], [_StockDescricao],
		   [_ImpostoCodigo], [_ImpostoDescricao], [_EstadoCodigo], [_EstadoDescricao], [_Descricao], [_Link], [_Caracteristicas], [_Imagem], 
		   [_Preco], [_OutrosCustos], [_OutrosCustosDescricao], [_Validade], [_ValidadeDescricao],
		   [ComplementoCodigo], [Partnumber], [MarcaCodigo], [CategoriaCodigo], 
		   [StockCodigo], [StockCodigoSubstituto], [StockCodigoSubstitutoJustificacao], 
		   CASE WHEN [StockCodigoSubstituto] IS NULL THEN [StockCodigo] ELSE [StockCodigoSubstituto] END AS [StockCodigoEfetivo],
		   [ImpostoCodigo], [EstadoCodigo], [Descricao], [Link], [Caracteristicas], [Imagem], 
		   [PrecoCusto], [PrecoCustoFormula], [Validade], [ValidadeFormula], 
		   [ProdutoCodigo], 
		   [Completo], [Integrado],
		   [Inativo], [Versao]
	
	FROM [dbo].[Cotacoes]
	WHERE [Inativo] = CAST('false' as bit);


-- O indice que torna esta view materializada chama-se IDX_CotacoesAtivas.
	