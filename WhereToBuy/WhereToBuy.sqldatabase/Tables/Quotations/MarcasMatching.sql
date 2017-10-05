/*
	Esta tabela estabelece os mapeamentos existentes entre as marcas recebidas dos fornecedores e os marcas internas do WhereBoTuy
	Se uma regra estiver inativa (Inativo=true), as cotações dependentes da regra não serão mapeadas e serão lançados avisos na tabela CotacoesAvisos.
*/

CREATE TABLE [dbo].[MarcasMatching]				-- exemplo de registo					
(
	[FornecedorCodigo] nvarchar(5) NOT NULL,	-- 000024	
	[Codigo] nvarchar(128) NOT NULL,				-- hp (código externo)
	[Descricao] nvarchar(128) NOT NULL,			-- Hewlett Packard (descrição externa)
	[MapTo] nvarchar(5) NULL,					-- HP (código interno)
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_MarcasMatching PRIMARY KEY (FornecedorCodigo, Codigo)
	CONSTRAINT FK_MarcasMatching_Fornecedor FOREIGN KEY (FornecedorCodigo) REFERENCES [dbo].[Fornecedores](Codigo)
	CONSTRAINT FK_MarcasMatching_MapTo FOREIGN KEY (MapTo) REFERENCES [dbo].[Marcas](Codigo)
)