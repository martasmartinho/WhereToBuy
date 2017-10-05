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
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\"

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
PRINT N'Creating [dbo].[Idiomas]...';


GO
CREATE TABLE [dbo].[Idiomas] (
    [Codigo]    NVARCHAR (5)  NOT NULL,
    [Descricao] NVARCHAR (50) NOT NULL,
    [Inativo]   BIT           NOT NULL,
    [Criacao]   SMALLDATETIME NOT NULL,
    [Versao]    DATETIME      NOT NULL,
    CONSTRAINT [PK_Idiomas] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Altering [dbo].[QuotationWarningSelect]...';


GO
ALTER PROCEDURE [dbo].[QuotationWarningSelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2048), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT c.[Id], c.[Data], c.[_ProdutoCodigo], 
							c.[_ComplementoCodigo], c.[FornecedorCodigo], f.[Nome], f.[Contribuinte]
							c.[AvisoTipoCodigo], c.[Descricao], c.[Criacao]				
						FROM [dbo].[CotacoesAvisos] c INNER JOIN [dbo].[Fornacedores] f
							ON c.[FornecedorCodigo] = f.[Codigo] and f[Inativo]=FALSE' 
	

	SET @where = LTRIM(RTRIM(@WhereClause))
	IF Len(@where) > 0
	BEGIN
		SET @where = ' WHERE ' + @where
	END


	SET @orderBy = LTRIM(RTRIM(@OrderByClause))
	IF LEN(@orderBy) > 0
	BEGIN
		SET @orderBy = ' ORDER BY ' + @orderBy
	END


	SET @sqlQuery = @select + @where + @orderBy  


	EXEC(@sqlQuery)


RETURN @@ROWCOUNT
GO
PRINT N'Creating [dbo].[LanguageCount]...';


GO
CREATE PROCEDURE [dbo].[LanguageCount]
	@WhereClause nvarchar(512) = ''			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
AS

	DECLARE @select nvarchar(1024), @where nvarchar(512), @sqlQuery nvarchar(2048)

	SET @select = 'SELECT Count([Codigo]) FROM [dbo].[Idiomas]' 

	SET @where = LTRIM(RTRIM(@WhereClause))
	IF Len(@where) > 0
	BEGIN
		SET @where = ' WHERE ' + @where
	END


	SET @sqlQuery = @select + @where


	EXEC(@sqlQuery)

RETURN  -- This SP must be used with ExecuteScalar
GO
PRINT N'Creating [dbo].[LanguageDelete]...';


GO
CREATE PROCEDURE [dbo].[LanguageDelete]
	@Codigo nvarchar(5),
	@Versao datetime
AS
	DECLARE @Erro nvarchar(255)


	-- VALIDAR SE O CODIGO EXISTE
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Idiomas] WHERE [Codigo] = @Codigo)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-D01] ' + 'O registo que pretende eliminar não existe!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	-- VALIDAR SE A VERSÃO AINDA É A MESMA
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Idiomas] WHERE [Codigo] = @Codigo AND [Versao] = @Versao)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-D02] ' + 'O registo que pretende eliminar, já foi alterado desde que o abriu!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END


	-- TUDO OK PARA SEGUIR EM FRENTE
	DECLARE @NumeroLinhasAfetadas int


	-- ELIMINAR O REGISTO
	BEGIN TRY

		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF
		 
		-- ELIMINAR REGISTO
		DELETE FROM [dbo].[Idiomas] Where [Codigo] = @Codigo

		SET @NumeroLinhasAfetadas = @@ROWCOUNT -- GUARDAR O NUMERO DE LINHAS AFETADAS

		-- ACABA CONTAGEM DE @@ROWCOUNT
		SET NOCOUNT ON

		-- RETORNAR O NUMERO DE LINHAS AFETADAS
		-- Quando usado o ExecuteNonQuery é o @@ROWCOUNT que é lido (SET NOCOUNT OFF/ON), e não esta variável
		RETURN @NumeroLinhasAfetadas

	END TRY


	-- SE EXISTIRAM ERROS
	BEGIN CATCH
		
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-D10] ' + ERROR_MESSAGE();
		RAISERROR (@Erro, 15, 1)
		RETURN 0

	END CATCH
GO
PRINT N'Creating [dbo].[LanguageInsert]...';


GO
CREATE PROCEDURE [dbo].[LanguageInsert]
	@Codigo nvarchar(5),
	@Descricao nvarchar(50),
	@Inativo bit
AS
	
	DECLARE @Criacao smalldatetime, @Versao datetime, @NumeroLinhasAfetadas int, @Erro nvarchar(255)

	SET @Criacao = GETDATE()
	SET @Versao = GETDATE()


	-- INSERÇÃO DO REGISTO
	BEGIN TRY
		 
		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF

		-- INSERIR REGISTO
		INSERT INTO [dbo].[Idiomas] ([Codigo], [Descricao], [Inativo], [Criacao], [Versao]) 
					VALUES (@Codigo, @Descricao, @Inativo, @Criacao, @Versao);
		
		SET @NumeroLinhasAfetadas = @@ROWCOUNT -- GUARDAR O NUMERO DE LINHAS AFETADAS

		-- ACABA CONTAGEM DE @@ROWCOUNT
		SET NOCOUNT ON

		-- RETORNAR O NUMERO DE LINHAS AFETADAS
		-- Quando usado o ExecuteNonQuery é o @@ROWCOUNT que é lido (SET NOCOUNT OFF/ON),  e não esta variável
		RETURN @NumeroLinhasAfetadas

	END TRY


	-- SE EXISTIRAM ERROS, DESFAZER TRANSACÇÃO
	BEGIN CATCH

		-- DISPARAR UM ERRO
		SET @Erro = 'WhereToBuy-I10] ' + ERROR_MESSAGE();
		RAISERROR (@Erro, 15, 1)
		RETURN 0

	END CATCH
GO
PRINT N'Creating [dbo].[LanguageSelect]...';


GO
CREATE PROCEDURE [dbo].[LanguageSelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2048), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT i.[Codigo], i.[Descricao], i.[Inativo], i.[Criacao], i.[Versao]				
						FROM [dbo].[Idiomas] i' 
	

	SET @where = LTRIM(RTRIM(@WhereClause))
	IF Len(@where) > 0
	BEGIN
		SET @where = ' WHERE ' + @where
	END


	SET @orderBy = LTRIM(RTRIM(@OrderByClause))
	IF LEN(@orderBy) > 0
	BEGIN
		SET @orderBy = ' ORDER BY ' + @orderBy
	END


	SET @sqlQuery = @select + @where + @orderBy  


	EXEC(@sqlQuery)


RETURN @@ROWCOUNT
GO
PRINT N'Creating [dbo].[LanguageUpdate]...';


GO
CREATE PROCEDURE [dbo].[LanguageUpdate]
	@Codigo nvarchar(5),
	@Descricao nvarchar(50),
	@Inativo bit,
	@Versao datetime
AS

	DECLARE @Erro nvarchar(255)


	-- VALIDAR SE O REGISTO EXISTE
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Idiomas] WHERE [Codigo] = @Codigo)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U01] ' + 'O registo que pretende alterar não existe!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	-- VALIDAR SE A VERSÃO AINDA É A MESMA
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Idiomas] WHERE [Codigo] = @Codigo AND [Versao] = @Versao)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U02] ' + 'O registo que pretende alterar, já foi alterado desde que o abriu!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END


	-- TUDO OK PARA SEGUIR EM FRENTE
	DECLARE @NovaVersao datetime, @NumeroLinhasAfetadas int

	SET @NovaVersao = GETDATE()


	-- ALTERAÇÃO DO REGISTO
	BEGIN TRY

		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF

		-- ALTERAR REGISTO
		UPDATE [dbo].[Idiomas] SET [Descricao] = @Descricao,
									[Versao] = @NovaVersao
								WHERE [Codigo] = @Codigo

		SET @NumeroLinhasAfetadas = @@ROWCOUNT -- GUARDAR O NUMERO DE LINHAS AFETADAS

		-- ACABA CONTAGEM DE @@ROWCOUNT
		SET NOCOUNT ON

		-- RETORNAR O NUMERO DE LINHAS AFETADAS
		-- Quando usado o ExecuteNonQuery é o @@ROWCOUNT que é lido (SET NOCOUNT OFF/ON), e não esta variável
		RETURN @NumeroLinhasAfetadas

	END TRY


	-- SE EXISTIRAM ERROS
	BEGIN CATCH

		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U10] ' + ERROR_MESSAGE();
		RAISERROR (@Erro, 15, 1)
		RETURN 0

	END CATCH
GO
PRINT N'Creating [dbo].[SupplierCount]...';


GO
CREATE PROCEDURE [dbo].[SupplierCount]
	@WhereClause nvarchar(512) = ''			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
AS

	DECLARE @select nvarchar(1024), @where nvarchar(512), @sqlQuery nvarchar(2048)

	SET @select = 'SELECT Count([Codigo]) FROM [dbo].[Fornecedores]' 

	SET @where = LTRIM(RTRIM(@WhereClause))
	IF Len(@where) > 0
	BEGIN
		SET @where = ' WHERE ' + @where
	END


	SET @sqlQuery = @select + @where


	EXEC(@sqlQuery)

RETURN  -- This SP must be used with ExecuteScalar
GO
PRINT N'Creating [dbo].[SupplierDelete]...';


GO
CREATE PROCEDURE [dbo].[SupplierDelete]
	@Codigo nvarchar(5),
	@Versao datetime
AS
	DECLARE @Erro nvarchar(255)


	-- VALIDAR SE O CODIGO EXISTE
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Fornecedores] WHERE [Codigo] = @Codigo)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-D01] ' + 'O registo que pretende eliminar não existe!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	-- VALIDAR SE A VERSÃO AINDA É A MESMA
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Fornecedores] WHERE [Codigo] = @Codigo AND [Versao] = @Versao)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-D02] ' + 'O registo que pretende eliminar, já foi alterado desde que o abriu!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END


	-- TUDO OK PARA SEGUIR EM FRENTE
	DECLARE @NumeroLinhasAfetadas int


	-- ELIMINAR O REGISTO
	BEGIN TRY

		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF
		 
		-- ELIMINAR REGISTO
		DELETE FROM [dbo].[Fornecedores] Where [Codigo] = @Codigo

		SET @NumeroLinhasAfetadas = @@ROWCOUNT -- GUARDAR O NUMERO DE LINHAS AFETADAS

		-- ACABA CONTAGEM DE @@ROWCOUNT
		SET NOCOUNT ON

		-- RETORNAR O NUMERO DE LINHAS AFETADAS
		-- Quando usado o ExecuteNonQuery é o @@ROWCOUNT que é lido (SET NOCOUNT OFF/ON), e não esta variável
		RETURN @NumeroLinhasAfetadas

	END TRY


	-- SE EXISTIRAM ERROS
	BEGIN CATCH
		
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-D10] ' + ERROR_MESSAGE();
		RAISERROR (@Erro, 15, 1)
		RETURN 0

	END CATCH
GO
PRINT N'Creating [dbo].[SupplierInsert]...';


GO
CREATE PROCEDURE [dbo].[SupplierInsert]
	@Codigo nvarchar(5), 
	@Nome nvarchar(50), 
	@Morada nvarchar(200), 
	@CodigoPostal nvarchar(15),
	@LocalidadePostal nvarchar(50), 
	@Contribuinte nvarchar(20), 
	@Vendedor nvarchar(50), 
	@Telefone nvarchar(20), 
	@Telemovel nvarchar(15), 
	@SMS nvarchar(15), 
	@Email nvarchar(100), 
	@AcessoOnlineAtivo bit, 
	@Username nvarchar(20), 
	@Password nvarchar(20), 
	@HorasValidadeSugestao smallint, 
	@ProdutosMatchingAutomatico bit, 
	@ProdutosCriacaoAutomatica bit, 
	@DisponibilizaInfoProdutoDetalhe bit, 
	@DescricaoPontuacaoInicial smallint, 
	@CaracteristicasPontuacaoInicial smallint, 
	@LinkPontuacaoInicial smallint, 
	@ImagemPontuacaoInicial smallint, 
	@DescricaoSugereInativo bit, 
	@CaracteristicasSugereInativo bit, 
	@LinkSugereInativo bit, 
	@ImagemSugereInativo bit, 
	@AtualizacaoAutomaticaInativaSugestao bit, 
	@ProdutosConfiancaPreco float, 
	@ProdutosConfiancaDisponibilidade float, 
	@Inativo bit 

AS
	
	DECLARE @Criacao smalldatetime, @Versao datetime, @NumeroLinhasAfetadas int, @Erro nvarchar(255)

	SET @Criacao = GETDATE()
	SET @Versao = GETDATE()


	-- INSERÇÃO DO REGISTO
	BEGIN TRY
		 
		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF

		-- INSERIR REGISTO
		INSERT INTO [dbo].[Fornecedores] ([Codigo], [Nome], [Morada], [CodigoPostal], [LocalidadePostal], [Contribuinte], 
											[Vendedor], [Telefone], [Telemovel], [SMS], [Email], [AcessoOnlineAtivo], 
											[Username], [Password], [HorasValidadeSugestao], [ProdutosMatchingAutomatico], 
											[ProdutosCriacaoAutomatica], [DisponibilizaInfoProdutoDetalhe], 
											[DescricaoPontuacaoInicial], [CaracteristicasPontuacaoInicial], 
											[LinkPontuacaoInicial], [ImagemPontuacaoInicial], [DescricaoSugereInativo], 
											[CaracteristicasSugereInativo], [LinkSugereInativo], [ImagemSugereInativo], 
											[AtualizacaoAutomaticaInativaSugestao], [ProdutosConfiancaPreco], 
											[ProdutosConfiancaDisponibilidade], [Inativo], [Criacao], [Versao])
 
					VALUES (@Codigo, @Nome, @Morada, @CodigoPostal, @LocalidadePostal, @Contribuinte, 
											@Vendedor, @Telefone, @Telemovel, @SMS, @Email, @AcessoOnlineAtivo, 
											@Username, @Password, @HorasValidadeSugestao, @ProdutosMatchingAutomatico, 
											@ProdutosCriacaoAutomatica, @DisponibilizaInfoProdutoDetalhe, 
											@DescricaoPontuacaoInicial, @CaracteristicasPontuacaoInicial, 
											@LinkPontuacaoInicial, @ImagemPontuacaoInicial, @DescricaoSugereInativo, 
											@CaracteristicasSugereInativo, @LinkSugereInativo, @ImagemSugereInativo, 
											@AtualizacaoAutomaticaInativaSugestao, @ProdutosConfiancaPreco, 
											@ProdutosConfiancaDisponibilidade, @Inativo, @Criacao, @Versao);

		
		SET @NumeroLinhasAfetadas = @@ROWCOUNT -- GUARDAR O NUMERO DE LINHAS AFETADAS

		-- ACABA CONTAGEM DE @@ROWCOUNT
		SET NOCOUNT ON

		-- RETORNAR O NUMERO DE LINHAS AFETADAS
		-- Quando usado o ExecuteNonQuery é o @@ROWCOUNT que é lido (SET NOCOUNT OFF/ON),  e não esta variável
		RETURN @NumeroLinhasAfetadas

	END TRY


	-- SE EXISTIRAM ERROS, DESFAZER TRANSACÇÃO
	BEGIN CATCH

		-- DISPARAR UM ERRO
		SET @Erro = 'WhereToBuy-I10] ' + ERROR_MESSAGE();
		RAISERROR (@Erro, 15, 1)
		RETURN 0

	END CATCH
GO
PRINT N'Creating [dbo].[SupplierSelect]...';


GO
CREATE PROCEDURE [dbo].[SupplierSelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2048), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT fornecedores.[Codigo], fornecedores.[Nome], fornecedores.[Morada], 
							fornecedores.[CodigoPostal], fornecedores.[LocalidadePostal], 
							fornecedores.[Contribuinte], fornecedores.[Vendedor], 
							fornecedores.[Telefone], fornecedores.[Telemovel], 
							fornecedores.[SMS], fornecedores.[Email], 
							fornecedores.[AcessoOnlineAtivo], 
							fornecedores.[Username], fornecedores.[Password], 
							fornecedores.[HorasValidadeSugestao], 
							fornecedores.[ProdutosMatchingAutomatico], 
							fornecedores.[ProdutosCriacaoAutomatica], 
							fornecedores.[DisponibilizaInfoProdutoDetalhe], 
							fornecedores.[DescricaoPontuacaoInicial], 
							fornecedores.[CaracteristicasPontuacaoInicial], 
							fornecedores.[LinkPontuacaoInicial], 
							fornecedores.[ImagemPontuacaoInicial], 
							fornecedores.[DescricaoSugereInativo], 
							fornecedores.[CaracteristicasSugereInativo], 
							fornecedores.[LinkSugereInativo], 
							fornecedores.[ImagemSugereInativo], 
							fornecedores.[AtualizacaoAutomaticaInativaSugestao], 
							fornecedores.[ProdutosConfiancaPreco], 
							fornecedores.[ProdutosConfiancaDisponibilidade], 
							fornecedores.[Inativo], fornecedores.[Criacao], 
							fornecedores.[Versao] 
						FROM [dbo].[Fornecedores] fornecedores' 
	

	SET @where = LTRIM(RTRIM(@WhereClause))
	IF Len(@where) > 0
	BEGIN
		SET @where = ' WHERE ' + @where
	END


	SET @orderBy = LTRIM(RTRIM(@OrderByClause))
	IF LEN(@orderBy) > 0
	BEGIN
		SET @orderBy = ' ORDER BY ' + @orderBy
	END


	SET @sqlQuery = @select + @where + @orderBy  


	EXEC(@sqlQuery)


RETURN @@ROWCOUNT
GO
PRINT N'Creating [dbo].[SupplierUpdate]...';


GO
CREATE PROCEDURE [dbo].[SupplierUpdate]
	@Codigo nvarchar(5), 
	@Nome nvarchar(50), 
	@Morada nvarchar(200), 
	@CodigoPostal nvarchar(15),
	@LocalidadePostal nvarchar(50), 
	@Contribuinte nvarchar(20), 
	@Vendedor nvarchar(50), 
	@Telefone nvarchar(20), 
	@Telemovel nvarchar(15), 
	@SMS nvarchar(15), 
	@Email nvarchar(100), 
	@AcessoOnlineAtivo bit, 
	@Username nvarchar(20), 
	@Password nvarchar(20), 
	@HorasValidadeSugestao smallint, 
	@ProdutosMatchingAutomatico bit, 
	@ProdutosCriacaoAutomatica bit, 
	@DisponibilizaInfoProdutoDetalhe bit, 
	@DescricaoPontuacaoInicial smallint, 
	@CaracteristicasPontuacaoInicial smallint, 
	@LinkPontuacaoInicial smallint, 
	@ImagemPontuacaoInicial smallint, 
	@DescricaoSugereInativo bit, 
	@CaracteristicasSugereInativo bit, 
	@LinkSugereInativo bit, 
	@ImagemSugereInativo bit, 
	@AtualizacaoAutomaticaInativaSugestao bit, 
	@ProdutosConfiancaPreco float, 
	@ProdutosConfiancaDisponibilidade float, 
	@Inativo bit, 
	@Versao datetime 


AS

	DECLARE @Erro nvarchar(255)


	-- VALIDAR SE O REGISTO EXISTE
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Fornecedores] WHERE [Codigo] = @Codigo)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U01] ' + 'O registo que pretende alterar não existe!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	-- VALIDAR SE A VERSÃO AINDA É A MESMA
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Fornecedores] WHERE [Codigo] = @Codigo AND [Versao] = @Versao)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U02] ' + 'O registo que pretende alterar, já foi alterado desde que o abriu!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END


	-- TUDO OK PARA SEGUIR EM FRENTE
	DECLARE @NovaVersao datetime, @NumeroLinhasAfetadas int

	SET @NovaVersao = GETDATE()


	-- ALTERAÇÃO DO REGISTO
	BEGIN TRY

		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF

		-- ALTERAR REGISTO
		UPDATE [dbo].[Fornecedores] SET [Nome]=@Nome, 
										[Morada]=@Morada, 
										[CodigoPostal]=@CodigoPostal, 
										[LocalidadePostal]=@LocalidadePostal, 
										[Contribuinte]=@Contribuinte, 
										[Vendedor]=@Vendedor, 
										[Telefone]=@Telefone, 
										[Telemovel]=@Telemovel, 
										[SMS]=@SMS, 
										[Email]=@Email, 
										[AcessoOnlineAtivo]=@AcessoOnlineAtivo, 
										[Username]=@Username, 
										[Password]=@Password, 
										[HorasValidadeSugestao]=@HorasValidadeSugestao, 
										[ProdutosMatchingAutomatico]=@ProdutosMatchingAutomatico, 
										[ProdutosCriacaoAutomatica]=@ProdutosCriacaoAutomatica, 
										[DisponibilizaInfoProdutoDetalhe]=@DisponibilizaInfoProdutoDetalhe, 
										[DescricaoPontuacaoInicial]=@DescricaoPontuacaoInicial, 
										[CaracteristicasPontuacaoInicial]=@CaracteristicasPontuacaoInicial, 
										[LinkPontuacaoInicial]=@LinkPontuacaoInicial, 
										[ImagemPontuacaoInicial]=@ImagemPontuacaoInicial, 
										[DescricaoSugereInativo]=@DescricaoSugereInativo, 
										[CaracteristicasSugereInativo]=@CaracteristicasSugereInativo, 
										[LinkSugereInativo]=@LinkSugereInativo, 
										[ImagemSugereInativo]=@ImagemSugereInativo, 
										[AtualizacaoAutomaticaInativaSugestao]=@AtualizacaoAutomaticaInativaSugestao, 
										[ProdutosConfiancaPreco]=@ProdutosConfiancaPreco, 
										[ProdutosConfiancaDisponibilidade]=@ProdutosConfiancaDisponibilidade, 
										[Inativo]=@Inativo,
										[Versao] = @NovaVersao
								WHERE [Codigo] = @Codigo

		SET @NumeroLinhasAfetadas = @@ROWCOUNT -- GUARDAR O NUMERO DE LINHAS AFETADAS

		-- ACABA CONTAGEM DE @@ROWCOUNT
		SET NOCOUNT ON

		-- RETORNAR O NUMERO DE LINHAS AFETADAS
		-- Quando usado o ExecuteNonQuery é o @@ROWCOUNT que é lido (SET NOCOUNT OFF/ON), e não esta variável
		RETURN @NumeroLinhasAfetadas

	END TRY


	-- SE EXISTIRAM ERROS
	BEGIN CATCH

		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U10] ' + ERROR_MESSAGE();
		RAISERROR (@Erro, 15, 1)
		RETURN 0

	END CATCH
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
				('IAPD1', 'Inativação Administrativa (Prevenção Preço Desfazado - Preco fora do intervalo)', 7, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('IAPD2', 'Inativação Administrativa (Prevenção Preço Desfazado - Amplitude do preço excedida)', 7, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				
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
				('CIPM9', 'Cotação Incompleta [Produto] (ambiguidade detetada com outro partnumber de outra marca)', 8, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),

				('CAC01', 'Cotação Atuais, atualização de Preço de Custo (CotacoesRegras, ProdutosMatching)', 4, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CAC02', 'Cotação Atuais, atualização da formula do Preço de Custo', 2, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CAC03', 'Cotação Atuais, atualização de Validade (CotacoesRegras, ProdutosMatching)', 4, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CAC04', 'Cotação Atuais, atualização da formula de calculo da Validade', 2, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CAC05', 'Cotação Atuais, atualização da descrição do produto', 4, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CAC06', 'Cotação Atuais, atualização das caracteristicas do produto', 4, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CAC07', 'Cotação Atuais, atualização do link do produto', 4, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CAC08', 'Cotação Atuais, atualização da imagem do produto', 4, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CAC09', 'Cotação Atuais, atualização do código substituto de stock (CotacoesRegras, ProdutosMatching)', 4, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE()),
				('CAC10', 'Cotação Atuais, atualização da justificação de alteração do código substituto', 2, NULL, NULL, CAST('false' AS bit), GETDATE(), GETDATE())

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




/* TERMOSPREOCUPANTES */
IF NOT EXISTS(SELECT [Termo] FROM [dbo].[TermosPreocupantes])
BEGIN	
	INSERT INTO [dbo].[TermosPreocupantes] ([Termo], [Indice] ,[Notas], [Inativo], [Criacao], [Versao])
		VALUES ('%[ ]saldo[ ]%', 6, '', CAST('false' AS bit), GETDATE(), GETDATE()),
			   ('%[ ]promo%', 6, '', CAST('false' AS bit), GETDATE(), GETDATE()),
			   ('%<a[ ]%', 7, '', CAST('false' AS bit), GETDATE(), GETDATE()),
			   ('%[< |<| ]script[ ]%', 9, '', CAST('false' AS bit), GETDATE(), GETDATE()),
			   ('%[ ]href[ =]%', 7, '', CAST('false' AS bit), GETDATE(), GETDATE()),
			   ('%<br%', 7, '', CAST('false' AS bit), GETDATE(), GETDATE())
			  
END
GO

GO
PRINT N'Update complete.';


GO
