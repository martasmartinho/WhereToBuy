/*
	Esta tabela contem as regras de horas de validade e StockCodigoSubstituto a serem usadas para cada conjugação:
		Fornecedor / Marca / Categoria / Stock

	A DataReset serve para que uma regra possa ser deixada de considerar automaticamente a partir de determinado tempo.

	Os registos desta tabela são gerados automáticamente pela subtração dos distinct da View Cotacoes Atuais c/ as CotacoesRegras
*/

CREATE TABLE [dbo].[CotacoesRegras]				-- exemplo de registo					
(
	[FornecedorCodigo] nvarchar(5) NOT NULL,	-- 000024	
	[MarcaCodigo] nvarchar(5) NOT NULL,			-- HP
	[CategoriaCodigo] nvarchar(20) NOT NULL,	-- TEC.IJT.01
	[StockCodigo] nvarchar(5) NOT NULL,			-- 01
	[HorasValidade] smallint NOT NULL,			-- valor sugerido pela ficha do fornecedor. (usar um valor negativo para manter as cotações do fornecedor sempre fora de validade - inválidas)
	[StockCodigoSubstituto] nvarchar(5) NULL,	-- por defeito null, podemos definir o stock que queremos informar para cada conjugação fornecedor / marca / categoria / stock
	[DataReset] smalldatetime NULL,				-- nesta data os valores por defeito são assumidos automáticamente..
	[Notas] nvarchar(256) NULL,					-- podem ser usadas por exemplo para justificar a regra..
	[Versao] datetime NOT NULL

	CONSTRAINT PK_CotacoesRegras PRIMARY KEY (FornecedorCodigo, MarcaCodigo, CategoriaCodigo, StockCodigo)
	CONSTRAINT FK_CotacoesRegras_Fornecedor FOREIGN KEY (FornecedorCodigo) REFERENCES [dbo].[Fornecedores](Codigo)
	CONSTRAINT FK_CotacoesRegras_Marca FOREIGN KEY (MarcaCodigo) REFERENCES [dbo].[Marcas](Codigo)
	CONSTRAINT FK_CotacoesRegras_Categoria FOREIGN KEY (CategoriaCodigo) REFERENCES [dbo].[Categorias](Codigo)
	CONSTRAINT FK_CotacoesRegras_Stock FOREIGN KEY (StockCodigo) REFERENCES [dbo].[Stocks](Codigo)
	CONSTRAINT FK_CotacoesRegras_StockSubstituto FOREIGN KEY (StockCodigoSubstituto) REFERENCES [dbo].[Stocks](Codigo)
)