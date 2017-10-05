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
PRINT N'Altering [dbo].[CotacoesAtuaisAmplitudeExcedidaView]...';


GO
/*
	This view simplifies the process of determining what quotations do not respect the amplitude defined on it's category.
	The set of quotations represented by this view, are the quotations that have problems with amplitude defined on each category.
*/

ALTER VIEW [dbo].[CotacoesAtuaisAmplitudeExcedidaView]
	AS 

	SELECT c.ProdutoCodigo,


		   -- price amplitude of this product
		   MIN(c.PrecoCusto) / MAX(c.PrecoCusto) AS AmplitudePercentual,
		   
		   
		   -- how the deviation of the amplitude has been calculated (formula)
		   '(min/max) < A  <=>  ' +
		   '(' + CAST(MIN(c.PrecoCusto) as nvarchar) + '/' + CAST(MAX(c.PrecoCusto) as nvarchar) + ')  <  ' + CAST(t.PrecoAmplitudeMax as nvarchar) + '  <=>  ' +
		    CAST(ROUND(MIN(c.PrecoCusto) / MAX(c.PrecoCusto), 2) as nvarchar) + '  <  ' + CAST(t.PrecoAmplitudeMax as nvarchar) 
			AS AmplitudeFormulaDetecaoDesfasamento,


			-- calculate which price (min or max) is offset when compared with the average
			CASE 
				 -- if diference between min and avg is greater then the avg to the max
				 WHEN ABS(MIN(c.PrecoCusto) - AVG(c.PrecoCusto)) > (MAX(c.PrecoCusto) - AVG(c.PrecoCusto)) 
							THEN MIN(c.PrecoCusto)

				 -- if diference between min and avg is less then the avg to the max
				 WHEN ABS(MIN(c.PrecoCusto) - AVG(c.PrecoCusto)) < (MAX(c.PrecoCusto) - AVG(c.PrecoCusto)) 
							THEN MAX(c.PrecoCusto)

				 -- on the other cases (equal) there isn't any offset
				 ELSE NULL
			END AS PrecoDesfasado,

			
			-- detecting the offset price (it is min or max the most distant from the average)
			CASE 
				 -- if diference between min and avg is greater then the avg to the max
				 WHEN ABS(MIN(c.PrecoCusto) - AVG(c.PrecoCusto)) > (MAX(c.PrecoCusto) - AVG(c.PrecoCusto)) 
							THEN '| min-avg | > (max-avg)  <=>  ' +
							     '| ' + CAST(MIN(c.PrecoCusto) as nvarchar) + '-' + CAST(AVG(c.PrecoCusto) as nvarchar) + ' | > (' + CAST(MAX(c.PrecoCusto) as nvarchar) + '-' + CAST(AVG(c.PrecoCusto) as nvarchar) + ')  <=>  ' +
								 CAST(ABS(MIN(c.PrecoCusto) - AVG(c.PrecoCusto)) as nvarchar) +' > ' + CAST((MAX(c.PrecoCusto) - AVG(c.PrecoCusto)) as nvarchar) + ' **preço mais barato é o mais afastado da média!'
								 
				-- if diference between min and avg is less then the avg to the max
				 WHEN ABS(MIN(c.PrecoCusto) - AVG(c.PrecoCusto)) < (MAX(c.PrecoCusto) - AVG(c.PrecoCusto)) 
							THEN '| min-avg | < (max-avg)  <=>  ' +
							     '| ' + CAST(MIN(c.PrecoCusto) as nvarchar) + '-' + CAST(AVG(c.PrecoCusto) as nvarchar) + ' | < (' + CAST(MAX(c.PrecoCusto) as nvarchar) + '-' + CAST(AVG(c.PrecoCusto) as nvarchar) + ')  <=>  ' +
								 CAST(ABS(MIN(c.PrecoCusto) - AVG(c.PrecoCusto)) as nvarchar) +' < ' + CAST((MAX(c.PrecoCusto) - AVG(c.PrecoCusto)) as nvarchar) + ' **preço mais caro é o mais afastado da média!'
					
				-- on the other cases (equal) there isn't any offset
				 ELSE NULL
				 
			END AS PrecoDesfasadoFormulaDecisao
			

	FROM [dbo].[CotacoesAtuaisView] c
		 INNER JOIN [dbo].[Categorias] t
			   ON c.CategoriaCodigo = t.Codigo
		 INNER JOIN [dbo].[ProdutosMatching] m
			   ON c.FornecedorCodigo = m.FornecedorCodigo
			      AND c.ComplementoCodigo = m.ComplementoCodigo
				  AND c._ProdutoCodigo = m.Codigo


	-- we want filter only quotations which ProductMatching rules are defined to not prevent PreçosDesfazados
	WHERE m.DispensaPrevencaoPrecosDesfasados = CAST('false' as bit)


	-- preform a group by each ProdutoCodigo
	GROUP BY c.ProdutoCodigo, t.PrecoAmplitudeMax


	-- filter only groups with irregular amplitude 
	-- and the product has more than two quotations to be compared (only that way this prevention makes sense)
	HAVING (MIN(c.PrecoCusto) / MAX(c.PrecoCusto)) < t.PrecoAmplitudeMax
		   AND COUNT('-') > 2
GO
PRINT N'Altering [dbo].[CotacoesAtuaisAmplitudeIrregularView]...';


GO
/*
	Quotations of this view will be configured as disabled because it's offset price...
*/

ALTER VIEW [dbo].[CotacoesAtuaisAmplitudeIrregularView]
	AS 

	SELECT c.*,
		   ca.AmplitudePercentual, ca.AmplitudeFormulaDetecaoDesfasamento, ca.PrecoDesfasado, ca.PrecoDesfasadoFormulaDecisao

	FROM [dbo].[CotacoesAtuaisAmplitudeExcedidaView] ca
		 INNER JOIN [dbo].[CotacoesAtuaisView] c
			   ON ca.ProdutoCodigo = c.ProdutoCodigo
				  AND ca.PrecoDesfasado = c.PrecoCusto

	-- consider only Quotations that the "PrecoDesfazado" is settled (exists a price offset).
	WHERE ca.PrecoDesfasado IS NOT NULL
GO
PRINT N'Altering [dbo].[CotacoesAtuaisPrecosForaIntervaloView]...';


GO
/*
	This view simplifies the process of determining what quotations have price out of the category interval...
*/

ALTER VIEW [dbo].[CotacoesAtuaisPrecosForaIntervaloView]
	AS 

	SELECT c.*,
		   t.PrecoMinimoPermitido, t.PrecoMaximoPermitido

	FROM [dbo].[CotacoesAtuaisView] c
		 INNER JOIN [dbo].[Categorias] t
			   ON c.CategoriaCodigo = t.Codigo
		 INNER JOIN [dbo].[ProdutosMatching] m
			   ON c.FornecedorCodigo = m.FornecedorCodigo
				  AND c.ComplementoCodigo = m.ComplementoCodigo
				  AND c._ProdutoCodigo = m.Codigo

	-- filter all Actual Quotations that the price is out of the interval defined on category,
	-- considering only ProductMatching rules that are configured with DispensaPrevencaoPrecosDesfazados = 'false'
	WHERE (c.PrecoCusto < t.PrecoMinimoPermitido OR c.PrecoCusto > t.PrecoMaximoPermitido)
		  AND m.DispensaPrevencaoPrecosDesfasados = CAST('false' as bit)
GO
PRINT N'Altering [dbo].[Step2c1_MappingProducts_FeedNewRules]...';


GO
/*
	This SP feeds Products Matching rules (ProdutosMatching) with new rules to be filled manually by user
	or automatic (specific to products matching) later in algorithm process.
*/


ALTER PROCEDURE [dbo].[Step2c1_MappingProducts_FeedNewRules]
	@Info nvarchar(256) OUTPUT
AS
	
	-- new rules feeding
	BEGIN TRY

		/*
			Feed (...)Matching table with new incomplete rules, to simplify mapping work to the user or to be automatically mapped later.
		*/
		INSERT [dbo].[ProdutosMatching] (FornecedorCodigo, ComplementoCodigo, Codigo, Descricao, MapTo, 
			   HorasValidadeCotacao, StockCodigoSubstituto, DispensaPrevencaoPrecosDesfasados, DispensaPrevencaoFalsoStock, DataReset, Notas,
			   Inativo, Criacao, Versao)

		SELECT um.FornecedorCodigo,
			   um.ComplementoCodigo, 
			   um.Codigo,
			   (
					-- get 1st existing description in CotacoesIncompletasMapeamentoProdutoView for this product
					SELECT TOP 1 CASE WHEN ci._Descricao IS NULL THEN '(vazio)'
									  ELSE ci._Descricao
								 END
					FROM [dbo].[CotacoesIncompletasMapeamentoProdutoView] ci
					WHERE ci.FornecedorCodigo = um.FornecedorCodigo
						  AND ci.ComplementoCodigo = um.ComplementoCodigo
						  AND ci._ProdutoCodigo = um.Codigo
				    ORDER BY ci._Descricao DESC  -- prevents the existence of some _Descricao = NULL
		      ) AS [Descricao],
			  NULL,		-- MapTo will be filled later by user or automatic next in the algorithm (specific to products)
			  NULL,		-- this null means that prevails the rules defined on CotacoesRegras. (to automatic set as inative all quotations for this supplier / product, set this value to -1)
			  NULL,		-- use this field if you want to specify a stock position directly to the product. this rule prevails to CotacoesRegras rules
			  CAST('false' as bit),	-- by default all products must be tested to offset prices..
			  CAST('false' as bit),	-- by default all products must be tested on FakeStock prevention
			  NULL,		-- Date to auto reset this custom definitions
			  NULL,
			  CAST('false' as bit),
			  GETDATE(),
			  GETDATE()
		FROM
			  (
					-- un-existing but needed matching rules
					SELECT DISTINCT ci.FornecedorCodigo, ci.ComplementoCodigo, ci._ProdutoCodigo AS Codigo
					FROM [dbo].[CotacoesIncompletasMapeamentoProdutoView] ci
					EXCEPT
					SELECT pm.FornecedorCodigo, pm.ComplementoCodigo, pm.Codigo
					FROM [dbo].[ProdutosMatching] pm
			  ) um



		-- fill output @info with sucess message
		SET @Info = '[Step2c1_MappingProducts_FeedNewRules-01] Number of created matching rules: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2c1_MappingProducts_FeedNewRules-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
GO
PRINT N'Altering [dbo].[Step3a1_Preparation_ProductMatchingsReset]...';


GO
/*
	This SP, resets all Product Matching rules (ProdutosMatching table) to it's defaults when date on DataReset field is reached.
*/


ALTER PROCEDURE [dbo].[Step3a1_Preparation_ProductMatchingsReset]
	@Info nvarchar(256) OUTPUT
AS

	BEGIN TRY

		/*
			Reset to default Product Matching rules with DataReset date <= GETDATE()
		*/
		
		UPDATE pm

			-- restablish defaults
		SET pm.HorasValidadeCotacao = NULL,
		    pm.StockCodigoSubstituto = NULL,
			pm.DispensaPrevencaoPrecosDesfasados = CAST('false' as bit),
			
			-- if there are some notes already settled on row do not remove them (append text at begining)
			pm.Notas =
					CASE WHEN pm.Notas IS NULL THEN '!reset (' + CONVERT(varchar, GETDATE(), 120) + ')'
					     ELSE SUBSTRING('!reset (' + CONVERT(varchar, GETDATE(), 120) + ') | ' + pm.Notas, 1, 256)
					END,

			-- set DataReset to default to
			pm.DataReset = NULL,

			-- thats a new row version
			pm.Versao = GETDATE()


		/*  Join to Suppliers table is not necessary at the moment, but could make sense in future to retreive defaults from
			Supplier data
		*/
		FROM [dbo].[ProdutosMatching] pm
			 INNER JOIN [dbo].[Fornecedores] f
				   ON pm.FornecedorCodigo = f.Codigo

		-- filter all rules with a DataReset settled with a date <= today...
		WHERE pm.DataReset IS NOT NULL
			  AND pm.DataReset <= GETDATE()




		-- fill output @info with sucess message
		SET @Info = '[Step3a1_Preparation_ProductMatchingsReset-01] Number of ProductMatching rules reseted to default: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step3a1_Preparation_ProductMatchingsReset-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
GO
PRINT N'Creating [dbo].[Step4b5_IrregularAmplitude_Notifications]...';


GO
/*
	This SP makes sure that user is notified via CotacoesAvisos table, that some actual quotations were disabled becouse the
	it's price is causing an irregular price amplitude (for the same product)
*/

CREATE PROCEDURE [dbo].[Step4b5_IrregularAmplitude_Notifications]
	@Info nvarchar(256) OUTPUT
AS
	
	-- insert notifications on CotacoesAvisos table
	BEGIN TRY

		/*
			Insert notifications rows for all disabled quotations
		*/
		



		-- fill output @info with sucess message
		SET @Info = '[Step4b5_IrregularAmplitude_Notifications-01] Number of inserted notifications: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step4b5_IrregularAmplitude_Notifications-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
GO
PRINT N'Refreshing [dbo].[Step4b3_OutOfInterval_Notifications]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Step4b3_OutOfInterval_Notifications]';


GO
PRINT N'Refreshing [dbo].[Step4b4_OutOfInterval_Execution]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[Step4b4_OutOfInterval_Execution]';


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
GO

GO
PRINT N'Update complete.';


GO
