/*
	Esta tabela albergará todas as cotações recebidas dos fornecedores.
	
	NOTAS:
		> Os campos externos (ex: _CategoriaCodigo, _CategoriaDescricao, ..) admitem strings muito grandes, para ser possivel tratar qualquer tipo de 
		  informação recebida como código. Por exemplo, o fornecedor pode não mandar o código da familia mas sim a sua descrição que terá que funcionar como código!!
*/

CREATE TABLE [dbo].[Cotacoes]										-- exemplo de registo		
(
	[FornecedorCodigo] nvarchar(5) NOT NULL,						-- 00024		
	[Data] datetime NOT NULL,										-- 2015/07/15 19:02:54 (data de criação do registo)
	[_ProdutoCodigo] nvarchar(256) NOT NULL,						-- x2413
	[_ComplementoCodigo] nvarchar(128) NOT NULL,					-- IE
	[_ComplementoDescricao] nvarchar(128) NOT NULL,					-- inclui entrega
	[_Partnumber] nvarchar(25) NOT NULL,							-- CB\514a (partnumber do fabricante)
	[_MarcaCodigo] nvarchar(128) NOT NULL,							-- hp
	[_MarcaDescricao] nvarchar(128) NOT NULL,						-- Hewlett Packard
	[_CategoriaCodigo] nvarchar(128) NOT NULL,						-- tin
	[_CategoriaDescricao] nvarchar(128) NOT NULL,					-- Tinteiros
	[_StockCodigo] nvarchar(128) NOT NULL,							-- Sim
	[_StockDescricao] nvarchar(128) NOT NULL,						-- Com stock
	[_ImpostoCodigo] nvarchar(128) NOT NULL,						-- 23
	[_ImpostoDescricao] nvarchar(128) NOT NULL,						-- taxa normal
	[_EstadoCodigo] nvarchar(128) NOT NULL,							-- promo
	[_EstadoDescricao] nvarchar(128) NOT NULL,						-- promoção
	[_Descricao] nvarchar(256) NOT NULL,							-- TINT HP CB PRETO
	[_Link] nvarchar(1024) NOT NULL,								-- http:// i3239923923
	[_Caracteristicas] nvarchar(2048) NOT NULL,						-- isto são caracteristicas teste..
	[_Imagem] nvarchar(1024) NOT NULL,								-- http://./img1.hpg; http://./img2.hpg; http://./img3.hpg;
	[_Preco] decimal(14,4) NOT NULL,								-- 90.00
	[_OutrosCustos] decimal(14,4) NOT NULL,							-- 5.00
	[_OutrosCustosDescricao] nvarchar(128) NOT NULL,				-- (TaxaEntrega + ECOREE)
	[_Validade] smalldatetime NOT NULL,								-- 2012-05-05
	[_ValidadeDescricao] nvarchar(128) NOT NULL,					-- Promoção XPTO (com data limitada)
	[ComplementoCodigo] nvarchar(5) NULL,							-- 01
	[Partnumber] nvarchar(25) NULL,									-- CB514A
	[MarcaCodigo] nvarchar(5) NULL,									-- HP
	[CategoriaCodigo] nvarchar(20) NULL,							-- Tinteiros
	[StockCodigo] nvarchar(5) NULL,									-- 20
	[StockCodigoSubstituto] nvarchar(5) NULL,						-- 10   ((quando há uma qualquer regra de negócio que sobrepoe o stock recebido do fornecedor))
	[StockCodigoSubstitutoJustificacao] nvarchar(256) NULL,			-- ((porque é que foi forçado um codigo de stock substituto))
	[ImpostoCodigo] nvarchar(5) NULL,								-- IVA.PTC.N23
	[EstadoCodigo] nvarchar(5) NULL,								-- PROMO
	[Descricao] nvarchar(256) NULL,									-- Tinteiro HP Preto CB514A
	[Link] nvarchar(1024) NULL,										-- http://www.hp.com/pppiwe
	[Caracteristicas] nvarchar(2048) NULL,							-- isto são caracteristcas teste normalizadas..
	[Imagem] nvarchar(1024) NULL,									-- http://./img1.hpg; http://./img2.hpg; http://./img3.hpg
	[PrecoCusto] decimal(14,4) NULL,								-- 95
	[PrecoCustoFormula] nvarchar(256) NULL,							-- PrecoCusto + (TaxaEntrega + ECOREE) = 90 + 5 = 95
	[Validade] smalldatetime NULL,									-- (até quando a cotação é valida)
	[ValidadeFormula] nvarchar(256) NULL,							-- (demonstração de como se chegou à data de validade)
	[ProdutoCodigo] nvarchar(40) NULL,								-- HPCB514A[01]
	[Completo] bit NOT NULL,										-- Uma cotação fica completa quando todos os seus mapeamentos ocorreram com sucesso.
	[Integrado] bit NOT NULL,										-- Uma cotação fica integrada quando é promovida a cotação atual.
	[Inativo] bit NOT NULL,										
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Cotacoes PRIMARY KEY (FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo)
	CONSTRAINT FK_Cotacoes_Fornecedor FOREIGN KEY (FornecedorCodigo) REFERENCES [dbo].[Fornecedores](Codigo)
	CONSTRAINT FK_Cotacoes_Complemento FOREIGN KEY (ComplementoCodigo) REFERENCES [dbo].[Complementos](Codigo)
	CONSTRAINT FK_Cotacoes_Marca FOREIGN KEY (MarcaCodigo) REFERENCES [dbo].[Marcas](Codigo)
	CONSTRAINT FK_Cotacoes_Categoria FOREIGN KEY (CategoriaCodigo) REFERENCES [dbo].[Categorias](Codigo)
	CONSTRAINT FK_Cotacoes_Stock FOREIGN KEY (StockCodigo) REFERENCES [dbo].[Stocks](Codigo)
	CONSTRAINT FK_Cotacoes_StockSubstituto FOREIGN KEY (StockCodigoSubstituto) REFERENCES [dbo].[Stocks](Codigo)
	CONSTRAINT FK_Cotacoes_Imposto FOREIGN KEY (ImpostoCodigo) REFERENCES [dbo].[Impostos](Codigo)
	CONSTRAINT FK_Cotacoes_Estado FOREIGN KEY (EstadoCodigo) REFERENCES [dbo].[Estados](Codigo)
	CONSTRAINT FK_Cotacoes_Produto FOREIGN KEY (ProdutoCodigo) REFERENCES [dbo].[Produtos](Codigo)
)