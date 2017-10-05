﻿/*
Deployment script for WhereToBuy

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "WhereToBuy"
:setvar DefaultFilePrefix "WhereToBuy"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- cria a base de dados se não exstir ainda
IF NOT EXISTS(SELECT [Name] FROM [SYS].[DATABASES] WHERE [Name] = 'WhereToBuy')
BEGIN
	CREATE DATABASE WhereToBuy COLLATE LATIN1_GENERAL_CI_AS	-- ATENÇÃO QUE TEM TAMBÉM QUE SE ALTERAR A COLLATION NAS PROPRIEDADES DESTE PROJETO
END						
GO

GO
PRINT N'Creating [dbo].[GetTextoLimpoFunction]...';


GO
/*
	O objetivo desta função é limpar uma string com caracteres lixo e espaço em excesso, por exemplo.
*/

CREATE FUNCTION [dbo].GetTextoLimpoFunction
(
	@texto nvarchar(256)
)
RETURNS nvarchar(256)
AS
BEGIN

	-- retirar caracteres vazio inicias e fianis
	SET @texto = LTRIM(RTRIM(@texto));

	-- retirar espaços em excesso (apenas 1 é admissivel)
	SET @texto = REPLACE(@texto, '  ', ' ');
	SET @texto = REPLACE(@texto, '   ', ' ');
	SET @texto = REPLACE(@texto, '    ', ' ');
	SET @texto = REPLACE(@texto, '     ', ' ');

	-- retirar acentuação (neste momento temos a garantia que estão todos em maiusculas)
	SET @texto = REPLACE(@texto, 'À', 'A');
	SET @texto = REPLACE(@texto, 'Á', 'A');
	SET @texto = REPLACE(@texto, 'Ã', 'A');
	SET @texto = REPLACE(@texto, 'Â', 'A');
	SET @texto = REPLACE(@texto, 'Ä', 'A');
	SET @texto = REPLACE(@texto, 'È', 'E');
	SET @texto = REPLACE(@texto, 'É', 'E');
	SET @texto = REPLACE(@texto, 'Ê', 'E');
	SET @texto = REPLACE(@texto, 'Ë', 'E');
	SET @texto = REPLACE(@texto, 'Ì', 'I');
	SET @texto = REPLACE(@texto, 'Í', 'I');
	SET @texto = REPLACE(@texto, 'Î', 'I');
	SET @texto = REPLACE(@texto, 'Ï', 'I');
	SET @texto = REPLACE(@texto, 'Ò', 'O');
	SET @texto = REPLACE(@texto, 'Ó', 'O');
	SET @texto = REPLACE(@texto, 'Õ', 'O');
	SET @texto = REPLACE(@texto, 'Ô', 'O');
	SET @texto = REPLACE(@texto, 'Ö', 'O');
	SET @texto = REPLACE(@texto, 'Ù', 'U');
	SET @texto = REPLACE(@texto, 'Ú', 'U');
	SET @texto = REPLACE(@texto, 'Û', 'U');
	SET @texto = REPLACE(@texto, 'Ü', 'U');

	SET @texto = REPLACE(@texto, 'à', 'a');
	SET @texto = REPLACE(@texto, 'á', 'a');
	SET @texto = REPLACE(@texto, 'ã', 'a');
	SET @texto = REPLACE(@texto, 'â', 'a');
	SET @texto = REPLACE(@texto, 'ä', 'a');
	SET @texto = REPLACE(@texto, 'è', 'e');
	SET @texto = REPLACE(@texto, 'é', 'e');
	SET @texto = REPLACE(@texto, 'ê', 'e');
	SET @texto = REPLACE(@texto, 'ë', 'e');
	SET @texto = REPLACE(@texto, 'ì', 'i');
	SET @texto = REPLACE(@texto, 'í', 'i');
	SET @texto = REPLACE(@texto, 'î', 'i');
	SET @texto = REPLACE(@texto, 'ï', 'i');
	SET @texto = REPLACE(@texto, 'ò', 'o');
	SET @texto = REPLACE(@texto, 'ó', 'o');
	SET @texto = REPLACE(@texto, 'õ', 'o');
	SET @texto = REPLACE(@texto, 'ô', 'o');
	SET @texto = REPLACE(@texto, 'ö', 'o');
	SET @texto = REPLACE(@texto, 'ù', 'u');
	SET @texto = REPLACE(@texto, 'ú', 'u');
	SET @texto = REPLACE(@texto, 'û', 'u');
	SET @texto = REPLACE(@texto, 'ü', 'u');

	-- corrigir caracteres anomalos
	SET @texto = REPLACE(@texto, '´', '');
	SET @texto = REPLACE(@texto, '`', '');
	SET @texto = REPLACE(@texto, '"', '');
	SET @texto = REPLACE(@texto, '~', '');
	SET @texto = REPLACE(@texto, '^', '');
	SET @texto = REPLACE(@texto, '''', '');
	SET @texto = REPLACE(@texto, '¨', '');
	SET @texto = REPLACE(@texto, '\', '/');

	-- devolver texto limpo
	RETURN @texto;
END
GO
PRINT N'Altering [dbo].[GetCotacaoPartnumberFunction]...';


GO
ALTER FUNCTION [dbo].[GetCotacaoPartnumberFunction]
(
	@_Partnumber nvarchar(25)
)
RETURNS nvarchar(25)
AS
BEGIN

	-- limpar caracteres e espaços anómalos
	SET @_Partnumber = dbo.GetTextoLimpoFunction(@_Partnumber);

	-- os partnumbers serão sempre especificados em maiusculas
	SET @_Partnumber = UPPER(@_Partnumber);

	-- devolver o partnumber limpo e normalizado
	RETURN @_Partnumber;
END
GO
PRINT N'Creating [dbo].[GetCodigoExternoNormalizadoFunction]...';


GO
/*
	Esta função recebe um código externo e normaliza-o de forma a que obedeça sempre às regras internas.
	Pretende-se com esta medida diminuir às hipoteses de NotMatched falso. Por exemplo, se o código deixar de ser gerado pelo sistema externo em minusculas e passar
	a ser gerado em maiusculas ou com mais que um espaço...
*/



CREATE FUNCTION [dbo].[GetCodigoExternoNormalizadoFunction]
(
	@codigo nvarchar(256)
)
RETURNS nvarchar(25)
AS
BEGIN

	-- limpeza inicial
	SET @codigo = dbo.GetTextoLimpoFunction(@codigo);

	-- os códigos externos vão sempre ser transformados em minusculas
	SET @codigo = LOWER(@codigo);

	-- os espaços serão sempre underscores (existe a garantia que os espaços a mais foram removidos na função GetTextoLimpoFunction()
	SET @codigo = REPLACE(@codigo, ' ', '_');

	-- devolver o codigo normalizado e limpo
	RETURN @codigo;
END
GO
PRINT N'Altering [dbo].[CotacaoInsertSP]...';


GO
/*
	Esta Stored Procedure é tanto usada nas inserções unitárias (por exemplo a partir de um formulário na web), como
	nas inserções em lote...

	No caso das inserções em lote, antes da importação própriamente dita, as CotacoesIncompletas do fornecedor em questão 
	têm que ser removidas. Assim, o argumento @PrevineDuplicacoes deve ser passado com o valor 'false' já que esta prevenção
	foi efetuada com antecedência.

	No caso das inserções unitárias, o parametro @PrevineDuplicacoes deve ser passado com o valor 'true' para que a remoção
	de qualquer cotação incompleta, seja feita pela própria SP.

	!!! IMPORTANTE !!!
	> Existem campo que têm que ser obrigatóriamente indicados e outros que, se forem null (ou seja não indicados) serão sugeridos
	  automáticamente com (vazio).
	
	> Os campos Validade e ValidadeFormula são recebidos porque os prazos de validade de uma cotação podem ser
	  também comunicados pelo fornecedor. Se forem recebidos como NULL, então é o WhereToBuy que vai atribuir a validade.
*/

ALTER PROCEDURE [dbo].[CotacaoInsertSP]
	@PrevineDuplicacoes bit,
	@FornecedorCodigo nvarchar(20),
	@Data datetime,
	@_ProdutoCodigo nvarchar(256),
	@_ComplementoCodigo nvarchar(128),
	@_ComplementoDescricao nvarchar(128),
	@_Partnumber nvarchar(25),
	@_MarcaCodigo nvarchar(128),
	@_MarcaDescricao nvarchar(128),
	@_CategoriaCodigo nvarchar(128),
	@_CategoriaDescricao nvarchar(128),
	@_StockCodigo nvarchar(128),
	@_StockDescricao nvarchar(128),
	@_ImpostoCodigo nvarchar(128),
	@_ImpostoDescricao nvarchar(128),
	@_Descricao nvarchar(256),
	@_Link nvarchar(1024),
	@_Caracteristicas nvarchar(2048),
	@_Imagem nvarchar(1024),
	@_Preco decimal(14,4),
	@_OutrosCustos decimal(14,4),
	@_OutrosCustosDescricao nvarchar(128),
	@Validade smalldatetime,
	@ValidadeFormula nvarchar(256)
AS

	-- variaveis
	DECLARE @Erro nvarchar(255)



	-- @PrevineDuplicacoes
	IF @PrevineDuplicacoes IS NULL
	BEGIN
		-- disparar um erro
		SET @Erro = '[CotacaoInsertSP-01] (StoredProcedure)' + 'o valor do parametro @PrevineDuplicacoes não pode ser nulo!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END



	-- @FornecedorCodigo
	IF @FornecedorCodigo IS NULL
	BEGIN
		-- disparar um erro
		SET @Erro = '[CotacaoInsertSP-02] (StoredProcedure)' + 'o valor do parametro @FornecedorCodigo não pode ser nulo!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	IF LEN(RTRIM(@FornecedorCodigo)) < 1
	BEGIN
		-- disparar um erro
		SET @Erro = '[CotacaoInsertSP-03] (StoredProcedure)' + 'o valor do parametro @FornecedorCodigo tem um tamanho inválido!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END



	-- @Data
	IF @Data IS NULL
	BEGIN
		-- disparar um erro
		SET @Erro = '[CotacaoInsertSP-04] (StoredProcedure)' + 'o valor do parametro @Data não pode ser nulo!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END



	-- @_ProdutoCodigo
	/*
		SERÁ AVALIADO APENAS NO FINAL DOS OUTROS PARAMETROS, PORQUE PRECISAMOS DO VALOR DA MARCA E PARTNUMBER PARA CONSTRUIR UM 
		CÓDIGO DE FORNECEDOR CASO ESTE NÃO ENVIE UM.
	*/



	-- @_ComplementoCodigo
	IF @_ComplementoCodigo IS NULL
	BEGIN
		SET @_ComplementoCodigo = '(vazio)';
	END

	IF LEN(RTRIM(@_ComplementoCodigo)) < 1
	BEGIN
		SET @_ComplementoCodigo = '(vazio)';
	END
	/*ELSE
	BEGIN
		-- forçar que o código seja sempre em minusculas e sem espaços
		SET @_ComplementoCodigo = REPLACE(
		SET @_ComplementoCodigo = LOWER(RTRIM(LTRIM(@_ComplementoCodigo)));
	END*/



	-- @_ComplementoDescricao
	IF @_ComplementoDescricao IS NULL
	BEGIN
		SET @_ComplementoDescricao = @_ComplementoCodigo;
	END

	IF LEN(RTRIM(@_ComplementoDescricao)) < 1
	BEGIN
		SET @_ComplementoDescricao = @_ComplementoCodigo;
	END



	-- @_Partnumber
	IF @_Partnumber IS NULL
	BEGIN
		-- disparar um erro
		SET @Erro = '[CotacaoInsertSP-11] (StoredProcedure)' + 'o valor do parametro @_Partnumber não pode ser nulo!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	IF LEN(RTRIM(@_Partnumber)) < 1
	BEGIN
		-- disparar um erro
		SET @Erro = '[CotacaoInsertSP-12] (StoredProcedure)' + 'o valor do parametro @_Partnumber tem um tamanho inválido!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END



	-- @_MarcaCodigo
	IF @_MarcaCodigo IS NULL
	BEGIN
		-- disparar um erro
		SET @Erro = '[CotacaoInsertSP-13] (StoredProcedure)' + 'o valor do parametro @_MarcaCodigo não pode ser nulo!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	IF LEN(RTRIM(@_MarcaCodigo)) < 1
	BEGIN
		-- disparar um erro
		SET @Erro = '[CotacaoInsertSP-14] (StoredProcedure)' + 'o valor do parametro @_MarcaCodigo tem um tamanho inválido!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END



	-- @_MarcaDescricao
	IF @_MarcaDescricao IS NULL
	BEGIN
		SET @_MarcaDescricao = @_MarcaCodigo;
	END

	IF LEN(RTRIM(@_MarcaDescricao)) < 1
	BEGIN
		SET @_MarcaDescricao = @_MarcaCodigo;
	END



	-- @_CategoriaCodigo
	IF @_CategoriaCodigo IS NULL
	BEGIN
		-- disparar um erro
		SET @Erro = '[CotacaoInsertSP-17] (StoredProcedure)' + 'o valor do parametro @_CategoriaCodigo não pode ser nulo!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	IF LEN(RTRIM(@_CategoriaCodigo)) < 1
	BEGIN
		-- disparar um erro
		SET @Erro = '[CotacaoInsertSP-18] (StoredProcedure)' + 'o valor do parametro @_CategoriaCodigo tem um tamanho inválido!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END




	-- @_CategoriaDescricao
	IF @_CategoriaDescricao IS NULL
	BEGIN
		SET @_CategoriaDescricao = @_CategoriaCodigo;
	END

	IF LEN(RTRIM(@_CategoriaDescricao)) < 1
	BEGIN
		SET @_CategoriaDescricao = @_CategoriaCodigo;
	END




	-- @_StockCodigo
	IF @_StockCodigo IS NULL
	BEGIN
		SET @_StockCodigo = '(vazio)';
	END

	IF LEN(RTRIM(@_StockCodigo)) < 1
	BEGIN
		SET @_StockCodigo = '(vazio)';
	END



	
	-- @_StockDescricao
	IF @_StockDescricao IS NULL
	BEGIN
		SET @_StockDescricao = @_StockCodigo;
	END

	IF LEN(RTRIM(@_StockDescricao)) < 1
	BEGIN
		SET @_StockDescricao = @_StockCodigo;
	END




	-- @_ImpostoCodigo
	IF @_ImpostoCodigo IS NULL
	BEGIN
		SET @_ImpostoCodigo = '(vazio)';
	END

	IF LEN(RTRIM(@_ImpostoCodigo)) < 1
	BEGIN
		SET @_ImpostoCodigo = '(vazio)';
	END



	
	-- @_ImpostoDescricao
	IF @_ImpostoDescricao IS NULL
	BEGIN
		SET @_ImpostoDescricao = @_ImpostoCodigo;
	END

	IF LEN(RTRIM(@_ImpostoDescricao)) < 1
	BEGIN
		SET @_ImpostoDescricao = @_ImpostoCodigo;
	END




	-- @_Descricao
	IF @_Descricao IS NULL
	BEGIN
		-- disparar um erro
		SET @Erro = '[CotacaoInsertSP-29] (StoredProcedure)' + 'o valor do parametro @_Descricao não pode ser nulo!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	IF LEN(RTRIM(@_Descricao)) < 1
	BEGIN
		-- disparar um erro
		SET @Erro = '[CotacaoInsertSP-30] (StoredProcedure)' + 'o valor do parametro @_Descricao tem um tamanho inválido!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END




	-- @_Link
	IF @_Link IS NULL
	BEGIN
		SET @_Link = '(vazio)';
	END

	IF LEN(RTRIM(@_Link)) < 1
	BEGIN
		SET @_Link = '(vazio)';
	END




	-- @_Caracteristicas
	IF @_Caracteristicas IS NULL
	BEGIN
		SET @_Caracteristicas = '(vazio)';
	END

	IF LEN(RTRIM(@_Caracteristicas)) < 1
	BEGIN
		SET @_Caracteristicas = '(vazio)';
	END




	-- @_Imagem
	IF @_Imagem IS NULL
	BEGIN
		SET @_Imagem = '(vazio)';
	END

	IF LEN(RTRIM(@_Imagem)) < 1
	BEGIN
		SET @_Imagem = '(vazio)';
	END





	-- @_Preco
	IF @_Preco IS NULL
	BEGIN
		-- disparar um erro
		SET @Erro = '[CotacaoInsertSP-37] (StoredProcedure)' + 'o valor do parametro @_Preco não pode ser nulo!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END





	-- @_OutrosCustos
	IF @_OutrosCustos IS NULL
	BEGIN
		SET @_OutrosCustos = 0.0;
	END





	-- @_OutrosCustosDescricao
	IF @_OutrosCustosDescricao IS NULL
	BEGIN
		SET @_OutrosCustosDescricao = '(vazio)';
	END

	IF LEN(RTRIM(@_OutrosCustosDescricao)) < 1
	BEGIN
		SET @_OutrosCustosDescricao = '(vazio)';
	END





	-- LIMPAR @_Partnumber (já foi validado como recebido acim, agora pretende-se limpar)
	SET @_Partnumber = dbo.GetCotacaoPartnumberFunction(@_Partnumber);





	-- @_ProdutoCodigo
	IF @_ProdutoCodigo IS NULL
	BEGIN
		SET @_ProdutoCodigo = RTRIM(LTRIM(@_MarcaCodigo)) + '.' + RTRIM(LTRIM(@_Partnumber));
	END

	IF LEN(RTRIM(@_ProdutoCodigo)) < 1
	BEGIN
		SET @_ProdutoCodigo = RTRIM(LTRIM(@_MarcaCodigo)) + '.' + RTRIM(LTRIM(@_Partnumber));
	END





	-- PREVINE DUPLICAÇÕES (remove a CotacaoNaoIntegrada que possa existir para este FornecedorCodigo / _ProdutoCodigo
	IF @PrevineDuplicacoes = CAST('true' as bit)
	BEGIN
		DELETE FROM [dbo].CotacoesNaoIntegradasView
			   WHERE [FornecedorCodigo] = @FornecedorCodigo
					 AND [_ProdutoCodigo] = @_ProdutoCodigo
	END




	-- INSERIR
	SET NOCOUNT ON; -- não há necessidade de contar os registos (é sempre 1)


	INSERT INTO [dbo].[Cotacoes]
		   (
			   [FornecedorCodigo], [Data], [_ProdutoCodigo], [_ComplementoCodigo], [_ComplementoDescricao],
			   [_Partnumber], [_MarcaCodigo], [_MarcaDescricao], [_CategoriaCodigo], [_CategoriaDescricao], [_StockCodigo], [_StockDescricao],
			   [_ImpostoCodigo], [_ImpostoDescricao], [_Descricao], [_Link], [_Caracteristicas], [_Imagem], [_Preco], [_OutrosCustos], [_OutrosCustosDescricao],
			   [ComplementoCodigo], [Partnumber], [MarcaCodigo], [CategoriaCodigo], 
			   [StockCodigo], [StockCodigoSubstituto], [StockCodigoSubstitutoJustificacao], 
			   [ImpostoCodigo], [Descricao], [Link], [Caracteristicas], [PrecoCusto], [PrecoCustoFormula],
			   [Validade], [ValidadeFormula], [ProdutoCodigo], 
			   [Completo], [Integrado],
			   [Inativo], [Versao]
			)
			VALUES
			(
			   @FornecedorCodigo, @Data, @_ProdutoCodigo, @_ComplementoCodigo, @_ComplementoDescricao,
			   @_Partnumber, @_MarcaCodigo, @_MarcaDescricao, @_CategoriaCodigo, @_CategoriaDescricao, @_StockCodigo, @_StockDescricao,
			   @_ImpostoCodigo, @_ImpostoDescricao, @_Descricao, @_Link, @_Caracteristicas, @_Imagem, @_Preco, @_OutrosCustos, @_OutrosCustosDescricao,
			   null, null, null, null, 
			   null, null, null, 
			   null, null, null, null, null, null,
			   null, null, null, 
			   CAST('false' as bit), CAST('false' as bit),
			   CAST('false' as bit), GETDATE()
			);

RETURN 1;	-- devolve informação que inseriu um registo
GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


/* IMPOSTOS */
IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Impostos])
BEGIN	
	INSERT INTO [dbo].[Impostos] ([Codigo], [Descricao], [DesignacaoFiscal], [Taxa], [Inativo], [Criacao], [Versao])
		VALUES ('IVA.PTC.N23', 'IVA PT Continental - Taxa Normal de 23%', 'IVA PT.Continental 23%', 23.00, CAST('false' AS bit), GETDATE(), GETDATE())
END


/* COMPLEMENTOS */
IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Complementos])
BEGIN	
	INSERT INTO [dbo].[Complementos] ([Codigo], [Descricao], [TermoAcrescentar], [TermosRemover], [Inativo], [Criacao], [Versao])
		VALUES ('N/A', 'Não aplicável', NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE())
END


/* AVISOSTIPO */
IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[AvisosTipo])
BEGIN	
	INSERT INTO [dbo].[AvisosTipo] ([Codigo], [Descricao], [Gravidade], [Notas], [Icon], [Inativo], [Criacao], [Versao])
		 VALUES ('PFS', 'Prevenção Falso Stock', 3, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('IAPD', 'Inativação Administrativa (Prevenção Preço Desfazado)', 7, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CICM0', 'Cotação Incompleta [Complemento] (mapeamento incompleto, preenchimento manual necessário)', 6, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CICM1', 'Cotação Incompleta [Complemento] (mapeamento inativo)', 2, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CIMM0', 'Cotação Incompleta [Marca] (mapeamento incompleto, preenchimento manual necessário)', 6, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CIMM1', 'Cotação Incompleta [Marca] (mapeamento inativo)', 2, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CITM0', 'Cotação Incompleta [Categoria] (mapeamento incompleto, preenchimento manual necessário)', 6, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CITM1', 'Cotação Incompleta [Categoria] (mapeamento inativo)', 2, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CISM0', 'Cotação Incompleta [Stock] (mapeamento incompleto, preenchimento manual necessário)', 6, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CISM1', 'Cotação Incompleta [Stock] (mapeamento inativo)', 6, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CIIM0', 'Cotação Incompleta [Imposto] (mapeamento incompleto, preenchimento manual necessário)', 6, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CIIM1', 'Cotação Incompleta [Imposto] (mapeamento inativo)', 2, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CIPM0', 'Cotação Incompleta [Produto] (mapeamento incompleto, preenchimento manual necessário)', 6, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CIPM1', 'Cotação Incompleta [Produto] (mapeamento inativo)', 2, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CIPM2', 'Cotação Incompleta [Produto] (fornecedor configurado para não criar automaticamente fichas de produtos)', 6, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CIPM3', 'Cotação Incompleta [Produto] (fornecedor configurado para não mapear automaticamente produtos)', 6, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CIPM4', 'Cotação Incompleta [Produto] (produto inativo)', 2, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CIPM9', 'Cotação Incompleta [Produto] (ambiguidade detetada com outro partnumber de outra marca)', 8, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE())

END



/* STOCKS */
IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Stocks])
BEGIN	
	INSERT INTO [dbo].[Stocks] ([Codigo], [Descricao], [DisponibilidadeNivel], [Notas], [Inativo], [Criacao], [Versao])
		 VALUES ('D-5','Indisponivel permanentemente', -5, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('D-4','Indisponivel sob encomenda (dias entrega: +40)', -4, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('D-3','Indisponivel sob encomenda (dias entrega: +25)', -3, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('D-2','Indisponivel sob encomenda (dias entrega: +10)', -2, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('D-1','Indisponivel sob encomenda (dias entrega: +6)', -1, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('D+0','Indisponivel momentaneamente (dias entrega: +2)', 0, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('D+1','Disponivel, stock reduzido', 1, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('D+2','Disponivel, stock limitado', 2, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('D+3','Disponivel, stock normal', 3, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('D+4','Disponivel, stock abundante', 4, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('D+5','Disponivel permanentemente', 5, NULL, CAST('false' AS bit), GETDATE(), GETDATE())
END




/* FORNECEDORES */
IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Fornecedores])
BEGIN	
	INSERT INTO [dbo].[Fornecedores]
			   ([Codigo], [Nome], [Morada], [CodigoPostal], [LocalidadePostal], [Contribuinte], 
				[Vendedor], [Telefone], [Telemovel], [SMS], [Email], [AcessoOnlineAtivo], [Username], [Password],
				[HorasValidadeSugestao], [ProdutosMatchingAutomatico], [ProdutosCriacaoAutomatica], [DisponibilizaInfoProdutoDetalhe],
				[DescricaoPontuacaoInicial], [CaracteristicasPontuacaoInicial], [LinkPontuacaoInicial], [ImagemPontuacaoInicial],
				[DescricaoSugereInativo], [CaracteristicasSugereInativo], [LinkSugereInativo], [ImagemSugereInativo],
				[AtualizacaoAutomaticaInativaSugestao], [ProdutosConfiancaPreco], [ProdutosConfiancaDisponibilidade],
				[Inativo], [Criacao], [Versao])
		 VALUES
			   ('00024', 'CPC - Companhia Portuguesa de Computadores...', 'Rua Monte dos Pipos, 649', '4460-059', 'Guifões', '999999990',
				'Susana', '222333444', '939949959', '969979989', 'susana@cpcdi.pt', CAST('false' AS bit), 'cpc001', 'cpcpass',
				24, CAST('true' AS bit), CAST('true' AS bit), CAST('true' AS bit),
				5, 5, 5, 5,
				CAST('false' AS bit), CAST('false' AS bit), CAST('false' AS bit), CAST('false' AS bit),
				CAST('false' AS bit), 90, 90,
				CAST('false' AS bit), GETDATE(), GETDATE())
END
GO

GO
PRINT N'Update complete.';


GO