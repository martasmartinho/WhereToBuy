/*
	Esta tabela permite categorizar cada registo da tabela CotacoesAvisos de forma a tornar mais fácil o seu agrupamento
	O campo Gravidade [0,9] deve ser preenchido com um valor inteiro representativo da gravidade do aviso. 9 = gravidade máxima.
*/

CREATE TABLE [dbo].[AvisosTipo]					-- exemplo de registo			outro exemplo									outro exemplo					
(
	[Codigo] nvarchar(5) NOT NULL,				-- PFS							IAPD											CIMM							
	[Descricao] nvarchar(128) NOT NULL,			-- Prevenção "Falso Stock"		Inativação Administrativa Preço Desfazado		Cotação Incompleta (Marca Matching - regra inativa)		
	[Gravidade] smallint NOT NULL,				-- 2							6												(4)								
	[Notas] nvarchar(256) NULL,					--                       		usado quando é encontrada uma cotação que..	usado quando..
	[Icon] nvarchar(20) NULL,
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_AvisosTipo PRIMARY KEY (Codigo)
)