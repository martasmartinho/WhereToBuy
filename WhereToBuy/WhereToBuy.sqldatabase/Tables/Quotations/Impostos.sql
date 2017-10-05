/*
	Tabela que permite configurar os impostos tratados pelo sistema.
*/

CREATE TABLE [dbo].[Impostos]					-- exemplo de registo
(
	[Codigo] nvarchar(5) NOT NULL,				-- IVA.PTC.N23
	[Descricao] nvarchar(50) NOT NULL,			-- Iva Portugal Taxa Normal 23%
	[DesignacaoFiscal] nvarchar(50) NOT NULL,	-- IVA PT(Continental) 23%
	[Taxa] float NOT NULL,						-- 23.00
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Impostos PRIMARY KEY (Codigo)
)
