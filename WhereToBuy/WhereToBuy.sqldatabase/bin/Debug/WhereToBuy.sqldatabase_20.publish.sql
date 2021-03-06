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
PRINT N'Creating [dbo].[Produtos]...';


GO
CREATE TABLE [dbo].[Produtos] (
    [Codigo]             NVARCHAR (40)   NOT NULL,
    [Descricao]          NVARCHAR (256)  NOT NULL,
    [Partnumber]         NVARCHAR (25)   NOT NULL,
    [CategoriaCodigo]    NVARCHAR (20)   NOT NULL,
    [MarcaCodigo]        NVARCHAR (20)   NOT NULL,
    [ImpostoCodigo]      NVARCHAR (20)   NOT NULL,
    [FornecedorCodigo]   NVARCHAR (20)   NOT NULL,
    [PrecoCusto]         DECIMAL (14, 4) NULL,
    [PrecoCusto_Data]    SMALLDATETIME   NULL,
    [PrecoCusto_U1]      DECIMAL (14, 4) NULL,
    [PrecoCusto_U1Data]  SMALLDATETIME   NULL,
    [PrecoCusto_U2]      DECIMAL (14, 4) NULL,
    [PrecoCusto_U2Data]  SMALLDATETIME   NULL,
    [PrecoCusto_U3]      DECIMAL (14, 4) NULL,
    [PrecoCusto_U3Data]  SMALLDATETIME   NULL,
    [StockCodigo]        NVARCHAR (20)   NULL,
    [StockCodigo_Data]   SMALLDATETIME   NULL,
    [StockCodigo_U1]     NVARCHAR (20)   NULL,
    [StockCodigo_U1Data] SMALLDATETIME   NULL,
    [StockCodigo_U2]     NVARCHAR (20)   NULL,
    [StockCodigo_U2Data] SMALLDATETIME   NULL,
    [StockCodigo_U3]     NVARCHAR (20)   NULL,
    [StockCodigo_U3Data] SMALLDATETIME   NULL,
    [Descontinuado]      BIT             NOT NULL,
    [Inativo]            BIT             NOT NULL,
    [Criacao]            SMALLDATETIME   NOT NULL,
    [Versao]             DATETIME        NOT NULL,
    CONSTRAINT [PK_Produtos] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[ProdutosDetalhe]...';


GO
CREATE TABLE [dbo].[ProdutosDetalhe] (
    [ProdutoCodigo]                NVARCHAR (40)  NOT NULL,
    [FornecedorCodigo]             NVARCHAR (20)  NOT NULL,
    [Descricao]                    NVARCHAR (256) NOT NULL,
    [DescricaoPontuacao]           SMALLINT       NOT NULL,
    [DescricaoInativa]             BIT            NOT NULL,
    [Caracteristicas]              SMALLINT       NOT NULL,
    [CaracteristicasPontuacao]     SMALLINT       NOT NULL,
    [CaracteristicasInativas]      BIT            NOT NULL,
    [Link]                         SMALLINT       NOT NULL,
    [LinkPontuacao]                SMALLINT       NOT NULL,
    [LinkInativo]                  BIT            NOT NULL,
    [AtualizacaoAutomaticaInativa] BIT            NOT NULL,
    [AtualizacaoManualNecessaria]  BIT            NOT NULL,
    [IndicePreocupacaoConteudo]    TINYINT        NOT NULL,
    [Criacao]                      SMALLDATETIME  NOT NULL,
    [Versao]                       DATETIME       NOT NULL,
    CONSTRAINT [PK_ProdutosDetalhe] PRIMARY KEY CLUSTERED ([ProdutoCodigo] ASC, [FornecedorCodigo] ASC)
);


GO
PRINT N'Creating FK_Produtos_Categoria...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Categoria] FOREIGN KEY ([CategoriaCodigo]) REFERENCES [dbo].[Categorias] ([Codigo]);


GO
PRINT N'Creating FK_Produtos_Marca...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Marca] FOREIGN KEY ([MarcaCodigo]) REFERENCES [dbo].[Marcas] ([Codigo]);


GO
PRINT N'Creating FK_Produtos_Imposto...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Imposto] FOREIGN KEY ([ImpostoCodigo]) REFERENCES [dbo].[Impostos] ([Codigo]);


GO
PRINT N'Creating FK_Produtos_Fornecedor...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating FK_Produtos_Stock...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Stock] FOREIGN KEY ([StockCodigo]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating FK_Produtos_Stock_U1...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Stock_U1] FOREIGN KEY ([StockCodigo_U1]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating FK_Produtos_Stock_U2...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Stock_U2] FOREIGN KEY ([StockCodigo_U2]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating FK_Produtos_Stock_U3...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Stock_U3] FOREIGN KEY ([StockCodigo_U3]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating FK_ProdutosDetalhe_Produto...';


GO
ALTER TABLE [dbo].[ProdutosDetalhe] WITH NOCHECK
    ADD CONSTRAINT [FK_ProdutosDetalhe_Produto] FOREIGN KEY ([ProdutoCodigo]) REFERENCES [dbo].[Produtos] ([Codigo]);


GO
PRINT N'Creating FK_ProdutosDetalhe_Fornecedor...';


GO
ALTER TABLE [dbo].[ProdutosDetalhe] WITH NOCHECK
    ADD CONSTRAINT [FK_ProdutosDetalhe_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


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


/*IMPOSTOS*/
IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Impostos])
BEGIN	
	INSERT INTO [dbo].[Impostos] ([Codigo], [Descricao], [DesignacaoFiscal], [Taxa], [Inativo], [Criacao], [Versao])
		VALUES ('IVA.PTC.N23', 'IVA PT Continental - Taxa Normal de 23%', 'IVA PT.Continental 23%', 23.00, CAST('false' AS bit), GETDATE(), GETDATE())
END
GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Produtos] WITH CHECK CHECK CONSTRAINT [FK_Produtos_Categoria];

ALTER TABLE [dbo].[Produtos] WITH CHECK CHECK CONSTRAINT [FK_Produtos_Marca];

ALTER TABLE [dbo].[Produtos] WITH CHECK CHECK CONSTRAINT [FK_Produtos_Imposto];

ALTER TABLE [dbo].[Produtos] WITH CHECK CHECK CONSTRAINT [FK_Produtos_Fornecedor];

ALTER TABLE [dbo].[Produtos] WITH CHECK CHECK CONSTRAINT [FK_Produtos_Stock];

ALTER TABLE [dbo].[Produtos] WITH CHECK CHECK CONSTRAINT [FK_Produtos_Stock_U1];

ALTER TABLE [dbo].[Produtos] WITH CHECK CHECK CONSTRAINT [FK_Produtos_Stock_U2];

ALTER TABLE [dbo].[Produtos] WITH CHECK CHECK CONSTRAINT [FK_Produtos_Stock_U3];

ALTER TABLE [dbo].[ProdutosDetalhe] WITH CHECK CHECK CONSTRAINT [FK_ProdutosDetalhe_Produto];

ALTER TABLE [dbo].[ProdutosDetalhe] WITH CHECK CHECK CONSTRAINT [FK_ProdutosDetalhe_Fornecedor];


GO
PRINT N'Update complete.';


GO
