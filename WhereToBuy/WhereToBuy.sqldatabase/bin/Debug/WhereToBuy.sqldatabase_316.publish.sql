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
PRINT N'Altering [dbo].[GetProdutoMedidasDisponibilidadeFunction]...';


GO
/*
	This function generates product statistics:

		> U1VD - last availability variation (x = 1 not changed, x > 1 means availability level was rise and x < 1 means the availability level was dropped
		> U2VD - penultimate...
		> U3VD - antepenultimate...

		> EED (Estatistica de Estabilidade da Disponibilidade) means Statistic of Stability of Availability.
	          Represents a estimation in days of the stability of actual Stock Availability level.
			  How much days will the current level of availability stay actual.

		> ICD (Indice de Confiança na Disponibilidade) means Availability Trust Index.
		      Thats a value between [0:1] that represents the current level of availability trust.

			  ICD = 0.5CE + 0.2CT + 0.3CF

		> CE  (Componente Estabilidade) Estability Component
		      Is the availability level trust in prespective to EED. 
		
		> CT  (Componente Tendencia) Trend Component
			  Is the availability trust in prespective of last evolution of stock availability.

		> CF  (Confiança Fornecedor) Trust on the Supplier
			  Is the trust that exists on the supplier (supplier table record)

*/

ALTER FUNCTION [dbo].GetProdutoMedidasDisponibilidadeFunction
(
	@U3Disponivel smallint,
	@U3Data smalldatetime,
	@U2Disponivel smallint,
	@U2Data smalldatetime,
	@U1Disponivel smallint,
	@U1Data smalldatetime,
	@UDisponivel smallint,
	@UData smalldatetime,
	@ConfiancaFornecedor float
)
RETURNS @results TABLE
(
	__id integer PRIMARY KEY NOT NULL IDENTITY(1,1),
	U3VD float NULL,
	U2VD float NULL,
	U1VD float NULL,
	EED float NOT NULL,
	ICD float NOT NULL,
	EEDFormula nvarchar(256),
	ICDCEFormula nvarchar(256),
	ICDCTFormula nvarchar(256),
	ICDCFFormula nvarchar(256),
	ICDFormula nvarchar(256)
)
AS
BEGIN

	-- variables
	DECLARE @eedFormula nvarchar(256), @icdFormula nvarchar(256), @ceFormula nvarchar(256), @ctFormula nvarchar(256), @cfFormula nvarchar(256),
	        @u3vd float, @u2vd float, @u1vd float, @eed float, @icd float, @ce float, @ct float, @cf float;


	-- initialize variables
	SET @eedFormula = '';
	SET @icdFormula = '';
	SET @ceFormula = '';
	SET @ctFormula = '';
	SET @cfFormula = '';
	SET @u3vd = NULL;
	SET @u2vd = NULL;
	SET @u1vd = NULL;
	SET @eed = 1;	-- minimum stability: 1 day
	SET @icd = 0;
	SET @ce = 0;
	SET @ct = 0;
	SET @cf = 0



	-- trend calculation
	IF @U3Disponivel IS NOT NULL	-- if this level it is not null, then we have the guarantee that @U2Disponivel it isn't too.
	BEGIN
		SET @u3vd = CAST(@U2Disponivel as float) / @U3Disponivel;
	END

	IF @U2Disponivel IS NOT NULL	-- if this level it is not null, then we have the guarantee that @U1Disponivel it isn't too.
	BEGIN
		SET @u2vd = CAST(@U1Disponivel as float) / @U2Disponivel;
	END

	IF @U1Disponivel IS NOT NULL	-- if this level it is not null, then we have the guarantee that @UDisponivel it isn't too.
	BEGIN
		SET @u1vd = CAST(@UDisponivel as float) / @U1Disponivel;
	END



	-- EED and EEDFormula calculation
	SELECT @eed =
				CASE WHEN @U3Data IS NOT NULL THEN 
													0.1 * DATEDIFF(DAY, @U3Data, @U2Data) +
													0.2 * DATEDIFF(DAY, @U2Data, @U1Data) +
													0.7 * DATEDIFF(DAY, @U1Data, @UData)

					 WHEN @U2Data IS NOT NULL THEN	
													0.2 * DATEDIFF(DAY, @U2Data, @U1Data) +
													0.7 * DATEDIFF(DAY, @U1Data, @UData)

					 WHEN @U1Data IS NOT NULL THEN	
													0.7 * DATEDIFF(DAY, @U1Data, @UData)

					 ELSE 1
				END,

			@eedFormula = 
				CASE WHEN @U3Data IS NOT NULL THEN 
													'EED = 0.1(U3Data - U2Data) + ' +
														  '0.2(U2Data - U1Data) + ' +
													      '0.7(U1Data - UData) <=> ' +
													'EED = 0.1(' + CONVERT(varchar, @U3Data, 103) + ' - ' + CONVERT(varchar, @U2Data, 103)  + ') + ' +
													      '0.2(' + CONVERT(varchar, @U2Data, 103) + ' - ' + CONVERT(varchar, @U1Data, 103)  + ') + ' +
														  '0.7(' + CONVERT(varchar, @U1Data, 103) + ' - ' + CONVERT(varchar, @UData, 103)  + ') <=> ' +
													'EED = ' + CAST(@eed as varchar)

					 WHEN @U2Data IS NOT NULL THEN	
													'EED = 0.2(U2Data - U1Data) + ' +
													      '0.7(U1Data - UData) <=> ' +
													'EED = 0.2(' + CONVERT(varchar, @U2Data, 103) + ' - ' + CONVERT(varchar, @U1Data, 103)  + ') + ' +
														  '0.7(' + CONVERT(varchar, @U1Data, 103) + ' - ' + CONVERT(varchar, @UData, 103)  + ') <=> ' +
													'EED = ' + CAST(@eed as varchar)

					 WHEN @U1Data IS NOT NULL THEN	
													'EED = 0.7(U1Data - UData) <=> ' +
													'EED = 0.7(' + CONVERT(varchar, @U1Data, 103) + ' - ' + CONVERT(varchar, @UData, 103)  + ') <=> ' +
													'EED = ' + CAST(@eed as varchar)

					 ELSE 'EED = 1 (default)'
			    END



	
	-- CE (ICD component calculation)
	IF @UData IS NOT NULL
	BEGIN
		
		-- CE calculation
		SELECT @ce = (1 - (DATEDIFF(DAY, @UData, GETDATE()) / @eed));

		-- CE formula calculation
		SELECT @ceFormula = 'CE = (1 - ((Hoje - UData) / EEP)) <=> ' +
							'CE = (1 - ((' + CONVERT(varchar, GETDATE(), 103) + ' - ' + CONVERT(varchar, @UData, 103) + ') / ' + CAST(@eed as varchar)  + ')) <=> ' +
							'CE = ' + CAST(@ce as varchar);
	END
	ELSE
	BEGIN
		-- default
		SELECT @ce = 0, 
			   @ceFormula = 'CE = 0 (default)';
	END




	-- CT (ICD component calculation)
	IF @U1Disponivel IS NOT NULL -- we have sure that @UDisponivel is not null too
	BEGIN

		-- CT calculation
		SELECT @ct = 
				   CASE WHEN @UDisponivel < @U1Disponivel THEN (1 - ABS(((@UDisponivel - @U1Disponivel) / 11) * 5))
						ELSE (1 - ABS((@UDisponivel - @U1Disponivel) / 11))
				   END;

		-- CT formula calculation
		SELECT @ctFormula = 
				   CASE WHEN @UDisponivel < @U1Disponivel THEN 
														'CT = (1 - |((UDisponivel - U1Disponivel) / 11) * 5|) <=> ' + 
														'CT = (1 - |(((' + CONVERT(varchar, @UDisponivel) + ') - (' + CONVERT(varchar, @U1Disponivel) + ')) / 11) * 5|) <=>' +
														'CT = ' + CAST(@ct as varchar)
						ELSE 
							  'CT = (1 - |(UDisponivel - U1Disponivel) / 11|) <=> ' + 
							  'CT = (1 - |((' + CONVERT(varchar, @UDisponivel) + ') - (' + CONVERT(varchar, @U1Disponivel) + ')) / 11|) <=>' +
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
							'CF = ' + CAST(@cf as varchar);

	END
	ELSE
	BEGIN
		-- default
		SELECT @cf = 0, 
			   @cfFormula = 'CF = 0 (default)';
	END




	-- ICD calculation
	SELECT @icd = (0.5 * @ce) + (0.2 * @ct) + (0.3 * @cf);

	-- ICD Formula calculation
	SELECT @icdFormula = 'ICD = 0.5CE + 0.2CT + 0.3CF <=> ' + 
						 'ICD = (0.5 * ' + CAST(@ce as varchar) + ') + (0.2 * ' + CAST(@ct as varchar) + ') + (0.3 * ' + CAST(@cf as varchar) + ') <=> ' +
						 'ICD = ' + CAST(@icd as varchar);

	-- ICD must be inside [0:1] interval
	SELECT @icdFormula =
					CASE WHEN @icd < 0 THEN @icdFormula + ', !E [0:1] => ICP = 0'
					     WHEN @icd > 1 THEN @icdFormula + ', !E [0:1] => ICP = 1'
						 ELSE @icdFormula
					END,
			@icd =
					CASE WHEN @icd < 0 THEN 0
					     WHEN @icd > 1 THEN 1
						 ELSE @icd
					END;
		  


	-- send back results
	INSERT INTO @results
	SELECT @u3vd, @u2vd, @u1vd, @eed, @icd, @eedFormula, @ceFormula, @ctFormula, @cfFormula, @icdFormula;


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
