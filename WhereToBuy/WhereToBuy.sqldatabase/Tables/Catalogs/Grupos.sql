/*
	Group and Subgroup are organizational levels to permit to data consumers to navigate on records hierarchically
	Group is the top most level of navigation
*/

CREATE TABLE [dbo].[Grupos]						-- registo exemplo
(
	[Codigo] nvarchar(20) NOT NULL,				-- TECH01.IMP01
	[Descricao] nvarchar(50) NOT NULL,			-- Impressão
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Grupos PRIMARY KEY (Codigo)
)
