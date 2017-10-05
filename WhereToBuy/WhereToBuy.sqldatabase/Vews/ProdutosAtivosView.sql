/*
	This view shows only active produts (Inativo='false') and not descontinued products (Descontinuado='false')
*/


CREATE VIEW [dbo].[ProdutosAtivosView] WITH SchemaBinding -- all fields must be specified because we want a materialized view
	AS 
	SELECT [Codigo], [Descricao], [Partnumber], [CategoriaCodigo], [MarcaCodigo], [ImpostoCodigo], [FornecedorCodigo],
		   [PrecoCusto], [PrecoCusto_Data], [PrecoCusto_U1], [PrecoCusto_U1Data], [PrecoCusto_U2], [PrecoCusto_U2Data], [PrecoCusto_U3], [PrecoCusto_U3Data],
           [StockCodigo], [StockCodigo_Data], [StockCodigo_U1], [StockCodigo_U1Data], [StockCodigo_U2], [StockCodigo_U2Data], [StockCodigo_U3], [StockCodigo_U3Data],
           [Descontinuado], [Inativo], [Criacao], [Versao]
	FROM [dbo].[Produtos]
	WHERE Inativo = CAST('false' as bit)
		  AND Descontinuado = CAST('false' as bit);


-- index that transforms this view a mateirialized view calls IDX_ProdutosAtivos