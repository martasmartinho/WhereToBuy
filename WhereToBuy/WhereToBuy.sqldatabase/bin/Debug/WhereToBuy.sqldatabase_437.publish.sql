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
PRINT N'Creating [dbo].[AvisosTipo]...';


GO
CREATE TABLE [dbo].[AvisosTipo] (
    [Codigo]    NVARCHAR (5)   NOT NULL,
    [Descricao] NVARCHAR (128) NOT NULL,
    [Gravidade] SMALLINT       NOT NULL,
    [Notas]     NVARCHAR (256) NULL,
    [Icon]      NVARCHAR (20)  NULL,
    [Inativo]   BIT            NOT NULL,
    [Criacao]   SMALLDATETIME  NOT NULL,
    [Versao]    DATETIME       NOT NULL,
    CONSTRAINT [PK_AvisosTipo] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[Catalogos]...';


GO
CREATE TABLE [dbo].[Catalogos] (
    [Codigo]    NVARCHAR (20)  NOT NULL,
    [Descricao] NVARCHAR (50)  NOT NULL,
    [Notas]     NVARCHAR (256) NULL,
    [Inativo]   BIT            NOT NULL,
    [Criacao]   SMALLDATETIME  NOT NULL,
    [Versao]    DATETIME       NOT NULL,
    CONSTRAINT [PK_Catalogos] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[Categorias]...';


GO
CREATE TABLE [dbo].[Categorias] (
    [Codigo]               NVARCHAR (20)   NOT NULL,
    [Descricao]            NVARCHAR (50)   NOT NULL,
    [PesoMedioUnidade]     FLOAT (53)      NOT NULL,
    [PrecoMinimoPermitido] DECIMAL (14, 4) NOT NULL,
    [PrecoMaximoPermitido] DECIMAL (14, 4) NOT NULL,
    [PrecoAmplitudeMax]    FLOAT (53)      NOT NULL,
    [Inativo]              BIT             NOT NULL,
    [Criacao]              SMALLDATETIME   NOT NULL,
    [Versao]               DATETIME        NOT NULL,
    CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[CategoriasMatching]...';


GO
CREATE TABLE [dbo].[CategoriasMatching] (
    [FornecedorCodigo] NVARCHAR (5)   NOT NULL,
    [Codigo]           NVARCHAR (128) NOT NULL,
    [Descricao]        NVARCHAR (128) NOT NULL,
    [MapTo]            NVARCHAR (20)  NULL,
    [Inativo]          BIT            NOT NULL,
    [Criacao]          SMALLDATETIME  NOT NULL,
    [Versao]           DATETIME       NOT NULL,
    CONSTRAINT [PK_CategoriasMatching] PRIMARY KEY CLUSTERED ([FornecedorCodigo] ASC, [Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[Classes]...';


GO
CREATE TABLE [dbo].[Classes] (
    [Codigo]            NVARCHAR (20)   NOT NULL,
    [Descricao]         NVARCHAR (50)   NOT NULL,
    [CatalogoCodigo]    NVARCHAR (20)   NOT NULL,
    [Margem]            FLOAT (53)      NOT NULL,
    [MargemValorMinimo] DECIMAL (14, 4) NOT NULL,
    [Notas]             NVARCHAR (256)  NULL,
    [Inativo]           BIT             NOT NULL,
    [Criacao]           SMALLDATETIME   NOT NULL,
    [Versao]            DATETIME        NOT NULL,
    CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[ClassesCategorias]...';


GO
CREATE TABLE [dbo].[ClassesCategorias] (
    [ClasseCodigo]    NVARCHAR (20)  NOT NULL,
    [CategoriaCodigo] NVARCHAR (20)  NOT NULL,
    [Notas]           NVARCHAR (256) NULL,
    [Versao]          DATETIME       NOT NULL,
    CONSTRAINT [PK_ClassesCategorias] PRIMARY KEY CLUSTERED ([ClasseCodigo] ASC, [CategoriaCodigo] ASC)
);


GO
PRINT N'Creating [dbo].[Complementos]...';


GO
CREATE TABLE [dbo].[Complementos] (
    [Codigo]           NVARCHAR (5)   NOT NULL,
    [Descricao]        NVARCHAR (50)  NOT NULL,
    [TermoAcrescentar] NVARCHAR (50)  NULL,
    [TermosRemover]    NVARCHAR (100) NULL,
    [Inativo]          BIT            NOT NULL,
    [Criacao]          SMALLDATETIME  NOT NULL,
    [Versao]           DATETIME       NOT NULL,
    CONSTRAINT [PK_Complementos] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[ComplementosMatching]...';


GO
CREATE TABLE [dbo].[ComplementosMatching] (
    [FornecedorCodigo] NVARCHAR (5)   NOT NULL,
    [Codigo]           NVARCHAR (128) NOT NULL,
    [Descricao]        NVARCHAR (128) NOT NULL,
    [MapTo]            NVARCHAR (5)   NULL,
    [Inativo]          BIT            NOT NULL,
    [Criacao]          SMALLDATETIME  NOT NULL,
    [Versao]           DATETIME       NOT NULL,
    CONSTRAINT [PK_ComplementosMatching] PRIMARY KEY CLUSTERED ([FornecedorCodigo] ASC, [Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[Cotacoes]...';


GO
CREATE TABLE [dbo].[Cotacoes] (
    [FornecedorCodigo]                  NVARCHAR (5)    NOT NULL,
    [Data]                              DATETIME        NOT NULL,
    [_ProdutoCodigo]                    NVARCHAR (256)  NOT NULL,
    [_ComplementoCodigo]                NVARCHAR (128)  NOT NULL,
    [_ComplementoDescricao]             NVARCHAR (128)  NOT NULL,
    [_Partnumber]                       NVARCHAR (25)   NOT NULL,
    [_MarcaCodigo]                      NVARCHAR (128)  NOT NULL,
    [_MarcaDescricao]                   NVARCHAR (128)  NOT NULL,
    [_CategoriaCodigo]                  NVARCHAR (128)  NOT NULL,
    [_CategoriaDescricao]               NVARCHAR (128)  NOT NULL,
    [_StockCodigo]                      NVARCHAR (128)  NOT NULL,
    [_StockDescricao]                   NVARCHAR (128)  NOT NULL,
    [_ImpostoCodigo]                    NVARCHAR (128)  NOT NULL,
    [_ImpostoDescricao]                 NVARCHAR (128)  NOT NULL,
    [_EstadoCodigo]                     NVARCHAR (128)  NOT NULL,
    [_EstadoDescricao]                  NVARCHAR (128)  NOT NULL,
    [_Descricao]                        NVARCHAR (256)  NOT NULL,
    [_Link]                             NVARCHAR (1024) NOT NULL,
    [_Caracteristicas]                  NVARCHAR (2048) NOT NULL,
    [_Imagem]                           NVARCHAR (1024) NOT NULL,
    [_Preco]                            DECIMAL (14, 4) NOT NULL,
    [_OutrosCustos]                     DECIMAL (14, 4) NOT NULL,
    [_OutrosCustosDescricao]            NVARCHAR (128)  NOT NULL,
    [_Validade]                         SMALLDATETIME   NOT NULL,
    [_ValidadeDescricao]                NVARCHAR (128)  NOT NULL,
    [ComplementoCodigo]                 NVARCHAR (5)    NULL,
    [Partnumber]                        NVARCHAR (25)   NULL,
    [MarcaCodigo]                       NVARCHAR (5)    NULL,
    [CategoriaCodigo]                   NVARCHAR (20)   NULL,
    [StockCodigo]                       NVARCHAR (5)    NULL,
    [StockCodigoSubstituto]             NVARCHAR (5)    NULL,
    [StockCodigoSubstitutoJustificacao] NVARCHAR (256)  NULL,
    [ImpostoCodigo]                     NVARCHAR (5)    NULL,
    [EstadoCodigo]                      NVARCHAR (5)    NULL,
    [Descricao]                         NVARCHAR (256)  NULL,
    [Link]                              NVARCHAR (1024) NULL,
    [Caracteristicas]                   NVARCHAR (2048) NULL,
    [Imagem]                            NVARCHAR (1024) NULL,
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
PRINT N'Creating [dbo].[CotacoesAvisos]...';


GO
CREATE TABLE [dbo].[CotacoesAvisos] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [Data]               SMALLDATETIME    NOT NULL,
    [_ProdutoCodigo]     NVARCHAR (256)   NOT NULL,
    [_ComplementoCodigo] NVARCHAR (128)   NOT NULL,
    [FornecedorCodigo]   NVARCHAR (5)     NOT NULL,
    [AvisoTipoCodigo]    NVARCHAR (5)     NOT NULL,
    [Descricao]          NVARCHAR (2048)  NOT NULL,
    [Criacao]            SMALLDATETIME    NOT NULL,
    CONSTRAINT [PK_CotacoesAvisos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UC_CotacoesAvisos] UNIQUE NONCLUSTERED ([FornecedorCodigo] ASC, [Data] ASC, [_ProdutoCodigo] ASC, [_ComplementoCodigo] ASC, [AvisoTipoCodigo] ASC)
);


GO
PRINT N'Creating [dbo].[CotacoesRegras]...';


GO
CREATE TABLE [dbo].[CotacoesRegras] (
    [FornecedorCodigo]      NVARCHAR (5)   NOT NULL,
    [MarcaCodigo]           NVARCHAR (5)   NOT NULL,
    [CategoriaCodigo]       NVARCHAR (20)  NOT NULL,
    [StockCodigo]           NVARCHAR (5)   NOT NULL,
    [HorasValidade]         SMALLINT       NOT NULL,
    [StockCodigoSubstituto] NVARCHAR (5)   NULL,
    [DataReset]             SMALLDATETIME  NULL,
    [Notas]                 NVARCHAR (256) NULL,
    [Versao]                DATETIME       NOT NULL,
    CONSTRAINT [PK_CotacoesRegras] PRIMARY KEY CLUSTERED ([FornecedorCodigo] ASC, [MarcaCodigo] ASC, [CategoriaCodigo] ASC, [StockCodigo] ASC)
);


GO
PRINT N'Creating [dbo].[Estados]...';


GO
CREATE TABLE [dbo].[Estados] (
    [Codigo]    NVARCHAR (5)  NOT NULL,
    [Descricao] NVARCHAR (50) NOT NULL,
    [Inativo]   BIT           NOT NULL,
    [Criacao]   SMALLDATETIME NOT NULL,
    [Versao]    DATETIME      NOT NULL,
    CONSTRAINT [PK_Estados] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[EstadosMatching]...';


GO
CREATE TABLE [dbo].[EstadosMatching] (
    [FornecedorCodigo] NVARCHAR (5)   NOT NULL,
    [Codigo]           NVARCHAR (128) NOT NULL,
    [Descricao]        NVARCHAR (128) NOT NULL,
    [MapTo]            NVARCHAR (5)   NULL,
    [Inativo]          BIT            NOT NULL,
    [Criacao]          SMALLDATETIME  NOT NULL,
    [Versao]           DATETIME       NOT NULL,
    CONSTRAINT [PK_EstadosMatching] PRIMARY KEY CLUSTERED ([FornecedorCodigo] ASC, [Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[Fornecedores]...';


GO
CREATE TABLE [dbo].[Fornecedores] (
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
    [Inativo]                              BIT            NOT NULL,
    [Criacao]                              SMALLDATETIME  NOT NULL,
    [Versao]                               DATETIME       NOT NULL,
    CONSTRAINT [PK_Fornecedores] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[Grupos]...';


GO
CREATE TABLE [dbo].[Grupos] (
    [Codigo]    NVARCHAR (20) NOT NULL,
    [Descricao] NVARCHAR (50) NOT NULL,
    [Inativo]   BIT           NOT NULL,
    [Criacao]   SMALLDATETIME NOT NULL,
    [Versao]    DATETIME      NOT NULL,
    CONSTRAINT [PK_Grupos] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[Impostos]...';


GO
CREATE TABLE [dbo].[Impostos] (
    [Codigo]           NVARCHAR (5)  NOT NULL,
    [Descricao]        NVARCHAR (50) NOT NULL,
    [DesignacaoFiscal] NVARCHAR (50) NOT NULL,
    [Taxa]             FLOAT (53)    NOT NULL,
    [Inativo]          BIT           NOT NULL,
    [Criacao]          SMALLDATETIME NOT NULL,
    [Versao]           DATETIME      NOT NULL,
    CONSTRAINT [PK_Impostos] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[ImpostosMatching]...';


GO
CREATE TABLE [dbo].[ImpostosMatching] (
    [FornecedorCodigo] NVARCHAR (5)   NOT NULL,
    [Codigo]           NVARCHAR (128) NOT NULL,
    [Descricao]        NVARCHAR (128) NOT NULL,
    [MapTo]            NVARCHAR (5)   NULL,
    [Inativo]          BIT            NOT NULL,
    [Criacao]          SMALLDATETIME  NOT NULL,
    [Versao]           DATETIME       NOT NULL,
    CONSTRAINT [PK_ImpostosMatching] PRIMARY KEY CLUSTERED ([FornecedorCodigo] ASC, [Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[Marcas]...';


GO
CREATE TABLE [dbo].[Marcas] (
    [Codigo]    NVARCHAR (5)  NOT NULL,
    [Descricao] NVARCHAR (50) NOT NULL,
    [Inativo]   BIT           NOT NULL,
    [Criacao]   SMALLDATETIME NOT NULL,
    [Versao]    DATETIME      NOT NULL,
    CONSTRAINT [PK_Marcas] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[MarcasMatching]...';


GO
CREATE TABLE [dbo].[MarcasMatching] (
    [FornecedorCodigo] NVARCHAR (5)   NOT NULL,
    [Codigo]           NVARCHAR (128) NOT NULL,
    [Descricao]        NVARCHAR (128) NOT NULL,
    [MapTo]            NVARCHAR (5)   NULL,
    [Inativo]          BIT            NOT NULL,
    [Criacao]          SMALLDATETIME  NOT NULL,
    [Versao]           DATETIME       NOT NULL,
    CONSTRAINT [PK_MarcasMatching] PRIMARY KEY CLUSTERED ([FornecedorCodigo] ASC, [Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[Produtos]...';


GO
CREATE TABLE [dbo].[Produtos] (
    [Codigo]             NVARCHAR (40)   NOT NULL,
    [Descricao]          NVARCHAR (256)  NOT NULL,
    [Partnumber]         NVARCHAR (25)   NOT NULL,
    [CategoriaCodigo]    NVARCHAR (20)   NOT NULL,
    [MarcaCodigo]        NVARCHAR (5)    NOT NULL,
    [ImpostoCodigo]      NVARCHAR (5)    NOT NULL,
    [FornecedorCodigo]   NVARCHAR (5)    NOT NULL,
    [PrecoCusto]         DECIMAL (14, 4) NULL,
    [PrecoCusto_Data]    SMALLDATETIME   NULL,
    [PrecoCusto_U1]      DECIMAL (14, 4) NULL,
    [PrecoCusto_U1Data]  SMALLDATETIME   NULL,
    [PrecoCusto_U2]      DECIMAL (14, 4) NULL,
    [PrecoCusto_U2Data]  SMALLDATETIME   NULL,
    [PrecoCusto_U3]      DECIMAL (14, 4) NULL,
    [PrecoCusto_U3Data]  SMALLDATETIME   NULL,
    [StockCodigo]        NVARCHAR (5)    NULL,
    [StockCodigo_Data]   SMALLDATETIME   NULL,
    [StockCodigo_U1]     NVARCHAR (5)    NULL,
    [StockCodigo_U1Data] SMALLDATETIME   NULL,
    [StockCodigo_U2]     NVARCHAR (5)    NULL,
    [StockCodigo_U2Data] SMALLDATETIME   NULL,
    [StockCodigo_U3]     NVARCHAR (5)    NULL,
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
    [ProdutoCodigo]                NVARCHAR (40)   NOT NULL,
    [FornecedorCodigo]             NVARCHAR (5)    NOT NULL,
    [Descricao]                    NVARCHAR (256)  NOT NULL,
    [DescricaoPontuacao]           SMALLINT        NOT NULL,
    [DescricaoInativa]             BIT             NOT NULL,
    [Caracteristicas]              NVARCHAR (2048) NOT NULL,
    [CaracteristicasPontuacao]     SMALLINT        NOT NULL,
    [CaracteristicasInativas]      BIT             NOT NULL,
    [Link]                         NVARCHAR (1024) NOT NULL,
    [LinkPontuacao]                SMALLINT        NOT NULL,
    [LinkInativo]                  BIT             NOT NULL,
    [Imagem]                       NVARCHAR (1024) NOT NULL,
    [ImagemPontuacao]              SMALLINT        NOT NULL,
    [ImagemInativa]                BIT             NOT NULL,
    [AtualizacaoAutomaticaInativa] BIT             NOT NULL,
    [AtualizacaoManualNecessaria]  BIT             NOT NULL,
    [IndicePreocupacaoConteudo]    TINYINT         NOT NULL,
    [Criacao]                      SMALLDATETIME   NOT NULL,
    [Versao]                       DATETIME        NOT NULL,
    CONSTRAINT [PK_ProdutosDetalhe] PRIMARY KEY CLUSTERED ([ProdutoCodigo] ASC, [FornecedorCodigo] ASC)
);


GO
PRINT N'Creating [dbo].[ProdutosMatching]...';


GO
CREATE TABLE [dbo].[ProdutosMatching] (
    [FornecedorCodigo]                  NVARCHAR (5)   NOT NULL,
    [ComplementoCodigo]                 NVARCHAR (128) NOT NULL,
    [Codigo]                            NVARCHAR (256) NOT NULL,
    [Descricao]                         NVARCHAR (256) NOT NULL,
    [MapTo]                             NVARCHAR (40)  NULL,
    [HorasValidadeCotacao]              SMALLINT       NULL,
    [StockCodigoSubstituto]             NVARCHAR (5)   NULL,
    [DispensaPrevencaoPrecosDesfasados] BIT            NOT NULL,
    [DispensaPrevencaoFalsoStock]       BIT            NOT NULL,
    [DataReset]                         SMALLDATETIME  NULL,
    [Notas]                             NVARCHAR (256) NULL,
    [Inativo]                           BIT            NOT NULL,
    [Criacao]                           SMALLDATETIME  NOT NULL,
    [Versao]                            DATETIME       NOT NULL,
    CONSTRAINT [PK_ProdutosMatching] PRIMARY KEY CLUSTERED ([FornecedorCodigo] ASC, [ComplementoCodigo] ASC, [Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[Stocks]...';


GO
CREATE TABLE [dbo].[Stocks] (
    [Codigo]                  NVARCHAR (5)   NOT NULL,
    [Descricao]               NVARCHAR (50)  NOT NULL,
    [DisponibilidadeNivel]    SMALLINT       NOT NULL,
    [ValidadeP50_StockCodigo] NVARCHAR (5)   NULL,
    [ValidadeP60_StockCodigo] NVARCHAR (5)   NULL,
    [ValidadeP70_StockCodigo] NVARCHAR (5)   NULL,
    [ValidadeP80_StockCodigo] NVARCHAR (5)   NULL,
    [ValidadeP90_StockCodigo] NVARCHAR (5)   NULL,
    [Notas]                   NVARCHAR (256) NULL,
    [Inativo]                 BIT            NOT NULL,
    [Criacao]                 SMALLDATETIME  NOT NULL,
    [Versao]                  DATETIME       NOT NULL,
    CONSTRAINT [PK_Stocks] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[StocksMatching]...';


GO
CREATE TABLE [dbo].[StocksMatching] (
    [FornecedorCodigo] NVARCHAR (5)   NOT NULL,
    [Codigo]           NVARCHAR (128) NOT NULL,
    [Descricao]        NVARCHAR (128) NOT NULL,
    [MapTo]            NVARCHAR (5)   NULL,
    [Inativo]          BIT            NOT NULL,
    [Criacao]          SMALLDATETIME  NOT NULL,
    [Versao]           DATETIME       NOT NULL,
    CONSTRAINT [PK_StocksMatching] PRIMARY KEY CLUSTERED ([FornecedorCodigo] ASC, [Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[Subgrupos]...';


GO
CREATE TABLE [dbo].[Subgrupos] (
    [Codigo]      NVARCHAR (20) NOT NULL,
    [Descricao]   NVARCHAR (50) NOT NULL,
    [GrupoCodigo] NVARCHAR (20) NOT NULL,
    [Inativo]     BIT           NOT NULL,
    [Criacao]     SMALLDATETIME NOT NULL,
    [Versao]      DATETIME      NOT NULL,
    CONSTRAINT [PK_Subgrupos] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);


GO
PRINT N'Creating [dbo].[SubgruposClasses]...';


GO
CREATE TABLE [dbo].[SubgruposClasses] (
    [SubgrupoCodigo] NVARCHAR (20)  NOT NULL,
    [ClasseCodigo]   NVARCHAR (20)  NOT NULL,
    [Notas]          NVARCHAR (256) NULL,
    [Versao]         DATETIME       NOT NULL,
    CONSTRAINT [PK_SubgruposClasses] PRIMARY KEY CLUSTERED ([SubgrupoCodigo] ASC, [ClasseCodigo] ASC)
);


GO
PRINT N'Creating [dbo].[TermosPreocupantes]...';


GO
CREATE TABLE [dbo].[TermosPreocupantes] (
    [Termo]   NVARCHAR (50)  NOT NULL,
    [Indice]  TINYINT        NOT NULL,
    [Notas]   NVARCHAR (256) NULL,
    [Inativo] BIT            NOT NULL,
    [Criacao] SMALLDATETIME  NOT NULL,
    [Versao]  DATETIME       NOT NULL,
    CONSTRAINT [PK_TermosPreocupantes] PRIMARY KEY CLUSTERED ([Termo] ASC)
);


GO
PRINT N'Creating [dbo].[FK_CategoriasMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[CategoriasMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_CategoriasMatching_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_CategoriasMatching_MapTo]...';


GO
ALTER TABLE [dbo].[CategoriasMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_CategoriasMatching_MapTo] FOREIGN KEY ([MapTo]) REFERENCES [dbo].[Categorias] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Classes_Catalogo]...';


GO
ALTER TABLE [dbo].[Classes] WITH NOCHECK
    ADD CONSTRAINT [FK_Classes_Catalogo] FOREIGN KEY ([CatalogoCodigo]) REFERENCES [dbo].[Catalogos] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_ClassesCategorias_Classe]...';


GO
ALTER TABLE [dbo].[ClassesCategorias] WITH NOCHECK
    ADD CONSTRAINT [FK_ClassesCategorias_Classe] FOREIGN KEY ([ClasseCodigo]) REFERENCES [dbo].[Classes] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_ClassesCategorias_Categoria]...';


GO
ALTER TABLE [dbo].[ClassesCategorias] WITH NOCHECK
    ADD CONSTRAINT [FK_ClassesCategorias_Categoria] FOREIGN KEY ([CategoriaCodigo]) REFERENCES [dbo].[Categorias] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_ComplementosMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[ComplementosMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_ComplementosMatching_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_ComplementosMatching_MapTo]...';


GO
ALTER TABLE [dbo].[ComplementosMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_ComplementosMatching_MapTo] FOREIGN KEY ([MapTo]) REFERENCES [dbo].[Complementos] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Cotacoes_Fornecedor]...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Cotacoes_Complemento]...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Complemento] FOREIGN KEY ([ComplementoCodigo]) REFERENCES [dbo].[Complementos] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Cotacoes_Marca]...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Marca] FOREIGN KEY ([MarcaCodigo]) REFERENCES [dbo].[Marcas] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Cotacoes_Categoria]...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Categoria] FOREIGN KEY ([CategoriaCodigo]) REFERENCES [dbo].[Categorias] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Cotacoes_Stock]...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Stock] FOREIGN KEY ([StockCodigo]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Cotacoes_StockSubstituto]...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_StockSubstituto] FOREIGN KEY ([StockCodigoSubstituto]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Cotacoes_Imposto]...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Imposto] FOREIGN KEY ([ImpostoCodigo]) REFERENCES [dbo].[Impostos] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Cotacoes_Estado]...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Estado] FOREIGN KEY ([EstadoCodigo]) REFERENCES [dbo].[Estados] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Cotacoes_Produto]...';


GO
ALTER TABLE [dbo].[Cotacoes] WITH NOCHECK
    ADD CONSTRAINT [FK_Cotacoes_Produto] FOREIGN KEY ([ProdutoCodigo]) REFERENCES [dbo].[Produtos] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_CotacoesAvisos_Fornecedor]...';


GO
ALTER TABLE [dbo].[CotacoesAvisos] WITH NOCHECK
    ADD CONSTRAINT [FK_CotacoesAvisos_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_CotacoesAvisos_AvisoTipo]...';


GO
ALTER TABLE [dbo].[CotacoesAvisos] WITH NOCHECK
    ADD CONSTRAINT [FK_CotacoesAvisos_AvisoTipo] FOREIGN KEY ([AvisoTipoCodigo]) REFERENCES [dbo].[AvisosTipo] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_CotacoesRegras_Fornecedor]...';


GO
ALTER TABLE [dbo].[CotacoesRegras] WITH NOCHECK
    ADD CONSTRAINT [FK_CotacoesRegras_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_CotacoesRegras_Marca]...';


GO
ALTER TABLE [dbo].[CotacoesRegras] WITH NOCHECK
    ADD CONSTRAINT [FK_CotacoesRegras_Marca] FOREIGN KEY ([MarcaCodigo]) REFERENCES [dbo].[Marcas] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_CotacoesRegras_Categoria]...';


GO
ALTER TABLE [dbo].[CotacoesRegras] WITH NOCHECK
    ADD CONSTRAINT [FK_CotacoesRegras_Categoria] FOREIGN KEY ([CategoriaCodigo]) REFERENCES [dbo].[Categorias] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_CotacoesRegras_Stock]...';


GO
ALTER TABLE [dbo].[CotacoesRegras] WITH NOCHECK
    ADD CONSTRAINT [FK_CotacoesRegras_Stock] FOREIGN KEY ([StockCodigo]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_CotacoesRegras_StockSubstituto]...';


GO
ALTER TABLE [dbo].[CotacoesRegras] WITH NOCHECK
    ADD CONSTRAINT [FK_CotacoesRegras_StockSubstituto] FOREIGN KEY ([StockCodigoSubstituto]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_EstadosMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[EstadosMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_EstadosMatching_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_EstadosMatching_MapTo]...';


GO
ALTER TABLE [dbo].[EstadosMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_EstadosMatching_MapTo] FOREIGN KEY ([MapTo]) REFERENCES [dbo].[Estados] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_ImpostosMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[ImpostosMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_ImpostosMatching_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_ImpostosMatching_MapTo]...';


GO
ALTER TABLE [dbo].[ImpostosMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_ImpostosMatching_MapTo] FOREIGN KEY ([MapTo]) REFERENCES [dbo].[Impostos] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_MarcasMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[MarcasMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_MarcasMatching_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_MarcasMatching_MapTo]...';


GO
ALTER TABLE [dbo].[MarcasMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_MarcasMatching_MapTo] FOREIGN KEY ([MapTo]) REFERENCES [dbo].[Marcas] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Produtos_Categoria]...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Categoria] FOREIGN KEY ([CategoriaCodigo]) REFERENCES [dbo].[Categorias] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Produtos_Marca]...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Marca] FOREIGN KEY ([MarcaCodigo]) REFERENCES [dbo].[Marcas] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Produtos_Imposto]...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Imposto] FOREIGN KEY ([ImpostoCodigo]) REFERENCES [dbo].[Impostos] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Produtos_Fornecedor]...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Produtos_Stock]...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Stock] FOREIGN KEY ([StockCodigo]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Produtos_Stock_U1]...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Stock_U1] FOREIGN KEY ([StockCodigo_U1]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Produtos_Stock_U2]...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Stock_U2] FOREIGN KEY ([StockCodigo_U2]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Produtos_Stock_U3]...';


GO
ALTER TABLE [dbo].[Produtos] WITH NOCHECK
    ADD CONSTRAINT [FK_Produtos_Stock_U3] FOREIGN KEY ([StockCodigo_U3]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_ProdutosDetalhe_Produto]...';


GO
ALTER TABLE [dbo].[ProdutosDetalhe] WITH NOCHECK
    ADD CONSTRAINT [FK_ProdutosDetalhe_Produto] FOREIGN KEY ([ProdutoCodigo]) REFERENCES [dbo].[Produtos] ([Codigo]);


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
PRINT N'Creating [dbo].[FK_ProdutosMatching_MapTo]...';


GO
ALTER TABLE [dbo].[ProdutosMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_ProdutosMatching_MapTo] FOREIGN KEY ([MapTo]) REFERENCES [dbo].[Produtos] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_ProdutosMatching_StockSubstituto]...';


GO
ALTER TABLE [dbo].[ProdutosMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_ProdutosMatching_StockSubstituto] FOREIGN KEY ([StockCodigoSubstituto]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Stocks_ValidadeP50]...';


GO
ALTER TABLE [dbo].[Stocks] WITH NOCHECK
    ADD CONSTRAINT [FK_Stocks_ValidadeP50] FOREIGN KEY ([ValidadeP50_StockCodigo]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Stocks_ValidadeP60]...';


GO
ALTER TABLE [dbo].[Stocks] WITH NOCHECK
    ADD CONSTRAINT [FK_Stocks_ValidadeP60] FOREIGN KEY ([ValidadeP60_StockCodigo]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Stocks_ValidadeP70]...';


GO
ALTER TABLE [dbo].[Stocks] WITH NOCHECK
    ADD CONSTRAINT [FK_Stocks_ValidadeP70] FOREIGN KEY ([ValidadeP70_StockCodigo]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Stocks_ValidadeP80]...';


GO
ALTER TABLE [dbo].[Stocks] WITH NOCHECK
    ADD CONSTRAINT [FK_Stocks_ValidadeP80] FOREIGN KEY ([ValidadeP80_StockCodigo]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Stocks_ValidadeP90]...';


GO
ALTER TABLE [dbo].[Stocks] WITH NOCHECK
    ADD CONSTRAINT [FK_Stocks_ValidadeP90] FOREIGN KEY ([ValidadeP90_StockCodigo]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_StocksMatching_Fornecedor]...';


GO
ALTER TABLE [dbo].[StocksMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_StocksMatching_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_StocksMatching_MapTo]...';


GO
ALTER TABLE [dbo].[StocksMatching] WITH NOCHECK
    ADD CONSTRAINT [FK_StocksMatching_MapTo] FOREIGN KEY ([MapTo]) REFERENCES [dbo].[Stocks] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_Subgrupos_Grupo]...';


GO
ALTER TABLE [dbo].[Subgrupos] WITH NOCHECK
    ADD CONSTRAINT [FK_Subgrupos_Grupo] FOREIGN KEY ([GrupoCodigo]) REFERENCES [dbo].[Grupos] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_SubgruposClasses_Subgrupo]...';


GO
ALTER TABLE [dbo].[SubgruposClasses] WITH NOCHECK
    ADD CONSTRAINT [FK_SubgruposClasses_Subgrupo] FOREIGN KEY ([SubgrupoCodigo]) REFERENCES [dbo].[Subgrupos] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_SubgruposClasses_Classe]...';


GO
ALTER TABLE [dbo].[SubgruposClasses] WITH NOCHECK
    ADD CONSTRAINT [FK_SubgruposClasses_Classe] FOREIGN KEY ([ClasseCodigo]) REFERENCES [dbo].[Classes] ([Codigo]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[CategoriasMatching] WITH CHECK CHECK CONSTRAINT [FK_CategoriasMatching_Fornecedor];

ALTER TABLE [dbo].[CategoriasMatching] WITH CHECK CHECK CONSTRAINT [FK_CategoriasMatching_MapTo];

ALTER TABLE [dbo].[Classes] WITH CHECK CHECK CONSTRAINT [FK_Classes_Catalogo];

ALTER TABLE [dbo].[ClassesCategorias] WITH CHECK CHECK CONSTRAINT [FK_ClassesCategorias_Classe];

ALTER TABLE [dbo].[ClassesCategorias] WITH CHECK CHECK CONSTRAINT [FK_ClassesCategorias_Categoria];

ALTER TABLE [dbo].[ComplementosMatching] WITH CHECK CHECK CONSTRAINT [FK_ComplementosMatching_Fornecedor];

ALTER TABLE [dbo].[ComplementosMatching] WITH CHECK CHECK CONSTRAINT [FK_ComplementosMatching_MapTo];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Fornecedor];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Complemento];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Marca];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Categoria];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Stock];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_StockSubstituto];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Imposto];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Estado];

ALTER TABLE [dbo].[Cotacoes] WITH CHECK CHECK CONSTRAINT [FK_Cotacoes_Produto];

ALTER TABLE [dbo].[CotacoesAvisos] WITH CHECK CHECK CONSTRAINT [FK_CotacoesAvisos_Fornecedor];

ALTER TABLE [dbo].[CotacoesAvisos] WITH CHECK CHECK CONSTRAINT [FK_CotacoesAvisos_AvisoTipo];

ALTER TABLE [dbo].[CotacoesRegras] WITH CHECK CHECK CONSTRAINT [FK_CotacoesRegras_Fornecedor];

ALTER TABLE [dbo].[CotacoesRegras] WITH CHECK CHECK CONSTRAINT [FK_CotacoesRegras_Marca];

ALTER TABLE [dbo].[CotacoesRegras] WITH CHECK CHECK CONSTRAINT [FK_CotacoesRegras_Categoria];

ALTER TABLE [dbo].[CotacoesRegras] WITH CHECK CHECK CONSTRAINT [FK_CotacoesRegras_Stock];

ALTER TABLE [dbo].[CotacoesRegras] WITH CHECK CHECK CONSTRAINT [FK_CotacoesRegras_StockSubstituto];

ALTER TABLE [dbo].[EstadosMatching] WITH CHECK CHECK CONSTRAINT [FK_EstadosMatching_Fornecedor];

ALTER TABLE [dbo].[EstadosMatching] WITH CHECK CHECK CONSTRAINT [FK_EstadosMatching_MapTo];

ALTER TABLE [dbo].[ImpostosMatching] WITH CHECK CHECK CONSTRAINT [FK_ImpostosMatching_Fornecedor];

ALTER TABLE [dbo].[ImpostosMatching] WITH CHECK CHECK CONSTRAINT [FK_ImpostosMatching_MapTo];

ALTER TABLE [dbo].[MarcasMatching] WITH CHECK CHECK CONSTRAINT [FK_MarcasMatching_Fornecedor];

ALTER TABLE [dbo].[MarcasMatching] WITH CHECK CHECK CONSTRAINT [FK_MarcasMatching_MapTo];

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

ALTER TABLE [dbo].[ProdutosMatching] WITH CHECK CHECK CONSTRAINT [FK_ProdutosMatching_Fornecedor];

ALTER TABLE [dbo].[ProdutosMatching] WITH CHECK CHECK CONSTRAINT [FK_ProdutosMatching_MapTo];

ALTER TABLE [dbo].[ProdutosMatching] WITH CHECK CHECK CONSTRAINT [FK_ProdutosMatching_StockSubstituto];

ALTER TABLE [dbo].[Stocks] WITH CHECK CHECK CONSTRAINT [FK_Stocks_ValidadeP50];

ALTER TABLE [dbo].[Stocks] WITH CHECK CHECK CONSTRAINT [FK_Stocks_ValidadeP60];

ALTER TABLE [dbo].[Stocks] WITH CHECK CHECK CONSTRAINT [FK_Stocks_ValidadeP70];

ALTER TABLE [dbo].[Stocks] WITH CHECK CHECK CONSTRAINT [FK_Stocks_ValidadeP80];

ALTER TABLE [dbo].[Stocks] WITH CHECK CHECK CONSTRAINT [FK_Stocks_ValidadeP90];

ALTER TABLE [dbo].[StocksMatching] WITH CHECK CHECK CONSTRAINT [FK_StocksMatching_Fornecedor];

ALTER TABLE [dbo].[StocksMatching] WITH CHECK CHECK CONSTRAINT [FK_StocksMatching_MapTo];

ALTER TABLE [dbo].[Subgrupos] WITH CHECK CHECK CONSTRAINT [FK_Subgrupos_Grupo];

ALTER TABLE [dbo].[SubgruposClasses] WITH CHECK CHECK CONSTRAINT [FK_SubgruposClasses_Subgrupo];

ALTER TABLE [dbo].[SubgruposClasses] WITH CHECK CHECK CONSTRAINT [FK_SubgruposClasses_Classe];


GO
PRINT N'Update complete.';


GO