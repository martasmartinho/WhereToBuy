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
PRINT N'Altering [dbo].[ProdutosDetalhe]...';


GO
ALTER TABLE [dbo].[ProdutosDetalhe] ALTER COLUMN [Caracteristicas] NVARCHAR (2048) NOT NULL;

ALTER TABLE [dbo].[ProdutosDetalhe] ALTER COLUMN [Link] NVARCHAR (1024) NOT NULL;


GO
PRINT N'Creating [dbo].[Cotacoes]...';


GO
CREATE TABLE [dbo].[Cotacoes] (
    [FornecedorCodigo]                  NVARCHAR (20)   NOT NULL,
    [Data]                              DATETIME        NOT NULL,
    [_ProdutoCodigo]                    NVARCHAR (30)   NOT NULL,
    [_ComplementoCodigo]                NVARCHAR (20)   NOT NULL,
    [_ComplementoDescricao]             NVARCHAR (50)   NOT NULL,
    [_Partnumber]                       NVARCHAR (25)   NOT NULL,
    [_MarcaCodigo]                      NVARCHAR (20)   NOT NULL,
    [_MarcaDescricao]                   NVARCHAR (50)   NOT NULL,
    [_CategoriaCodigo]                  NVARCHAR (20)   NOT NULL,
    [_CategoriaDescricao]               NVARCHAR (50)   NOT NULL,
    [_StockCodigo]                      NVARCHAR (20)   NOT NULL,
    [_StockDescricao]                   NVARCHAR (50)   NOT NULL,
    [_ImpostoCodigo]                    NVARCHAR (20)   NOT NULL,
    [_ImpostoDescricao]                 NVARCHAR (50)   NOT NULL,
    [_Descricao]                        NVARCHAR (256)  NOT NULL,
    [_Link]                             NVARCHAR (1024) NOT NULL,
    [_Caracteristicas]                  NVARCHAR (2048) NOT NULL,
    [_Preco]                            DECIMAL (14, 4) NOT NULL,
    [_OutrosCustos]                     DECIMAL (14, 4) NOT NULL,
    [_OutrosCustosDescricao]            NVARCHAR (128)  NOT NULL,
    [ComplementoCodigo]                 NVARCHAR (20)   NULL,
    [Partnumber]                        NVARCHAR (25)   NULL,
    [MarcaCodigo]                       NVARCHAR (20)   NULL,
    [CategoriaCodigo]                   NVARCHAR (20)   NULL,
    [StockCodigo]                       NVARCHAR (20)   NULL,
    [StockCodigoSubstituto]             NVARCHAR (20)   NULL,
    [StockCodigoSubstitutoJustificacao] NVARCHAR (256)  NULL,
    [ImpostoCodigo]                     NVARCHAR (20)   NULL,
    [Descricao]                         NVARCHAR (256)  NULL,
    [Link]                              NVARCHAR (1024) NULL,
    [Caracteristicas]                   NVARCHAR (2048) NULL,
    [PrecoCusto]                        DECIMAL (14, 4) NULL,
    [PrecoCustoFormula]                 NVARCHAR (256)  NULL,
    [Validade]                          SMALLDATETIME   NULL,
    [ValidadeFormula]                   NVARCHAR (256)  NULL,
    [ProdutoCodigo]                     NVARCHAR (40)   NULL,
    [Completo]                          BIT             NOT NULL,
    [Integrado]                         BIT             NOT NULL,
    [Inativo]                           BIT             NOT NULL,
    [Versao]                            DATETIME        NOT NULL,
    CONSTRAINT [PK_Cotacoes] PRIMARY KEY CLUSTERED ([FornecedorCodigo] ASC, [Data] ASC, [_ProdutoCodigo] ASC, [_ComplementoCodigo] ASC)
);


GO
PRINT N'Creating FK_Cotacoes_Fornecedor...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating FK_Cotacoes_Complemento...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Complemento] FOREIGN KEY ([ComplementoCodigo]) REFERENCES [dbo].[Complementos] ([Codigo]);


GO
PRINT N'Creating FK_Cotacoes_Marca...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Marca] FOREIGN KEY ([MarcaCodigo]) REFERENCES [dbo].[Marcas] ([Codigo]);


GO
PRINT N'Creating FK_Cotacoes_Categoria...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Categoria] FOREIGN KEY ([CategoriaCodigo]) REFERENCES [dbo].[Categorias] ([Codigo]);


GO
PRINT N'Creating FK_Cotacoes_Stock...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Stock] FOREIGN KEY ([StockCodigo]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating FK_Cotacoes_StockSubstituto...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_StockSubstituto] FOREIGN KEY ([StockCodigoSubstituto]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating FK_Cotacoes_Imposto...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Imposto] FOREIGN KEY ([ImpostoCodigo]) REFERENCES [dbo].[Impostos] ([Codigo]);


GO
PRINT N'Creating FK_Cotacoes_Produto...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Produto] FOREIGN KEY ([ProdutoCodigo]) REFERENCES [dbo].[Produtos] ([Codigo]);


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
ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Fornecedor];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Complemento];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Marca];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Categoria];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Stock];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_StockSubstituto];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Imposto];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Produto];


GO
PRINT N'Update complete.';


GO
