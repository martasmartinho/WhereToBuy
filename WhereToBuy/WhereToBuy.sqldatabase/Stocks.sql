/*
	Esta tabela permite configurar os diferentes códigos (disposições) de stock existentes.
	É uma forma uniformizada para identificar os patamares de disponibilidade para cada artigo.

	O campo DisponibilidadeNivel é para ser preenchido com um inteiro do intervalor [-5,5] que representa a facilidade de encontrar disponibilidade
		> -5 significa que é totalmente impossivel haver disponibilidade para fornecer
		> 0 significa que não há stock mas é momentaneo
		> 5 significa stock abundante, dificilmente ou nunca deixa de existir disponibilidade
*/

CREATE TABLE [dbo].[Stocks]						-- exemplo de registo			outro exemplo		outro exemplo					outro exemplo
(
	[Codigo] nvarchar(5) NOT NULL,				-- 10							20					60								80
	[Descricao] nvarchar(50) NOT NULL,			-- Em Stock						Em Stock			sob encomenda					sob encomenda
	[DisponibilidadeNivel] smallint NOT NULL,	-- 4							2					(-1)							(-4)
	[ValidadeP50_StockCodigo] nvarchar(5) NULL, -- D+3							D+2					D+1								NULL
	[ValidadeP60_StockCodigo] nvarchar(5) NULL,	-- NULL							D+1					D+0								NULL
	[ValidadeP70_StockCodigo] nvarchar(5) NULL, -- NULL							NULL				D+0								NULL
	[ValidadeP80_StockCodigo] nvarchar(5) NULL,
	[ValidadeP90_StockCodigo] nvarchar(5) NULL,
	[Notas] nvarchar(256) NULL,					-- stock sempre disponivel		stock finito		-- sem stock, mas é só encomendar		sem stock e é dificil encontrar no mercado
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Stocks PRIMARY KEY (Codigo)
	CONSTRAINT FK_Stocks_ValidadeP50 FOREIGN KEY (ValidadeP50_StockCodigo) REFERENCES [dbo].[Stocks](Codigo)
	CONSTRAINT FK_Stocks_ValidadeP60 FOREIGN KEY (ValidadeP60_StockCodigo) REFERENCES [dbo].[Stocks](Codigo)
	CONSTRAINT FK_Stocks_ValidadeP70 FOREIGN KEY (ValidadeP70_StockCodigo) REFERENCES [dbo].[Stocks](Codigo)
	CONSTRAINT FK_Stocks_ValidadeP80 FOREIGN KEY (ValidadeP80_StockCodigo) REFERENCES [dbo].[Stocks](Codigo)
	CONSTRAINT FK_Stocks_ValidadeP90 FOREIGN KEY (ValidadeP90_StockCodigo) REFERENCES [dbo].[Stocks](Codigo)
)