/*
	Esta tabela regista todos os eventos necessários alertar ao utilizador acerca de cotações. 
	Funciona como um LOG de tratamento de cotações
*/

CREATE TABLE [dbo].[CotacoesAvisos]				-- exemplo de registo					
(
	[Id] uniqueidentifier NOT NULL,				-- {1234:1234:ABCD:1234..
	[Data] smalldatetime NOT NULL,				-- Data da cotação (não confundir com a data de criação deste registo porque podem ser diferentes). Esta Data é a data em que o fornecedor gerou a cotação (ex: 6:00am)
	[_ProdutoCodigo] nvarchar(256) NOT NULL,		-- X2413													
	[_ComplementoCodigo] nvarchar(128) NOT NULL,	-- IE	
	[FornecedorCodigo] nvarchar(5) NOT NULL,	-- 000024													
	[AvisoTipoCodigo] nvarchar(5) NOT NULL,	-- PFS (Prevenção Falso Stock)
	[Descricao] nvarchar(2048) NOT NULL,		-- Deve ser um campo grande porque pode conter demonstrações matemáticas..
	[Criacao] smalldatetime NOT NULL			-- Data de criação (lançamento do aviso)

	CONSTRAINT PK_CotacoesAvisos PRIMARY KEY (Id)
	CONSTRAINT FK_CotacoesAvisos_Fornecedor FOREIGN KEY (FornecedorCodigo) REFERENCES [dbo].[Fornecedores](Codigo)
	CONSTRAINT FK_CotacoesAvisos_AvisoTipo FOREIGN KEY (AvisoTipoCodigo) REFERENCES [dbo].[AvisosTipo](Codigo)

	-- não pode haver mais que um registo com a mesma FornecedorCodigo / Data / _ProdutoCodigo / _ComplementoCodigo / AvisoTipoCodigo
	CONSTRAINT UC_CotacoesAvisos UNIQUE (FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo)
)