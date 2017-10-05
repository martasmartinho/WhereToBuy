CREATE TABLE [dbo].[FornecedoresMarcas]
(
	[FornecedorCodigo] nvarchar(5) NOT NULL,			
	[MarcaCodigo] nvarchar(5) NOT NULL,		
	[Confianca] float NOT NULL,	
	[Notas] nvarchar(256) NULL,		
	[Versao] datetime NOT NULL

	CONSTRAINT PK_FornecedoresMarcas PRIMARY KEY (FornecedorCodigo, MarcaCodigo)
	CONSTRAINT FK_FornecedoresMarcas_Fornecedor FOREIGN KEY (FornecedorCodigo) REFERENCES [dbo].[Fornecedores](Codigo)
	CONSTRAINT FK_FornecedoresMarcas_marca FOREIGN KEY (MarcaCodigo) REFERENCES [dbo].[Marcas](Codigo)

)
