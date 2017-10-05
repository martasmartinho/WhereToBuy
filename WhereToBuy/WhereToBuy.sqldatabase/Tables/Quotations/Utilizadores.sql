CREATE TABLE [dbo].[Utilizadores]
(
	[Username] nvarchar(20) NOT NULL,			
	[Password] nvarchar(250) NOT NULL,
	[Nome] nvarchar(50) NOT NULL,
	[Email] nvarchar(100) NOT NULL,
	[Mobile] nvarchar(15) NOT NULL,
	[Sms] nvarchar(15) NOT NULL,
	[Administrador] bit NOT NULL,
	[IdiomaCodigo] nvarchar(5) NOT NULL,		
	[Inativo] bit NOT NULL,
	[Criacao] smalldatetime NOT NULL,
	[Versao] datetime NOT NULL

	CONSTRAINT PK_Utilizadores PRIMARY KEY (Username),
	CONSTRAINT FK_Utilizador_Idioma FOREIGN KEY (IdiomaCodigo) REFERENCES [dbo].[Idiomas](Codigo)
)
