/*
	A class works as an aglomeration of products that belongs to the list of associated categories. 
	One Class map one to many Category(ies). The Products that belong to a classe, are the products that are
	related with the categories list related with that class.
	Classes are associated with one or more categories that in it's turn are related with products.
*/

CREATE TABLE [dbo].[Classes]					-- registo exemplo
(
	[Codigo] nvarchar(20) NOT NULL,				-- TECH01.IJT
	[Descricao] nvarchar(50) NOT NULL,			-- Impressoras jato de tinta
	[CatalogoCodigo] nvarchar(20) NOT NULL,		-- TECH01 (Catalogo online de produtos de tecnologia)
	[Margem] float NOT NULL,					-- 10
	[MargemValorMinimo] decimal(14,4) NOT NULL, -- 0.50 €
	[Notas] nvarchar(256) NULL,					-- podem ser usadas para indicar, por exemplo, a razão para se usar a magem..
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Classes PRIMARY KEY (Codigo)
	CONSTRAINT FK_Classes_Catalogo FOREIGN KEY (CatalogoCodigo) REFERENCES [dbo].[Catalogos](Codigo)
)
