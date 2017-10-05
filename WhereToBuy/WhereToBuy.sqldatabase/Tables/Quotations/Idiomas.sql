CREATE TABLE [dbo].[Idiomas]
(
	[Codigo] nvarchar(5) NOT NULL,			-- EN-UK Ou PT-PT
	[Descricao] nvarchar(50) NOT NULL,		-- Epson
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Idiomas PRIMARY KEY (Codigo)
)
