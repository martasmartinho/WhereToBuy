/*
	Esta tabela estabelece os mapeamentos existentes entre os complementos recebidos dos fornecedores e os complementos internos do WhereBoTuy
	Se uma regra estiver inativa (Inativo=true), as cotações dependentes da regra não serão mapeadas e serão lançados avisos na tabela CotacoesAvisos.
*/

CREATE TABLE [dbo].[ComplementosMatching]				-- exemplo de registo					
(
	[FornecedorCodigo] nvarchar(5) NOT NULL,	-- 00024	
	[Codigo] nvarchar(128) NOT NULL,			-- IE
	[Descricao] nvarchar(128) NOT NULL,			-- entrega incluida
	[MapTo] nvarchar(5) NULL,					-- 01
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_ComplementosMatching PRIMARY KEY (FornecedorCodigo, Codigo)
	CONSTRAINT FK_ComplementosMatching_Fornecedor FOREIGN KEY (FornecedorCodigo) REFERENCES [dbo].[Fornecedores](Codigo)
	CONSTRAINT FK_ComplementosMatching_MapTo FOREIGN KEY (MapTo) REFERENCES [dbo].[Complementos](Codigo)
)