/*
	Tabela de fornecedores tratados pelo sistema.
*/

CREATE TABLE [dbo].[Fornecedores]				-- exemplo de registo
(
	[Codigo] nvarchar(5) NOT NULL,							-- 00024
	[Nome] nvarchar(50) NOT NULL,							-- fornecedor Vende Tudo, SA
	[Morada] nvarchar(200) NOT NULL,						-- Avenida da Liberdade
	[CodigoPostal] nvarchar(15) NOT NULL,					-- 4000-001
	[LocalidadePostal] nvarchar(50) NOT NULL,				-- Matosinhos
	[Contribuinte] nvarchar(20) NOT NULL,					-- 500600700
	[Vendedor] nvarchar(50) NULL,						-- Ana Ferreira
	[Telefone] nvarchar(20) NULL,							-- 222333444
	[Telemovel] nvarchar(15) NOT NULL,						-- 919939969
	[SMS] nvarchar(15) NOT NULL,							-- 919939969
	[Email] nvarchar(100) NOT NULL,							-- encomendas@vendetudo.pt
	[AcessoOnlineAtivo] bit NOT NULL,						-- booleano que indica se o fornecedor pode aceder à plataforma de compras online
	[Username] nvarchar(20) NOT NULL,						-- ana@vendetudo.pt
	[Password] nvarchar(20) NOT NULL,						-- pAsSwOrD
	[HorasValidadeSugestao] smallint NOT NULL,				-- 48 (48h) (valor sugestão para a alimentação automática da tabela CotacoesRegras deste fornecedor)
	[ProdutosMatchingAutomatico] bit NOT NULL,				-- autoriza o algoritmo a tentar encontrar automaticamente o ProdutoCodigo das cotações deste fornecedor
	[ProdutosCriacaoAutomatica] bit NOT NULL,				-- autoriza o algoritmo a criar ficha de produto no caso de não existir já criado (usar somente com fornecedores não erráticos)
	[DisponibilizaInfoProdutoDetalhe] bit NOT NULL,			-- todas as cotações contribuem com a sua informação para a tabela ProdutoDetalhes. No entanto, só estará visivel publicamente a proveniente de fornecedores com esta flag a true.
	[DescricaoPontuacaoInicial] smallint NOT NULL,			-- as pontuações são usadas como forma de eleger, de entre a informação do mesmo produto enviada pelos diferentes fornecedores, qual será a classificada como mais fiável (completa). 
	[CaracteristicasPontuacaoInicial] smallint NOT NULL,	-- 
	[LinkPontuacaoInicial] smallint NOT NULL,				-- 
	[ImagemPontuacaoInicial] smallint NOT NULL,				--
	[DescricaoSugereInativo] bit NOT NULL,					-- se um fornecedor falha bastante, podemos inativar a informação com que contribui para ProdutoDetalhes. Apesar de ser importada, é inativada e, desta forma, não usada.
	[CaracteristicasSugereInativo] bit NOT NULL,			--
	[LinkSugereInativo] bit NOT NULL,						--
	[ImagemSugereInativo] bit NOT NULL,						--
	[AtualizacaoAutomaticaInativaSugestao] bit NOT NULL,	-- fornece o valor por defeito para cada ProdutoDetalhe. Se false, quando houver a atualização de um valor (por exemplo, caracteristicas), esta alteração não é feita automaticamente, ou seja, é apenas notificada.
	[ProdutosConfiancaPreco] float NOT NULL,				-- [0,100], percentagem que espelha a confiança que existe nos preços indicados pelo fornecedor. Esta percentagem entra como componente na equação que define o ICP (Indice de Confiança no Preço).
	[ProdutosConfiancaDisponibilidade] float NOT NULL,		-- [0,100], percentagem que espelha a confiança que existe nas disponibilidades indicadas pelo fornecedor. ESta percentagem entra como componente na equação que define o ICD (Indice de Confiança na Disponibilidade).
	[Inativo] bit NOT NULL,	
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Fornecedores PRIMARY KEY (Codigo)
)