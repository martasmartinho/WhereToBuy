/*
	Esta tabela vai armazenar os produtos que existem no sistema.
*/

CREATE TABLE [dbo].[Produtos]					
(												-- exemplo
	[Codigo] nvarchar(40) NOT NULL,				-- HPCB514A[IE]		
	[Descricao] nvarchar(256) NOT NULL,			-- Tinteiro HP 521 CB514A (inclui entrega)
	[Partnumber] nvarchar(25) NOT NULL,			-- CB514A 				
	[CategoriaCodigo] nvarchar(20) NOT NULL,	
	[MarcaCodigo] nvarchar(5) NOT NULL,
	[ImpostoCodigo] nvarchar(5) NOT NULL,
	[FornecedorCodigo] nvarchar(5) NOT NULL,
	[PrecoCusto] decimal(14,4) NULL,			-- ultimo preço de custo
	[PrecoCusto_Data] smalldatetime NULL,		-- data de alteração do ultimo preço de custo
	[PrecoCusto_U1] decimal(14,4) NULL,			-- penultimo preço de custo
	[PrecoCusto_U1Data] smalldatetime NULL,		-- data de alteração do penultimo preço de custo
	[PrecoCusto_U2] decimal(14,4) NULL,			-- ..
	[PrecoCusto_U2Data] smalldatetime NULL,
	[PrecoCusto_U3] decimal(14,4) NULL,
	[PrecoCusto_U3Data] smalldatetime NULL,
	[StockCodigo] nvarchar(5) NULL,			-- ultima posição de stock
	[StockCodigo_Data] smalldatetime NULL,		-- data de alteração da posição de stock
	[StockCodigo_U1] nvarchar(5) NULL,			-- ..
	[StockCodigo_U1Data] smalldatetime NULL,
	[StockCodigo_U2] nvarchar(5) NULL,
	[StockCodigo_U2Data] smalldatetime NULL,
	[StockCodigo_U3] nvarchar(5) NULL,
	[StockCodigo_U3Data] smalldatetime NULL,
	[Descontinuado] bit NOT NULL,				-- artigo que já não é vendido (mono)
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL


	CONSTRAINT PK_Produtos PRIMARY KEY (Codigo)
	CONSTRAINT FK_Produtos_Categoria FOREIGN KEY (CategoriaCodigo) REFERENCES [dbo].[Categorias](Codigo)
	CONSTRAINT FK_Produtos_Marca FOREIGN KEY (MarcaCodigo) REFERENCES [dbo].[Marcas](Codigo)
	CONSTRAINT FK_Produtos_Imposto FOREIGN KEY (ImpostoCodigo) REFERENCES [dbo].[Impostos](Codigo)
	CONSTRAINT FK_Produtos_Fornecedor FOREIGN KEY (FornecedorCodigo) REFERENCES [dbo].[Fornecedores](Codigo)
	CONSTRAINT FK_Produtos_Stock FOREIGN KEY (StockCodigo) REFERENCES [dbo].[Stocks](Codigo)
	CONSTRAINT FK_Produtos_Stock_U1 FOREIGN KEY (StockCodigo_U1) REFERENCES [dbo].[Stocks](Codigo)
	CONSTRAINT FK_Produtos_Stock_U2 FOREIGN KEY (StockCodigo_U2) REFERENCES [dbo].[Stocks](Codigo)
	CONSTRAINT FK_Produtos_Stock_U3 FOREIGN KEY (StockCodigo_U3) REFERENCES [dbo].[Stocks](Codigo)
)