/*
	Tabela que guarda a informação acerca das marcas tratadas pelo sistema.
*/

CREATE TABLE [dbo].[Marcas]					-- exemplo de registo
(
	[Codigo] nvarchar(5) NOT NULL,			-- EPS
	[Descricao] nvarchar(50) NOT NULL,		-- Epson
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Marcas PRIMARY KEY (Codigo)
)
