/*
	Esta tabela estabelece os mapeamentos existentes entre as categorias recebidas dos fornecedores e as categorias internas do WhereBoTuy
	Se uma regra estiver inativa (Inativo=true), as cotações dependentes da regra não serão mapeadas e serão lançados avisos na tabela CotacoesAvisos.
	> Uma categoria externa pode ser composta, ou seja, pode ser a familia concatenada com a subfamilia de forma a transformar os campos num unico valor a ser mapeado.
	  Por este motivo o campo código terá que ser bastante grande já que, como identificador de familia/subfamilia/grupo, podemos ter uma descrição e não um código
*/

CREATE TABLE [dbo].[CategoriasMatching]			-- exemplo de registo					
(
	[FornecedorCodigo] nvarchar(5) NOT NULL,	-- 00024	
	[Codigo] nvarchar(128) NOT NULL,			-- F0001/SF003
	[Descricao] nvarchar(128) NOT NULL,			-- Impressoras/Jato-tinta	
	[MapTo] nvarchar(20) NULL,					-- TEC.IJT.01
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_CategoriasMatching PRIMARY KEY (FornecedorCodigo, Codigo)
	CONSTRAINT FK_CategoriasMatching_Fornecedor FOREIGN KEY (FornecedorCodigo) REFERENCES [dbo].[Fornecedores](Codigo)
	CONSTRAINT FK_CategoriasMatching_MapTo FOREIGN KEY (MapTo) REFERENCES [dbo].[Categorias](Codigo)
)