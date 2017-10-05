/*
	Esta tabela estabelece os mapeamentos existentes entre as disponibilidades de stocks recebidas dos fornecedores e as classificações de stock internas do WhereBoTuy
	Se uma regra estiver inativa (Inativo=true), as cotações dependentes da regra não serão mapeadas e serão lançados avisos na tabela CotacoesAvisos.
*/

CREATE TABLE [dbo].[StocksMatching]			-- exemplo de registo					
(
	[FornecedorCodigo] nvarchar(5) NOT NULL,	-- 00024	
	[Codigo] nvarchar(128) NOT NULL,			-- Sim
	[Descricao] nvarchar(128) NOT NULL,			-- Com Stock	
	[MapTo] nvarchar(5) NULL,					-- 20
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_StocksMatching PRIMARY KEY (FornecedorCodigo, Codigo)
	CONSTRAINT FK_StocksMatching_Fornecedor FOREIGN KEY (FornecedorCodigo) REFERENCES [dbo].[Fornecedores](Codigo)
	CONSTRAINT FK_StocksMatching_MapTo FOREIGN KEY (MapTo) REFERENCES [dbo].[Stocks](Codigo)
)