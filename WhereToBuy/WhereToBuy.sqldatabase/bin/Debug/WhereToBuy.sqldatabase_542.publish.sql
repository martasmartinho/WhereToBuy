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
IF NOT EXISTS(SELECT [Name] FROM [SYS].[DATABASES] WHERE [Name] = 'WhereToBuy')
BEGIN
	CREATE DATABASE WhereToBuy COLLATE LATIN1_GENERAL_CI_AS	-- ATENÇÃO QUE TEM TAMBÉM QUE SE ALTERAR A COLLATION NAS PROPRIEDADES DESTE PROJETO
END		
GO

GO
/*
The column [dbo].[Fornecedores].[IndiceConfianca] on table [dbo].[Fornecedores] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Fornecedores])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_CategoriasMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[CategoriasMatching] DROP CONSTRAINT [FK_CategoriasMatching_Fornecedor];


GO
PRINT N'Dropping [dbo].[FK_ComplementosMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[ComplementosMatching] DROP CONSTRAINT [FK_ComplementosMatching_Fornecedor];


GO
PRINT N'Dropping [dbo].[FK_Cotacoes_Fornecedor]...';


GO
ALTER TABLE [dbo].[Cotacoes] DROP CONSTRAINT [FK_Cotacoes_Fornecedor];


GO
PRINT N'Dropping [dbo].[FK_CotacoesAvisos_Fornecedor]...';


GO
ALTER TABLE [dbo].[CotacoesAvisos] DROP CONSTRAINT [FK_CotacoesAvisos_Fornecedor];


GO
PRINT N'Dropping [dbo].[FK_CotacoesRegras_Fornecedor]...';


GO
ALTER TABLE [dbo].[CotacoesRegras] DROP CONSTRAINT [FK_CotacoesRegras_Fornecedor];


GO
PRINT N'Dropping [dbo].[FK_EstadosMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[EstadosMatching] DROP CONSTRAINT [FK_EstadosMatching_Fornecedor];


GO
PRINT N'Dropping [dbo].[FK_FornecedoresMarcas_Fornecedor]...';


GO
ALTER TABLE [dbo].[FornecedoresMarcas] DROP CONSTRAINT [FK_FornecedoresMarcas_Fornecedor];


GO
PRINT N'Dropping [dbo].[FK_ImpostosMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[ImpostosMatching] DROP CONSTRAINT [FK_ImpostosMatching_Fornecedor];


GO
PRINT N'Dropping [dbo].[FK_MarcasMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[MarcasMatching] DROP CONSTRAINT [FK_MarcasMatching_Fornecedor];


GO
PRINT N'Dropping [dbo].[FK_Produtos_Fornecedor]...';


GO
ALTER TABLE [dbo].[Produtos] DROP CONSTRAINT [FK_Produtos_Fornecedor];


GO
PRINT N'Dropping [dbo].[FK_ProdutosDetalhe_Fornecedor]...';


GO
ALTER TABLE [dbo].[ProdutosDetalhe] DROP CONSTRAINT [FK_ProdutosDetalhe_Fornecedor];


GO
PRINT N'Dropping [dbo].[FK_ProdutosMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[ProdutosMatching] DROP CONSTRAINT [FK_ProdutosMatching_Fornecedor];


GO
PRINT N'Dropping [dbo].[FK_StocksMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[StocksMatching] DROP CONSTRAINT [FK_StocksMatching_Fornecedor];


GO
PRINT N'Starting rebuilding table [dbo].[Fornecedores]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Fornecedores] (
    [Codigo]                               NVARCHAR (5)   NOT NULL,
    [Nome]                                 NVARCHAR (50)  NOT NULL,
    [Morada]                               NVARCHAR (200) NOT NULL,
    [CodigoPostal]                         NVARCHAR (15)  NOT NULL,
    [LocalidadePostal]                     NVARCHAR (50)  NOT NULL,
    [Contribuinte]                         NVARCHAR (20)  NOT NULL,
    [Vendedor]                             NVARCHAR (50)  NULL,
    [Telefone]                             NVARCHAR (20)  NULL,
    [Telemovel]                            NVARCHAR (15)  NOT NULL,
    [SMS]                                  NVARCHAR (15)  NOT NULL,
    [Email]                                NVARCHAR (100) NOT NULL,
    [AcessoOnlineAtivo]                    BIT            NOT NULL,
    [Username]                             NVARCHAR (20)  NOT NULL,
    [Password]                             NVARCHAR (20)  NOT NULL,
    [HorasValidadeSugestao]                SMALLINT       NOT NULL,
    [ProdutosMatchingAutomatico]           BIT            NOT NULL,
    [ProdutosCriacaoAutomatica]            BIT            NOT NULL,
    [DisponibilizaInfoProdutoDetalhe]      BIT            NOT NULL,
    [DescricaoPontuacaoInicial]            SMALLINT       NOT NULL,
    [CaracteristicasPontuacaoInicial]      SMALLINT       NOT NULL,
    [LinkPontuacaoInicial]                 SMALLINT       NOT NULL,
    [ImagemPontuacaoInicial]               SMALLINT       NOT NULL,
    [DescricaoSugereInativo]               BIT            NOT NULL,
    [CaracteristicasSugereInativo]         BIT            NOT NULL,
    [LinkSugereInativo]                    BIT            NOT NULL,
    [ImagemSugereInativo]                  BIT            NOT NULL,
    [AtualizacaoAutomaticaInativaSugestao] BIT            NOT NULL,
    [ProdutosConfiancaPreco]               FLOAT (53)     NOT NULL,
    [ProdutosConfiancaDisponibilidade]     FLOAT (53)     NOT NULL,
    [IndiceConfianca]                      FLOAT (53)     NOT NULL,
    [Inativo]                              BIT            NOT NULL,
    [Criacao]                              SMALLDATETIME  NOT NULL,
    [Versao]                               DATETIME       NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Fornecedores] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Fornecedores])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_Fornecedores] ([Codigo], [Nome], [Morada], [CodigoPostal], [LocalidadePostal], [Contribuinte], [Vendedor], [Telefone], [Telemovel], [SMS], [Email], [AcessoOnlineAtivo], [Username], [Password], [HorasValidadeSugestao], [ProdutosMatchingAutomatico], [ProdutosCriacaoAutomatica], [DisponibilizaInfoProdutoDetalhe], [DescricaoPontuacaoInicial], [CaracteristicasPontuacaoInicial], [LinkPontuacaoInicial], [ImagemPontuacaoInicial], [DescricaoSugereInativo], [CaracteristicasSugereInativo], [LinkSugereInativo], [ImagemSugereInativo], [AtualizacaoAutomaticaInativaSugestao], [ProdutosConfiancaPreco], [ProdutosConfiancaDisponibilidade], [Inativo], [Criacao], [Versao])
        SELECT   [Codigo],
                 [Nome],
                 [Morada],
                 [CodigoPostal],
                 [LocalidadePostal],
                 [Contribuinte],
                 [Vendedor],
                 [Telefone],
                 [Telemovel],
                 [SMS],
                 [Email],
                 [AcessoOnlineAtivo],
                 [Username],
                 [Password],
                 [HorasValidadeSugestao],
                 [ProdutosMatchingAutomatico],
                 [ProdutosCriacaoAutomatica],
                 [DisponibilizaInfoProdutoDetalhe],
                 [DescricaoPontuacaoInicial],
                 [CaracteristicasPontuacaoInicial],
                 [LinkPontuacaoInicial],
                 [ImagemPontuacaoInicial],
                 [DescricaoSugereInativo],
                 [CaracteristicasSugereInativo],
                 [LinkSugereInativo],
                 [ImagemSugereInativo],
                 [AtualizacaoAutomaticaInativaSugestao],
                 [ProdutosConfiancaPreco],
                 [ProdutosConfiancaDisponibilidade],
                 [Inativo],
                 [Criacao],
                 [Versao]
        FROM     [dbo].[Fornecedores]
        ORDER BY [Codigo] ASC;
    END

DROP TABLE [dbo].[Fornecedores];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Fornecedores]', N'Fornecedores';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_Fornecedores]', N'PK_Fornecedores', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[FK_CategoriasMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[CategoriasMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_CategoriasMatching_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_ComplementosMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[ComplementosMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_ComplementosMatching_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Cotacoes_Fornecedor]...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_CotacoesAvisos_Fornecedor]...';


GO
ALTER TABLE [dbo].[CotacoesAvisos] WITH NOCHECK
    ADD CONSTRAINT [FK_CotacoesAvisos_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_CotacoesRegras_Fornecedor]...';


GO
ALTER TABLE [dbo].[CotacoesRegras] WITH NOCHECK
    ADD CONSTRAINT [FK_CotacoesRegras_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_EstadosMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[EstadosMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_EstadosMatching_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_FornecedoresMarcas_Fornecedor]...';


GO
ALTER TABLE [dbo].[FornecedoresMarcas] WITH NOCHECK
    ADD CONSTRAINT [FK_FornecedoresMarcas_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_ImpostosMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[ImpostosMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_ImpostosMatching_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_MarcasMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[MarcasMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_MarcasMatching_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Produtos_Fornecedor]...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_ProdutosDetalhe_Fornecedor]...';


GO
ALTER TABLE [dbo].[ProdutosDetalhe] WITH NOCHECK
    ADD CONSTRAINT [FK_ProdutosDetalhe_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_ProdutosMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[ProdutosMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_ProdutosMatching_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_StocksMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[StocksMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_StocksMatching_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Refreshing [dbo].[CotacoesMatrizCriacaoProdutosView]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[CotacoesMatrizCriacaoProdutosView]';


GO
PRINT N'Refreshing [dbo].[CotacoesMatrizPreenchimentoMapToView]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[CotacoesMatrizPreenchimentoMapToView]';


GO
PRINT N'Refreshing [dbo].[CotacoesVencedorasView]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[CotacoesVencedorasView]';


GO
PRINT N'Refreshing [dbo].[ProdutosAtuaisView]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ProdutosAtuaisView]';


GO
PRINT N'Refreshing [dbo].[ProdutosView]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[ProdutosView]';


GO
PRINT N'Altering [dbo].[SupplierInsert]...';


GO
ALTER PROCEDURE [dbo].[SupplierInsert]
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
	@IndiceConfianca float, 
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
											[ProdutosConfiancaDisponibilidade], [IndiceConfianca], [Inativo], [Criacao], [Versao])
 
					VALUES (@Codigo, @Nome, @Morada, @CodigoPostal, @LocalidadePostal, @Contribuinte, 
											@Vendedor, @Telefone, @Telemovel, @SMS, @Email, @AcessoOnlineAtivo, 
											@Username, @Password, @HorasValidadeSugestao, @ProdutosMatchingAutomatico, 
											@ProdutosCriacaoAutomatica, @DisponibilizaInfoProdutoDetalhe, 
											@DescricaoPontuacaoInicial, @CaracteristicasPontuacaoInicial, 
											@LinkPontuacaoInicial, @ImagemPontuacaoInicial, @DescricaoSugereInativo, 
											@CaracteristicasSugereInativo, @LinkSugereInativo, @ImagemSugereInativo, 
											@AtualizacaoAutomaticaInativaSugestao, @ProdutosConfiancaPreco, 
											@ProdutosConfiancaDisponibilidade, @IndiceConfianca, @Inativo, @Criacao, @Versao);

		
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
		SET @Erro = '[WhereToBuy-I10] ' + ERROR_MESSAGE();
		RAISERROR (@Erro, 15, 1)
		RETURN 0

	END CATCH
GO
PRINT N'Altering [dbo].[SupplierUpdate]...';


GO
ALTER PROCEDURE [dbo].[SupplierUpdate]
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
	@IndiceConfianca float, 
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
										[IndiceConfianca]=@IndiceConfianca, 
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
PRINT N'Altering [dbo].[ProductSelect]...';


GO
ALTER PROCEDURE [dbo].[ProductSelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2500), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT p.[Codigo], p.[Descricao], p.[Partnumber], 
							p.[CategoriaCodigo], c.[Descricao] as CategoriaDescricao,
							p.[MarcaCodigo], m.[Descricao] as MarcaDescricao,
							p.[ImpostoCodigo], i.[Descricao] as ImpostoDescricao, i.[DesignacaoFiscal] as DesignacaoFiscal,
							p.[FornecedorCodigo], f.[Nome] as FonecedorNome, COALESCE(f.[ProdutosConfiancaPreco],0) as ProdutosConfiancaPreco, COALESCE(f.[ProdutosConfiancaDisponibilidade],0) as ProdutosConfiancaDisponibilidade, ICDFormula,
							pd.[AtualizacaoManualNecessaria] as AtualizacaoAutomatica,  COALESCE(pd.[IndicePreocupacaoConteudo], 0) as IPC,
							ICP, ICPFormula, ICPCEFormula, ICPCTFormula, ICPCFFormula,
							EEP, EEPFormula,
							EED, EEDFormula,
							ICD, ICDFormula, ICDCEFormula, ICDCTFormula, ICDCFFormula,							
							COALESCE(p.[PrecoCusto], 0) as PrecoCusto, p.[PrecoCusto_Data],
							COALESCE(p.[PrecoCusto_U1], 0) as PrecoCusto_U1, p.[PrecoCusto_U1Data], 
							COALESCE(p.[PrecoCusto_U2], 0) as PrecoCusto_U2, p.[PrecoCusto_U2Data], 
							COALESCE(p.[PrecoCusto_U3], 0) as PrecoCusto_U3, p.[PrecoCusto_U3Data], 
							p.[StockCodigo], s.[Descricao] as StockDescricao,p.[StockCodigo_Data], 
							p.[StockCodigo_U1], sU1.[Descricao] as StockDescricao_U1, p.[StockCodigo_U1Data], 
							p.[StockCodigo_U2], sU2.[Descricao] as StockDescricao_U2, p.[StockCodigo_U2Data], 
							p.[StockCodigo_U3], sU3.[Descricao] as StockDescricao_U3,p.[StockCodigo_U3Data], 
							p.[Descontinuado], p.[Inativo], 
							p.[Criacao], p.[Versao]
						FROM [dbo].[ProdutosView] p
						INNER JOIN  [dbo].[Fornecedores] f
							ON f.[Codigo] = p.[FornecedorCodigo]
						LEFT JOIN  [dbo].[Categorias] c
							ON c.[Codigo] = p.[CategoriaCodigo]
						LEFT JOIN  [dbo].[Marcas] m
							ON m.[Codigo] = p.[MarcaCodigo]
						LEFT JOIN  [dbo].[Impostos] i
							ON i.[Codigo] = p.[ImpostoCodigo]
						LEFT JOIN  [dbo].[ProdutosDetalhe] pd
							ON pd.[ProdutoCodigo] = p.[Codigo] AND pd.[FornecedorCodigo] = p.[FornecedorCodigo]
						LEFT JOIN  [dbo].[Stocks] s
							ON s.[Codigo] = p.[StockCodigo]
						LEFT JOIN  [dbo].[Stocks] sU1
							ON sU1.[Codigo] = p.[StockCodigo_U1]
						LEFT JOIN  [dbo].[Stocks] sU2
							ON sU2.[Codigo] = p.[StockCodigo_U2]
						LEFT JOIN  [dbo].[Stocks] sU3
							ON sU3.[Codigo] = p.[StockCodigo_U3]'
							 
	

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
PRINT N'Refreshing [dbo].[Step4c3_Finishing_ProductsDetailInsert]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Step4c3_Finishing_ProductsDetailInsert]';


GO
PRINT N'Refreshing [dbo].[Step2b1z_QuotationRules_FeedNewRules]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Step2b1z_QuotationRules_FeedNewRules]';


GO
PRINT N'Refreshing [dbo].[Step2c4_MappingProducts_Map]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Step2c4_MappingProducts_Map]';


GO
PRINT N'Refreshing [dbo].[Step2c5_MappingProducts_Notifications1]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Step2c5_MappingProducts_Notifications1]';


GO
PRINT N'Refreshing [dbo].[Step3a1_Preparation_ProductMatchingsReset]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Step3a1_Preparation_ProductMatchingsReset]';


GO
PRINT N'Refreshing [dbo].[Step3a2_Preparation_QuotationRulesReset]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Step3a2_Preparation_QuotationRulesReset]';


GO
PRINT N'Refreshing [dbo].[SupplierDelete]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[SupplierDelete]';


GO
PRINT N'Refreshing [dbo].[Step2c2_MappingProducts_InsertNewProducts]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Step2c2_MappingProducts_InsertNewProducts]';


GO
PRINT N'Refreshing [dbo].[Step2c3_MappingProducts_FillMapTo]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Step2c3_MappingProducts_FillMapTo]';


GO
PRINT N'Refreshing [dbo].[Step4c1_Finishing_ProductsUpdate]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Step4c1_Finishing_ProductsUpdate]';


GO
PRINT N'Refreshing [dbo].[Step4a4_Finishing_ProductDescontinuation]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Step4a4_Finishing_ProductDescontinuation]';


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
				[AtualizacaoAutomaticaInativaSugestao], [ProdutosConfiancaPreco], [ProdutosConfiancaDisponibilidade], [IndiceConfianca],
				[Inativo], [Criacao], [Versao])
		 VALUES
			   ('00000', 'DFL - Default', 'Default', '0000-000', 'Default', '000000000',
				'Default', '000000000', '000000000', '000000000', 'default@default.pt', CAST('false' AS bit), 'dfl001', 'dflpass',
				24, CAST('true' AS bit), CAST('true' AS bit), CAST('true' AS bit),
				5, 5, 5, 5,
				CAST('false' AS bit), CAST('false' AS bit), CAST('false' AS bit), CAST('false' AS bit),
				CAST('false' AS bit), 0, 0, 0,
				CAST('false' AS bit), GETDATE(), GETDATE()),
				('00024', 'CPC - Companhia Portuguesa de Computadores..', 'Rua Monte dos Pipos, 649', '4460-059', 'Guifões', '999999990',
				'Susana', '222333444', '939949959', '969979989', 'susana@cpcdi.pt', CAST('false' AS bit), 'cpc001', 'cpcpass',
				24, CAST('true' AS bit), CAST('true' AS bit), CAST('true' AS bit),
				5, 5, 5, 5,
				CAST('false' AS bit), CAST('false' AS bit), CAST('false' AS bit), CAST('false' AS bit),
				CAST('false' AS bit), 90, 90, 90,
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
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[CategoriasMatching] WITH CHECK CHECK CONSTRAINT [FK_CategoriasMatching_Fornecedor];

ALTER TABLE [dbo].[ComplementosMatching] WITH CHECK CHECK CONSTRAINT [FK_ComplementosMatching_Fornecedor];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Fornecedor];

ALTER TABLE [dbo].[CotacoesAvisos] WITH CHECK CHECK CONSTRAINT [FK_CotacoesAvisos_Fornecedor];

ALTER TABLE [dbo].[CotacoesRegras] WITH CHECK CHECK CONSTRAINT [FK_CotacoesRegras_Fornecedor];

ALTER TABLE [dbo].[EstadosMatching] WITH CHECK CHECK CONSTRAINT [FK_EstadosMatching_Fornecedor];

ALTER TABLE [dbo].[FornecedoresMarcas] WITH CHECK CHECK CONSTRAINT [FK_FornecedoresMarcas_Fornecedor];

ALTER TABLE [dbo].[ImpostosMatching] WITH CHECK CHECK CONSTRAINT [FK_ImpostosMatching_Fornecedor];

ALTER TABLE [dbo].[MarcasMatching] WITH CHECK CHECK CONSTRAINT [FK_MarcasMatching_Fornecedor];

ALTER TABLE [dbo].[Produtos] WITH CHECK CHECK CONSTRAINT [FK_Produtos_Fornecedor];

ALTER TABLE [dbo].[ProdutosDetalhe] WITH CHECK CHECK CONSTRAINT [FK_ProdutosDetalhe_Fornecedor];

ALTER TABLE [dbo].[ProdutosMatching] WITH CHECK CHECK CONSTRAINT [FK_ProdutosMatching_Fornecedor];

ALTER TABLE [dbo].[StocksMatching] WITH CHECK CHECK CONSTRAINT [FK_StocksMatching_Fornecedor];


GO
PRINT N'Update complete.';


GO
