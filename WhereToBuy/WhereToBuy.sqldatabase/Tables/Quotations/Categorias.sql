/*
	> Tabela onde viverão as categorias tratadas pelo sistema.
	> A categoria é um classificador que facilita o mapeamento de familias, grupos, subgrupos, classes, etc., dos produtos importados de sistemas externos.
	> O correto desdobramento de categorias permitirá a segmentação de produtos no sistema e, adicionalmente, permitirá também uma eficiente ligação às classes
	  configuradas num catálogo.
	> Assim, a categoria funciona como um elo de ligação para com os sistemas externos (dos fornecedores) mas também como uma forma simples de mapear produtos para
	  os catálogos configurados no sistema.
	> O peso médio (indicado em gramas) permitirá o cálculo aproximado do peso total de um conjunto de produtos (por exemplo numa venda online).
*/

CREATE TABLE [dbo].[Categorias]				-- exemplo de registo
(
	[Codigo] nvarchar(20) NOT NULL,					-- TEC.IJT.01
	[Descricao] nvarchar(50) NOT NULL,				-- Tecnologia / Impressoras / Jato Tinta (Pequeno porte)   //as de grande porte já pesam em média 8kg, por exemplo
	[PesoMedioUnidade] float NOT NULL,				-- 2500			(leia-se 2500g ou 2,5Kg)
	[PrecoMinimoPermitido] decimal(14,4) NOT NULL,	-- preço mais baixo permitido na categoria
	[PrecoMaximoPermitido] decimal(14,4) NOT NULL,	-- preço mais alto permitido na categoria
	[PrecoAmplitudeMax] float NOT NULL,				-- 0.95, define o valor máximo de desfazamento de preços de custo permitido para produtos desta categoria.
	[Confianca] float NOT NULL,						-- [0,100], percentagem que espelha a confiança que existe nna Categoria. Esta percentagem entra como componente na equação que define o PCJV (Preço de Custo Justo de Venda).
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Categorias PRIMARY KEY (Codigo)
)
