/*
	
*/

CREATE TABLE [dbo].[SubgruposClasses]			-- exemplo de registo					
(
	[SubgrupoCodigo] nvarchar(20) NOT NULL,		-- TECH01.IMP01.IJT01	
	[ClasseCodigo] nvarchar(20) NOT NULL,		-- TECH01.IJT
	[Notas] nvarchar(256) NULL,					-- podem ser usadas por exemplo para justificar a regra..
	[Versao] datetime NOT NULL

	CONSTRAINT PK_SubgruposClasses PRIMARY KEY (SubgrupoCodigo, ClasseCodigo)
	CONSTRAINT FK_SubgruposClasses_Subgrupo FOREIGN KEY (SubgrupoCodigo) REFERENCES [dbo].[Subgrupos](Codigo)
	CONSTRAINT FK_SubgruposClasses_Classe FOREIGN KEY (ClasseCodigo) REFERENCES [dbo].[Classes](Codigo)
)