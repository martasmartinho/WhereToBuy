/*
	Esta tabela vai armazenar os detalhes dos produtos que existem no sistema, por fornecedor.
	Cada ficha de produto pode ter várias linhas de detalhe consoante o numero de fornecedores que o forneça.
*/

CREATE TABLE [dbo].[ProdutosDetalhe]					
(													-- exemplo
	[ProdutoCodigo] nvarchar(40) NOT NULL,			-- HPCB514A[IE]	
	[FornecedorCodigo] nvarchar(5) NOT NULL,		-- 00024
	[Descricao] nvarchar(256) NOT NULL,				-- Tinteiro HP 521 CB514A (inclui entrega)
	[DescricaoPontuacao] smallint NOT NULL,			-- este sistema de pontuação permite distinguir de forma automática a credibilidade da informação recebida de vários fornecedores para o mesmo produto. Uma descrição com maior pontuação é mais fiável..
	[DescricaoInativa] bit NOT NULL,				-- este valor é sugerido automaticamente pela ficha do fornecedor.. quando queremos indicar que o fornecedor normalmente erra muito!!
	[Caracteristicas] nvarchar(2048) NOT NULL,		-- ..
	[CaracteristicasPontuacao] smallint NOT NULL,	
	[CaracteristicasInativas] bit NOT NULL, 
	[Link] nvarchar(1024) NOT NULL,					-- ..
	[LinkPontuacao] smallint NOT NULL,	
	[LinkInativo] bit NOT NULL, 
	[Imagem] nvarchar(1024) NOT NULL,				-- ..
	[ImagemPontuacao] smallint NOT NULL,	
	[ImagemInativa] bit NOT NULL, 
	[AtualizacaoAutomaticaInativa] bit NOT NULL,	-- Sugerido pela ficha do fornecedor no ato de inserção do ProdutosDetalhe. Permite configurar o sistema para, em caso de atualização de informação, não proceder a essa atualização de forma automática. Nestes casos apenas avisa..
	[AtualizacaoManualNecessaria] bit NOT NULL,		-- se verdadeiro, alerta o utilizador que há a necessidade de proceder a uma atualização manual da informação deste registo.
	[IndicePreocupacaoConteudo] tinyint NOT NULL,	-- [0,9] representa o nivel de preocupação a ter com o conteúdo da informação constante na descrição e nas caracteristicas.. 0 (zero) representa nenhuma gravidade e 9 gravidade extrema.
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL


	CONSTRAINT PK_ProdutosDetalhe PRIMARY KEY (ProdutoCodigo, FornecedorCodigo)
	CONSTRAINT FK_ProdutosDetalhe_Produto FOREIGN KEY (ProdutoCodigo) REFERENCES [dbo].[Produtos](Codigo)
	CONSTRAINT FK_ProdutosDetalhe_Fornecedor FOREIGN KEY (FornecedorCodigo) REFERENCES [dbo].[Fornecedores](Codigo)
)