/*
	Esta tabela estabelece os mapeamentos existentes entre os códigos de estado recebidos dos fornecedores e os códigos de estado do WhereBoTuy
	Se uma regra estiver inativa (Inativo=true), as cotações dependentes da regra não serão mapeadas e serão lançados avisos na tabela CotacoesAvisos.
*/

CREATE TABLE [dbo].[EstadosMatching]			-- exemplo de registo					
(
	[FornecedorCodigo] nvarchar(5) NOT NULL,	-- 00024	
	[Codigo] nvarchar(128) NOT NULL,			-- promo
	[Descricao] nvarchar(128) NOT NULL,			-- promocao
	[MapTo] nvarchar(5) NULL,					-- PROMO
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_EstadosMatching PRIMARY KEY (FornecedorCodigo, Codigo)
	CONSTRAINT FK_EstadosMatching_Fornecedor FOREIGN KEY (FornecedorCodigo) REFERENCES [dbo].[Fornecedores](Codigo)
	CONSTRAINT FK_EstadosMatching_MapTo FOREIGN KEY (MapTo) REFERENCES [dbo].[Estados](Codigo)
)