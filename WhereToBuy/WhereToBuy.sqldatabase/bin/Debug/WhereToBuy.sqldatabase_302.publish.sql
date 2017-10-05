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
PRINT N'Altering [dbo].[GetProdutoMedidasPrecoFunction]...';


GO
/*
	This function generates product statistics:

		> U1VP - last price variation (x = 1 not changed, x > 1 means price was rise and x < 1 means the price was dropped
		> U2VP - penultimate...
		> U3VP - antepenultimate...

		> EEP (Estatistica de Estabilidade de Preço) means Price Stability Statistic.
	          Represents a estimation in days of the stability of actual price.

		> ICP (Indice de Confiança no Preço) means Price Trust Index.
		      Thats a value between [0:1] that represents the current price trust.

			  ICP = 0.5CE + 0.2CT + 0.3CF

		> CE  (Componente Estabilidade) Estability Component
		      Is the price trust in prespective to EEP
		
		> CT  (Componente Tendencia) Trend Component
			  Is the price trust in prespective of last evolution of the price.

		> CF  (Confiança Fornecedor) Trust on the Supplier
			  Is the trust that exists on the supplier (supplier table record)

*/

ALTER FUNCTION [dbo].GetProdutoMedidasPrecoFunction
(
	@U3Preco decimal(14,4),
	@U3Data smalldatetime,
	@U2Preco decimal(14,4),
	@U2Data smalldatetime,
	@U1Preco decimal(14,4),
	@U1Data smalldatetime,
	@UPreco decimal(14,4),
	@UData smalldatetime,
	@ConfiancaFornecedor float
)
RETURNS @results TABLE
(
	__id integer PRIMARY KEY NOT NULL IDENTITY(1,1),
	U3VP decimal(14,4) NULL,
	U2VP decimal(14,4) NULL,
	U1VP decimal(14,4) NULL,
	EEP float NOT NULL,
	ICP float NOT NULL,
	EEPFormula nvarchar(256),
	ICPCEFormula nvarchar(256),
	ICPCTFormula nvarchar(256),
	ICPCFFormula nvarchar(256),
	ICPFormula nvarchar(256)
)
AS
BEGIN

	-- variables
	DECLARE @eepFormula NVARCHAR(256), @icpFormula nvarchar(256), @ceFormula nvarchar(256), @ctFormula nvarchar(256), @cfFormula nvarchar(256),
	        @u3vp float, @u2vp float, @u1vp float, @eep float, @icp float, @ce float, @ct float, @cf float;


	-- initialize variables
	SET @eepFormula = '';
	SET @icpFormula = '';
	SET @ceFormula = '';
	SET @ctFormula = '';
	SET @cfFormula = '';
	SET @u3vp = NULL;
	SET @u2vp = NULL;
	SET @u1vp = NULL;
	SET @eep = 1;	-- minimum stability: 1 day
	SET @icp = 0;
	SET @ce = 0;
	SET @ct = 0;
	SET @cf = 0



	-- trend calculation
	IF @U3Preco IS NOT NULL	-- if this price it is not null, then we have the guarantee that @U2Preco it isn't too.
	BEGIN
		SET @u3vp = @U2Preco / @U3Preco;
	END

	IF @U2Preco IS NOT NULL -- if this price it is not null, then we have the guarantee that @U1Preco it isn't too.
	BEGIN
		SET @u2vp = @U1Preco / @U2Preco;
	END

	IF @U1Preco IS NOT NULL -- if this price it is not null, then we have the guarantee that @UPreco it isn't too.
	BEGIN
		SET @u1vp = @UPreco / @U1Preco;
	END



	-- EEP and EEPFormula calculation
	SELECT @eep =
				CASE WHEN @U3Data IS NOT NULL THEN 
													0.1 * DATEDIFF(DAY, @U3Data, @U2Data) +
													0.4 * DATEDIFF(DAY, @U2Data, @U1Data) +
													0.5 * DATEDIFF(DAY, @U1Data, @UData)

					WHEN @U2Data IS NOT NULL THEN	
													0.4 * DATEDIFF(DAY, @U2Data, @U1Data) +
													0.5 * DATEDIFF(DAY, @U1Data, @UData)

					WHEN @U1Data IS NOT NULL THEN	
													0.5 * DATEDIFF(DAY, @U1Data, @UData)

					ELSE 1
				END,

			@eepFormula = 
				CASE WHEN @U3Data IS NOT NULL THEN 
													'EEP = 0.1(U3Data - U2Data) + ' +
														  '0.4(U2Data - U1Data) + ' +
													      '0.5(U1Data - UData) <=> ' +
													'EEP = 0.1(' + CONVERT(varchar, @U3Data, 103) + ' - ' + CONVERT(varchar, @U2Data, 103)  + ') + ' +
													      '0.4(' + CONVERT(varchar, @U2Data, 103) + ' - ' + CONVERT(varchar, @U1Data, 103)  + ') + ' +
														  '0.5(' + CONVERT(varchar, @U1Data, 103) + ' - ' + CONVERT(varchar, @UData, 103)  + ') <=> ' +
													'EEP = ' + CAST(@eep as varchar)

					 WHEN @U2Data IS NOT NULL THEN	
													'EEP = 0.4(U2Data - U1Data) + ' +
													      '0.5(U1Data - UData) <=> ' +
													'EEP = 0.4(' + CONVERT(varchar, @U2Data, 103) + ' - ' + CONVERT(varchar, @U1Data, 103)  + ') + ' +
														  '0.5(' + CONVERT(varchar, @U1Data, 103) + ' - ' + CONVERT(varchar, @UData, 103)  + ') <=> ' +
													'EEP = ' + CAST(@eep as varchar)

					 WHEN @U1Data IS NOT NULL THEN	
													'EEP = 0.5(U1Data - UData) <=> ' +
													'EEP = 0.5(' + CONVERT(varchar, @U1Data, 103) + ' - ' + CONVERT(varchar, @UData, 103)  + ') <=> ' +
													'EEP = ' + CAST(@eep as varchar)

					 ELSE 'EEP = 1 (default)'
			    END



	
	-- CE (ICP component calculation)
	IF @UData IS NOT NULL
	BEGIN
		
		-- CE calculation
		SELECT @ce = (1 - (DATEDIFF(DAY, @UData, GETDATE()) / @eep));

		-- CE formula calculation
		SELECT @ceFormula = 'CE = (1 - ((UData - Hoje) / EEP)) <=> ' +
							'CE = (1 - ((' + CONVERT(varchar, @UData, 103) + ' - ' + CONVERT(varchar, GETDATE(), 103) + ') / ' + CAST(@eep as varchar)  + ')) <=> ' +
							'CE = ' + CAST(@ce as varchar);
	END
	ELSE
	BEGIN
		-- default
		SELECT @ce = 0, 
			   @ceFormula = 'CE = 0 (default)';
	END




	-- CT (ICP component calculation)
	IF @U1Preco IS NOT NULL -- we have sure that @UPreco is not null too
	BEGIN

		-- CT calculation
		SELECT @ct = 
				   CASE WHEN @UPreco > @U1Preco THEN (@U1Preco / @UPreco) - (50 * (1 - (@U1Preco / @UPreco)))
						ELSE (@UPreco / @U1Preco)
				   END;

		-- CT formula calculation
		SELECT @ctFormula = 
				   CASE WHEN @UPreco > @U1Preco THEN 
														'CT = (U1Preco / UPreco) - ' + 
																	'(50 * (1 - (@U1Preco / @UPreco))) <=> ' +
														'CT = (' + CONVERT(varchar, @U1Preco) + ' / ' + CONVERT(varchar, @UPreco) + ') - ' +
																	'(50 * (1 - (' + CONVERT(varchar, @U1Preco) + ' / ' + CONVERT(varchar, @UPreco) + '))) <=> ' +
														'CT = ' + CAST(@ct as varchar)
						ELSE 
							  'CT = (UPreco / U1Preco) <=> ' +
							  'CT = (' + CONVERT(varchar, @UPreco) + ' / ' + CONVERT(varchar, @U1Preco) + ') <=> ' +
							  'CT = ' + CAST(@ct as varchar)
				   END;
	END
	ELSE
	BEGIN
		-- default
		SELECT @ct = 0, 
			   @ctFormula = 'CT = 0 (default)';
	END




	-- CF (ICP component calculation)
	IF @ConfiancaFornecedor IS NOT NULL
	BEGIN
		
		-- CF calculation
		SELECT @cf = @ConfiancaFornecedor / 100;

		-- CF formula calculation
		SELECT @cfFormula = 'CF = (ConfiancaFornecedor / 100) <=> ' +
							'CF = (' + CAST(@ConfiancaFornecedor as varchar) + ' / 100) <=> ' +
							'CF = (' + CAST(@cf as varchar);

	END
	ELSE
	BEGIN
		-- default
		SELECT @cf = 0, 
			   @cfFormula = 'CF = 0 (default)';
	END







	INSERT INTO @results
	SELECT @u3vp, @u2vp, @u1vp, @eep, 1, @eepFormula, @ceFormula, @ctFormula, @cfFormula, null;
	/*
	EEP float NOT NULL,
	ICP float NOT NULL,
	EEPFormula nvarchar(256),
	ICPCEFormula nvarchar(256),
	ICPCTFormula nvarchar(256),
	ICPCFFormula nvarchar(256),
	ICPFormula nvarchar(256)
	*/
	-- the end
	RETURN;
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
