/*
	Group and Subgroup are organizational levels to permit to data consumers to navigate on records hierarchically
	Group is the top most level of navigation
*/

CREATE TABLE [dbo].[Subgrupos]					-- registo exemplo
(
	[Codigo] nvarchar(20) NOT NULL,				-- TECH01.IMP01.IJT01
	[Descricao] nvarchar(50) NOT NULL,			-- Impressoras Jacto de Tinta
	[GrupoCodigo] nvarchar(20) NOT NULL,		-- TECH01.IMP01 (Impressão)
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Subgrupos PRIMARY KEY (Codigo)
	CONSTRAINT FK_Subgrupos_Grupo FOREIGN KEY (GrupoCodigo) REFERENCES [dbo].[Grupos](Codigo)
)
