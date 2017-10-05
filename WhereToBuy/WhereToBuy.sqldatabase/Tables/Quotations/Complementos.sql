/*
	Os complementos permitem que possam existir várias ofertas do mesmo produto desde que com serviços (complementos) distintos.
	O código do complemento será usado como sufixo do código de artigo principal.
	Ex: HPCB454A[01]	(é o produto HPCB454 mas com o serviço de entrega incluido, ou seja, com o complemento 01).
*/

CREATE TABLE [dbo].[Complementos]				-- registo exemplo
(
	[Codigo] nvarchar(5) NOT NULL,				-- 01
	[Descricao] nvarchar(50) NOT NULL,			-- produto com entrega incluida
	[TermoAcrescentar] nvarchar(50) NULL,		-- inclui entrega em PT continental
	[TermosRemover] nvarchar(100) NULL,			-- c/ entrega; c/ instalação; com instalação
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Complementos PRIMARY KEY (Codigo)
)
