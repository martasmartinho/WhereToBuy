/*
	This stored procedure entends to lower stock availability on all quotations that have more than 2/3 of it's valid life.
	Every Actual Quotations that have more than 2/3 of its life (to expire), must be reconfigured with a more secure availability stock code.
*/


CREATE PROCEDURE [dbo].[Step4b2_Cautions_FakeStock_Execution]
	@Info nvarchar(256) OUTPUT
AS

	BEGIN TRY

		/*
			Update quotations with a new Stock Code to prevent a fack stock promotion.
		*/
		UPDATE c

		SET StockCodigoSubstitutoJustificacao =
				CASE WHEN (GETDATE() >= c.P90) AND (c.ValidadeP90_StockCodigo IS NOT NULL) THEN 
								SUBSTRING('!Prevenção[FalsoStock] (' + CONVERT(varchar, GETDATE(), 103) + ') P90 atingido! ' + COALESCE(c.StockCodigoSubstitutoJustificacao, ''), 1, 256)

					 WHEN (GETDATE() >= c.P80) AND (c.ValidadeP80_StockCodigo IS NOT NULL) THEN
								SUBSTRING('!Prevenção[FalsoStock] (' + CONVERT(varchar, GETDATE(), 103) + ') P80 atingido! ' + COALESCE(c.StockCodigoSubstitutoJustificacao, ''), 1, 256)

					 WHEN (GETDATE() >= c.P70) AND (c.ValidadeP70_StockCodigo IS NOT NULL) THEN
								SUBSTRING('!Prevenção[FalsoStock] (' + CONVERT(varchar, GETDATE(), 103) + ') P70 atingido! ' + COALESCE(c.StockCodigoSubstitutoJustificacao, ''), 1, 256)

					 WHEN (GETDATE() >= c.P60) AND (c.ValidadeP60_StockCodigo IS NOT NULL) THEN
								SUBSTRING('!Prevenção[FalsoStock] (' + CONVERT(varchar, GETDATE(), 103) + ') P60 atingido! ' + COALESCE(c.StockCodigoSubstitutoJustificacao, ''), 1, 256)

					 WHEN (GETDATE() >= c.P50) AND (c.ValidadeP50_StockCodigo IS NOT NULL) THEN
								SUBSTRING('!Prevenção[FalsoStock] (' + CONVERT(varchar, GETDATE(), 103) + ') P50 atingido! ' + COALESCE(c.StockCodigoSubstitutoJustificacao, ''), 1, 256)

					 ELSE c.StockCodigoSubstitutoJustificacao
				END,
			
			StockCodigoSubstituto =
				CASE WHEN (GETDATE() >= c.P90) AND (c.ValidadeP90_StockCodigo IS NOT NULL) THEN 
								c.ValidadeP90_StockCodigo

					 WHEN (GETDATE() >= c.P80) AND (c.ValidadeP80_StockCodigo IS NOT NULL) THEN 
								c.ValidadeP80_StockCodigo

					 WHEN (GETDATE() >= c.P70) AND (c.ValidadeP70_StockCodigo IS NOT NULL) THEN 
								c.ValidadeP70_StockCodigo

					 WHEN (GETDATE() >= c.P60) AND (c.ValidadeP60_StockCodigo IS NOT NULL) THEN 
								c.ValidadeP60_StockCodigo

					 WHEN (GETDATE() >= c.P50) AND (c.ValidadeP50_StockCodigo IS NOT NULL) THEN 
								c.ValidadeP50_StockCodigo

					 ELSE c.StockCodigoSubstituto
				END,
			
			c.Versao = GETDATE()

		FROM [dbo].[CotacoesAtuaisFalsoStockView] c




		-- fill output @info with sucess message
		SET @Info = '[Step4b2_Cautions_FakeStock_Execution-01] Number of executed FakeStock preventions: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step4b2_Cautions_FakeStock_Execution-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
