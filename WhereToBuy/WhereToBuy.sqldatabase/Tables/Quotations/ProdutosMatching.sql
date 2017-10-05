/*
	Esta tabela estabelece os mapeamentos existentes entre os códigos de imposto recebidos dos fornecedores e os impostos internos do WhereBoTuy
	Se uma regra estiver inativa (Inativo=true), as cotações dependentes da regra não serão mapeadas e serão lançados avisos na tabela CotacoesAvisos.
	> As regras definidas nos mapeamentos de produtos, sobrepõe as definidas em CotacoesRegras
*/

CREATE TABLE [dbo].[ProdutosMatching]			-- exemplo de registo					
(
	[FornecedorCodigo] nvarchar(5) NOT NULL,			-- 00024	
	[ComplementoCodigo] nvarchar(128) NOT NULL,			-- IE (codigo externo)
	[Codigo] nvarchar(256) NOT NULL,					-- x2413
	[Descricao] nvarchar(256) NOT NULL,					-- TINT HP CB PRETO
	[MapTo] nvarchar(40) NULL,							-- HPCB514A[01]
	[HorasValidadeCotacao] smallint NULL,				-- 24 (se preenchido, este campo define o tempo de validade para cada cotação alimentada por esta regra)
	[StockCodigoSubstituto] nvarchar(5) NULL,			-- 20 (se definido, este código define a disponibilidade de stock para as cotações alimentadas por esta regra)
	[DispensaPrevencaoPrecosDesfasados] bit NOT NULL,	-- Se verdadeiro, as cotações alimentadas por esta regra não serão objeto da prevenção "Preços Desfazados"
	[DispensaPrevencaoFalsoStock] bit NOT NULL,			-- se verdadeiro, as cotações alimentadas por esta regra não serão objeto de prevenção "Falso Stock"
	[DataReset] smalldatetime NULL,						-- define a data em que os campos de personalização da regra (HorasValidadeCotacao, StockCodigoSubsituto, DispensaPrevencaoPrecosDesfazados) são reiniciados (perdem o efeito)
	[Notas] nvarchar(256) NULL,							-- pode descrever-se neste campo a razão da configuração de regras especificas para esta regra de mapeamento.
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_ProdutosMatching PRIMARY KEY (FornecedorCodigo, ComplementoCodigo, Codigo)
	CONSTRAINT FK_ProdutosMatching_Fornecedor FOREIGN KEY (FornecedorCodigo) REFERENCES [dbo].[Fornecedores](Codigo)
	CONSTRAINT FK_ProdutosMatching_MapTo FOREIGN KEY (MapTo) REFERENCES [dbo].[Produtos](Codigo)
	CONSTRAINT FK_ProdutosMatching_StockSubstituto FOREIGN KEY (StockCodigoSubstituto) REFERENCES [dbo].[Stocks](Codigo)
)