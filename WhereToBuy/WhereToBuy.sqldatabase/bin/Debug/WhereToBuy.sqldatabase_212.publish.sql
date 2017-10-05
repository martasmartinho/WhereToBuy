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
PRINT N'Altering [dbo].[GetCotacaoDescricaoFunction]...';


GO
/*
	This function receives the original description, brand name, partnumber, text to add and text to remove,
	and build a uniformed complete final product description
*/


ALTER FUNCTION [dbo].[GetCotacaoDescricaoFunction]
(
	@_Descricao nvarchar(256),
	@MarcaDescricao nvarchar(50),
	@Partnumber nvarchar(25),
	@TermoAcrescentar nvarchar(50),
	@TermosRemover nvarchar(100)
)
RETURNS nvarchar(256)
AS
BEGIN

	-- variables
	DECLARE @descricao nvarchar(256);
	DECLARE @termo as nvarchar(256);
	DECLARE termosCursor CURSOR FOR
		SELECT DISTINCT Element
		FROM [dbo].[SplitStringFunction](LOWER(@TermosRemover),',');


	-- force lowercase to description
	SET @descricao = LOWER(@_Descricao);

	-- ensure that brand name is there
	IF CHARINDEX(RTRIM(LOWER(@MarcaDescricao)), @descricao) = 0
	BEGIN
		SET @descricao = RTRIM(@descricao) + ' ' + RTRIM(LOWER(@MarcaDescricao));
	END

	-- ensure that partnumber is there
	IF CHARINDEX(RTRIM(LOWER(@Partnumber)), @descricao) = 0
	BEGIN
		SET @descricao = RTRIM(@descricao) + ' ' + RTRIM(LOWER(@Partnumber));
	END


	-- remove undesirable terms
	OPEN termosCursor;

	-- first fetch
	FETCH NEXT FROM termosCursor INTO @termo;

	-- iterate all records in cursor
	WHILE @@FETCH_STATUS = 0
	BEGIN

		-- remove term
		SET @descricao = REPLACE(@descricao, (' ' + RTRIM(LTRIM(@termo))), '');

		-- next fetch
		FETCH NEXT FROM termosCursor INTO @termo;

	END

	-- close cursor
	CLOSE termosCursor;
	DEALLOCATE termosCursor;


	-- concatenate term to add
	IF LEN(@TermoAcrescentar) > 0 
	BEGIN
		SET @descricao = @descricao + ' ' + @TermoAcrescentar;
	END


	-- final bad chars cleaning
	SET @descricao = [dbo].[CleanBadCharsFunction](@descricao);
		
	-- send back the normalized description
	RETURN @descricao;
END
GO
PRINT N'Altering [dbo].[GetTextoLimpoFunction]...';


GO
/*
	O objetivo desta função é limpar uma string com caracteres lixo e espaço em excesso, por exemplo.
*/

ALTER FUNCTION [dbo].GetTextoLimpoFunction
(
	@texto nvarchar(256)
)
RETURNS nvarchar(256)
AS
BEGIN

	-- remove accents
	SET @texto = REPLACE(@texto, 'À' COLLATE Latin1_General_CS_AS, 'A'); -- CS -> Case Sensitive
	SET @texto = REPLACE(@texto, 'Á' COLLATE Latin1_General_CS_AS, 'A');
	SET @texto = REPLACE(@texto, 'Ã' COLLATE Latin1_General_CS_AS, 'A');
	SET @texto = REPLACE(@texto, 'Â' COLLATE Latin1_General_CS_AS, 'A');
	SET @texto = REPLACE(@texto, 'Ä' COLLATE Latin1_General_CS_AS, 'A');
	SET @texto = REPLACE(@texto, 'È' COLLATE Latin1_General_CS_AS, 'E');
	SET @texto = REPLACE(@texto, 'É' COLLATE Latin1_General_CS_AS, 'E');
	SET @texto = REPLACE(@texto, 'Ê' COLLATE Latin1_General_CS_AS, 'E');
	SET @texto = REPLACE(@texto, 'Ë' COLLATE Latin1_General_CS_AS, 'E');
	SET @texto = REPLACE(@texto, 'Ì' COLLATE Latin1_General_CS_AS, 'I');
	SET @texto = REPLACE(@texto, 'Í' COLLATE Latin1_General_CS_AS, 'I');
	SET @texto = REPLACE(@texto, 'Î' COLLATE Latin1_General_CS_AS, 'I');
	SET @texto = REPLACE(@texto, 'Ï' COLLATE Latin1_General_CS_AS, 'I');
	SET @texto = REPLACE(@texto, 'Ò' COLLATE Latin1_General_CS_AS, 'O');
	SET @texto = REPLACE(@texto, 'Ó' COLLATE Latin1_General_CS_AS, 'O');
	SET @texto = REPLACE(@texto, 'Õ' COLLATE Latin1_General_CS_AS, 'O');
	SET @texto = REPLACE(@texto, 'Ô' COLLATE Latin1_General_CS_AS, 'O');
	SET @texto = REPLACE(@texto, 'Ö' COLLATE Latin1_General_CS_AS, 'O');
	SET @texto = REPLACE(@texto, 'Ù' COLLATE Latin1_General_CS_AS, 'U');
	SET @texto = REPLACE(@texto, 'Ú' COLLATE Latin1_General_CS_AS, 'U');
	SET @texto = REPLACE(@texto, 'Û' COLLATE Latin1_General_CS_AS, 'U');
	SET @texto = REPLACE(@texto, 'Ü' COLLATE Latin1_General_CS_AS, 'U');
	SET @texto = REPLACE(@texto, 'Ç' COLLATE Latin1_General_CS_AS, 'C');

	SET @texto = REPLACE(@texto, 'à' COLLATE Latin1_General_CS_AS, 'a');
	SET @texto = REPLACE(@texto, 'á' COLLATE Latin1_General_CS_AS, 'a');
	SET @texto = REPLACE(@texto, 'ã' COLLATE Latin1_General_CS_AS, 'a');
	SET @texto = REPLACE(@texto, 'â' COLLATE Latin1_General_CS_AS, 'a');
	SET @texto = REPLACE(@texto, 'ä' COLLATE Latin1_General_CS_AS, 'a');
	SET @texto = REPLACE(@texto, 'è' COLLATE Latin1_General_CS_AS, 'e');
	SET @texto = REPLACE(@texto, 'é' COLLATE Latin1_General_CS_AS, 'e');
	SET @texto = REPLACE(@texto, 'ê' COLLATE Latin1_General_CS_AS, 'e');
	SET @texto = REPLACE(@texto, 'ë' COLLATE Latin1_General_CS_AS, 'e');
	SET @texto = REPLACE(@texto, 'ì' COLLATE Latin1_General_CS_AS, 'i');
	SET @texto = REPLACE(@texto, 'í' COLLATE Latin1_General_CS_AS, 'i');
	SET @texto = REPLACE(@texto, 'î' COLLATE Latin1_General_CS_AS, 'i');
	SET @texto = REPLACE(@texto, 'ï' COLLATE Latin1_General_CS_AS, 'i');
	SET @texto = REPLACE(@texto, 'ò' COLLATE Latin1_General_CS_AS, 'o');
	SET @texto = REPLACE(@texto, 'ó' COLLATE Latin1_General_CS_AS, 'o');
	SET @texto = REPLACE(@texto, 'õ' COLLATE Latin1_General_CS_AS, 'o');
	SET @texto = REPLACE(@texto, 'ô' COLLATE Latin1_General_CS_AS, 'o');
	SET @texto = REPLACE(@texto, 'ö' COLLATE Latin1_General_CS_AS, 'o');
	SET @texto = REPLACE(@texto, 'ù' COLLATE Latin1_General_CS_AS, 'u');
	SET @texto = REPLACE(@texto, 'ú' COLLATE Latin1_General_CS_AS, 'u');
	SET @texto = REPLACE(@texto, 'û' COLLATE Latin1_General_CS_AS, 'u');
	SET @texto = REPLACE(@texto, 'ü' COLLATE Latin1_General_CS_AS, 'u');
	SET @texto = REPLACE(@texto, 'ç' COLLATE Latin1_General_CS_AS, 'c');

	-- correct chars
	SET @texto = REPLACE(@texto, '\', '/');
	SET @texto = REPLACE(@texto, ',', '.');
	SET @texto = REPLACE(@texto, 'º', ' ');
	SET @texto = REPLACE(@texto, 'ª', ' ');

	-- clean bad chars from string
	SET @texto = [dbo].[CleanBadCharsFunction](@texto);

	-- return text cleaned
	RETURN @texto;
END
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
		VALUES ('PT.23', 'IVA PT Continental - Taxa Normal de 23%', 'IVA PT.Continental 23%', 23.00, CAST('false' AS bit), GETDATE(), GETDATE())
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
				('CIDM0', 'Cotação Incompleta [Estado] (mapeamento incompleto, preenchimento manual necessário)', 6, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CIDM1', 'Cotação Incompleta [Estado] (mapeamento inativo)', 2, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
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



/* ESTADOS */
IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Estados])
BEGIN	
	INSERT INTO [dbo].[Estados] ([Codigo], [Descricao], [Inativo], [Criacao], [Versao])
		VALUES ('NOVO', 'Produto novo (nunca usado)', CAST('false' AS bit), GETDATE(), GETDATE()),
			   ('PROMO', 'Produto em promoção', CAST('false' AS bit), GETDATE(), GETDATE()),
			   ('USADO', 'Produto usado', CAST('false' AS bit), GETDATE(), GETDATE()),
			   ('OPORT', 'Oportunidade', CAST('false' AS bit), GETDATE(), GETDATE()),
			   ('GOPOR', 'Grande oportunidade', CAST('false' AS bit), GETDATE(), GETDATE())
END
GO

GO
PRINT N'Update complete.';


GO