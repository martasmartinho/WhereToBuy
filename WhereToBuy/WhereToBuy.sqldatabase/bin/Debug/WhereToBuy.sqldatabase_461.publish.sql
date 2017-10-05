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
PRINT N'Creating [dbo].[FornecedoresMarcas]...';


GO
CREATE TABLE [dbo].[FornecedoresMarcas] (
    [FornecedorCodigo] NVARCHAR (5)   NOT NULL,
    [MarcaCodigo]      NVARCHAR (5)   NOT NULL,
    [Confianca]        FLOAT (53)     NOT NULL,
    [Notas]            NVARCHAR (260) NULL,
    [Versao]           DATETIME       NOT NULL,
    CONSTRAINT [PK_FornecedoresMarcas] PRIMARY KEY CLUSTERED ([FornecedorCodigo] ASC, [MarcaCodigo] ASC)
);


GO
PRINT N'Creating [dbo].[FK_FornecedoresMarcas_Fornecedor]...';


GO
ALTER TABLE [dbo].[FornecedoresMarcas] WITH NOCHECK
    ADD CONSTRAINT [FK_FornecedoresMarcas_Fornecedor] FOREIGN KEY ([FornecedorCodigo]) REFERENCES [dbo].[Fornecedores] ([Codigo]);


GO
PRINT N'Creating [dbo].[FK_FornecedoresMarcas_marca]...';


GO
ALTER TABLE [dbo].[FornecedoresMarcas] WITH NOCHECK
    ADD CONSTRAINT [FK_FornecedoresMarcas_marca] FOREIGN KEY ([MarcaCodigo]) REFERENCES [dbo].[Marcas] ([Codigo]);


GO
PRINT N'Altering [dbo].[SupplierSelect]...';


GO
ALTER PROCEDURE [dbo].[SupplierSelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2048), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT f.[Codigo], f.[Nome], f.[Morada], 
							f.[CodigoPostal], f.[LocalidadePostal], 
							f.[Contribuinte], f.[Vendedor], 
							f.[Telefone], f.[Telemovel], 
							f.[SMS], f.[Email], 
							f.[AcessoOnlineAtivo], 
							f.[Username], fornecedores.[Password], 
							f.[HorasValidadeSugestao], 
							f.[ProdutosMatchingAutomatico], 
							f.[ProdutosCriacaoAutomatica], 
							f.[DisponibilizaInfoProdutoDetalhe], 
							f.[DescricaoPontuacaoInicial], 
							f.[CaracteristicasPontuacaoInicial], 
							f.[LinkPontuacaoInicial], 
							f.[ImagemPontuacaoInicial], 
							f.[DescricaoSugereInativo], 
							f.[CaracteristicasSugereInativo], 
							f.[LinkSugereInativo], 
							f.[ImagemSugereInativo], 
							f.[AtualizacaoAutomaticaInativaSugestao], 
							f.[ProdutosConfiancaPreco], 
							f.[ProdutosConfiancaDisponibilidade], 
							f.[Inativo], fornecedores.[Criacao], 
							f.[Versao] 
						FROM [dbo].[Fornecedores] f' 
	

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
PRINT N'Creating [dbo].[CategoryCount]...';


GO
CREATE PROCEDURE [dbo].[CategoryCount]
@WhereClause nvarchar(512) = ''			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
AS

	DECLARE @select nvarchar(1024), @where nvarchar(512), @sqlQuery nvarchar(2048)

	SET @select = 'SELECT Count([Codigo]) FROM [dbo].[Categorias]' 

	SET @where = LTRIM(RTRIM(@WhereClause))
	IF Len(@where) > 0
	BEGIN
		SET @where = ' WHERE ' + @where
	END


	SET @sqlQuery = @select + @where


	EXEC(@sqlQuery)

RETURN  -- This SP must be used with ExecuteScalar
GO
PRINT N'Creating [dbo].[CategoryDelete]...';


GO
CREATE PROCEDURE [dbo].[CategoryDelete]
	@Codigo nvarchar(5),
	@Versao datetime
AS
	DECLARE @Erro nvarchar(255)


	-- VALIDAR SE O CODIGO EXISTE
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Categorias] WHERE [Codigo] = @Codigo)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-D01] ' + 'O registo que pretende eliminar não existe!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	-- VALIDAR SE A VERSÃO AINDA É A MESMA
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Categorias] WHERE [Codigo] = @Codigo AND [Versao] = @Versao)
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
		DELETE FROM [dbo].[Categorias] Where [Codigo] = @Codigo

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
PRINT N'Creating [dbo].[CategoryInsert]...';


GO
CREATE PROCEDURE [dbo].[CategoryInsert]
	@Codigo nvarchar(5),
	@Descricao nvarchar(50),
	@PesoMedioUnidade float,
	@PrecoMinimoPermitido decimal(14,4),
	@PrecoMaximoPermitido decimal(14,4),
	@PrecoAmplitudeMax float,
	@Confianca float,
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
		INSERT INTO [dbo].[Categorias] ([Codigo], [Descricao], [PesoMedioUnidade],[PrecoMaximoPermitido],[PrecoMinimoPermitido], [PrecoAmplitudeMax], [Confianca], [Inativo], [Criacao], [Versao]) 
					VALUES (@Codigo, @Descricao, @PesoMedioUnidade, @PrecoMinimoPermitido, @PrecoMaximoPermitido, @PrecoAmplitudeMax, @Confianca, @Inativo, @Criacao, @Versao);
		
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
PRINT N'Creating [dbo].[CategorySelect]...';


GO
CREATE PROCEDURE [dbo].[CategorySelect]
	@WhereClause nvarchar(1024) = '',			-- ex1: Codigo IN ('001', '002'); ex2: Descricao LIKE '_este'; ex3: Descricao LIKE '%est%'
	@OrderByClause nvarchar(256) = ''			-- ex: Codigo desc
AS
	
	DECLARE @select nvarchar(2048), @where nvarchar(1024), @orderBy nvarchar(256), @sqlQuery nvarchar(4000)
	

	SET @select = 'SELECT c.[Codigo], c.[Descricao], c.[PesoMedioUnidade], c.[PesoMinimoPermitido], c.[PesoMaximoPermitido], c.[PrecoAmplitudeMax], c.[Confianca] c.[Inativo], c.[Criacao], marcas.[Versao]				
						FROM [dbo].[Categorias] c' 
	

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
PRINT N'Creating [dbo].[CategoryUpdate]...';


GO
CREATE PROCEDURE [dbo].[CategoryUpdate]
	@Codigo nvarchar(5),
	@Descricao nvarchar(50),
	@PesoMedioUnidade float,
	@PrecoMinimoPermitido decimal(14,4),
	@PrecoMaximoPermitido decimal(14,4),
	@PrecoAmplitudeMax float,
	@Confianca float,
	@Inativo bit,
	@Versao datetime
AS

	DECLARE @Erro nvarchar(255)


	-- VALIDAR SE O REGISTO EXISTE
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Categorias] WHERE [Codigo] = @Codigo)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U01] ' + 'O registo que pretende alterar não existe!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	-- VALIDAR SE A VERSÃO AINDA É A MESMA
	IF NOT EXISTS(SELECT [Codigo] FROM [dbo].[Categorias] WHERE [Codigo] = @Codigo AND [Versao] = @Versao)
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
		UPDATE [dbo].[Categorias] SET [Descricao] = @Descricao,
									[PesoMedioUnidade] = @PesoMedioUnidade,
									[PrecoMinimoPermitido] = @PrecoMinimoPermitido,
									[PrecoMaximoPermitido] = @PrecoMaximoPermitido,
									[PrecoAmplitudeMax] = @PrecoAmplitudeMax,
									[Confianca] = @Confianca,
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
PRINT N'Creating [dbo].[StateCount]...';


GO
CREATE PROCEDURE [dbo].[StateCount]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[StateDelete]...';


GO
CREATE PROCEDURE [dbo].[StateDelete]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[StateInsert]...';


GO
CREATE PROCEDURE [dbo].[StateInsert]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[StateSelect]...';


GO
CREATE PROCEDURE [dbo].[StateSelect]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[StateUpdate]...';


GO
CREATE PROCEDURE [dbo].[StateUpdate]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[SupplementCount]...';


GO
CREATE PROCEDURE [dbo].[SupplementCount]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[SupplementDelete]...';


GO
CREATE PROCEDURE [dbo].[SupplementDelete]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[SupplementInsert]...';


GO
CREATE PROCEDURE [SupplementInsert]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[SupplementSelect]...';


GO
CREATE PROCEDURE [dbo].[SupplementSelect]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[SupplementUpdate]...';


GO
CREATE PROCEDURE [dbo].[SupplementUpdate]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[TaxCount]...';


GO
CREATE PROCEDURE [dbo].[TaxCount]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[TaxDelete]...';


GO
CREATE PROCEDURE [dbo].[TaxDelete]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[TaxInsert]...';


GO
CREATE PROCEDURE [dbo].[TaxInsert]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[TaxSelect]...';


GO
CREATE PROCEDURE [dbo].[TaxSelect]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[TaxUpdate]...';


GO
CREATE PROCEDURE [dbo].[TaxUpdate]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[WorryingTermCount]...';


GO
CREATE PROCEDURE [dbo].[WorryingTermCount]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[WorryingTermDelete]...';


GO
CREATE PROCEDURE [dbo].[WorryingTermDelete]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[WorryingTermInsert]...';


GO
CREATE PROCEDURE [dbo].[WorryingTermInsert]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[WorryingTermSelect]...';


GO
CREATE PROCEDURE [dbo].[WorryingTermSelect]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
GO
PRINT N'Creating [dbo].[WorryingTermUpdate]...';


GO
CREATE PROCEDURE [dbo].[WorryingTermUpdate]
	@param1 int = 0,
	@param2 int
AS
	SELECT @param1, @param2
RETURN 0
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
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[FornecedoresMarcas] WITH CHECK CHECK CONSTRAINT [FK_FornecedoresMarcas_Fornecedor];

ALTER TABLE [dbo].[FornecedoresMarcas] WITH CHECK CHECK CONSTRAINT [FK_FornecedoresMarcas_marca];


GO
PRINT N'Update complete.';


GO
