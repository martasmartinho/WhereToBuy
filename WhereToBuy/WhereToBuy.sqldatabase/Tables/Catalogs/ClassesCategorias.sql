/*
	
*/

CREATE TABLE [dbo].[ClassesCategorias]			-- exemplo de registo					
(
	[ClasseCodigo] nvarchar(20) NOT NULL,		-- TECH01.IJT	
	[CategoriaCodigo] nvarchar(20) NOT NULL,	-- TEC.IJT.01
	[Notas] nvarchar(256) NULL,					-- podem ser usadas por exemplo para justificar a regra..
	[Versao] datetime NOT NULL

	CONSTRAINT PK_ClassesCategorias PRIMARY KEY (ClasseCodigo, CategoriaCodigo)
	CONSTRAINT FK_ClassesCategorias_Classe FOREIGN KEY (ClasseCodigo) REFERENCES [dbo].[Classes](Codigo)
	CONSTRAINT FK_ClassesCategorias_Categoria FOREIGN KEY (CategoriaCodigo) REFERENCES [dbo].[Categorias](Codigo)
)