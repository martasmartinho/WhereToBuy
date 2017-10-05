/*
	Tabela que permite configurar os estados de produto (promoção, usado, novo, oportunidade, etc.)
*/

CREATE TABLE [dbo].[Estados]					-- exemplo de registo
(
	[Codigo] nvarchar(5) NOT NULL,				-- NOVO
	[Descricao] nvarchar(50) NOT NULL,			-- Produto novo
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Estados PRIMARY KEY (Codigo)
)
