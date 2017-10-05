/*
	Esta tabela estabelece os mapeamentos existentes entre os códigos de imposto recebidos dos fornecedores e os impostos internos do WhereBoTuy
	Se uma regra estiver inativa (Inativo=true), as cotações dependentes da regra não serão mapeadas e serão lançados avisos na tabela CotacoesAvisos.
*/

CREATE TABLE [dbo].[ImpostosMatching]			-- exemplo de registo					
(
	[FornecedorCodigo] nvarchar(5) NOT NULL,	-- 00024	
	[Codigo] nvarchar(128) NOT NULL,			-- 23
	[Descricao] nvarchar(128) NOT NULL,			-- taxa normal
	[MapTo] nvarchar(5) NULL,					-- IVA.PTC.N23
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_ImpostosMatching PRIMARY KEY (FornecedorCodigo, Codigo)
	CONSTRAINT FK_ImpostosMatching_Fornecedor FOREIGN KEY (FornecedorCodigo) REFERENCES [dbo].[Fornecedores](Codigo)
	CONSTRAINT FK_ImpostosMatching_MapTo FOREIGN KEY (MapTo) REFERENCES [dbo].[Impostos](Codigo)
)