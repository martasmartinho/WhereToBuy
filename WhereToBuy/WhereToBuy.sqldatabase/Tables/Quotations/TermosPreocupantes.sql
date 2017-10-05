/*
	
*/

CREATE TABLE [dbo].[TermosPreocupantes]				-- exemplo de registo
(
	[Termo] nvarchar(50) NOT NULL,					-- saldo, <a, promo, href, <br%		
	[Indice] tinyint NOT NULL,						-- 9, 3, 2, 3, 1
	[Notas] nvarchar(256) NULL,						
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_TermosPreocupantes PRIMARY KEY (Termo)
)