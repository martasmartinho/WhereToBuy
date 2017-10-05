/*
	Catalog is an organization unit that group all products via classes that belongs to same family of products (named catalog)
*/

CREATE TABLE [dbo].[Catalogos]					-- registo exemplo
(
	[Codigo] nvarchar(20) NOT NULL,				-- TECH01
	[Descricao] nvarchar(50) NOT NULL,			-- Catalogo online de produtos de tecnologia
	[Notas] nvarchar(256) NULL,					-- podem ser usadas para indicar, por exemplo, a razão de existir este catalogo
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Catalogos PRIMARY KEY (Codigo)
)
