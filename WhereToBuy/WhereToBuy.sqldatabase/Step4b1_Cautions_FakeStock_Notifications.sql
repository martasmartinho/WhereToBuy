/*
	This SP makes sure that user is notified via CotacoesAvisos table, that some actual quotations were updated with
	new StockCodigoSubsituto due to date evolution and configuration of percentil codes on Stocks table.
*/

CREATE PROCEDURE [dbo].[Step4b1_Cautions_FakeStock_Notifications]
	@Info nvarchar(256) OUTPUT
AS
	
	-- insert notifications on CotacoesAvisos table
	BEGIN TRY

		/*
			Insert notifications rows for all quotations that can have a fake stock classification
		*/
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID(), 
			   c.FornecedorCodigo,
			   c.Data,
			   c._ProdutoCodigo,
			   c._ComplementoCodigo,
			   'PFS' AS AvisoTipoCodigo,
			   CASE WHEN (GETDATE() >= c.P90) AND (c.ValidadeP90_StockCodigo IS NOT NULL) THEN 
								'!Prevenção[FalsoStock] (' + CONVERT(varchar, GETDATE(), 103) + ') P90 atingido! '
					
					WHEN (GETDATE() >= c.P80) AND (c.ValidadeP80_StockCodigo IS NOT NULL) THEN
								'!Prevenção[FalsoStock] (' + CONVERT(varchar, GETDATE(), 103) + ') P80 atingido! '
					
					WHEN (GETDATE() >= c.P70) AND (c.ValidadeP70_StockCodigo IS NOT NULL) THEN
								'!Prevenção[FalsoStock] (' + CONVERT(varchar, GETDATE(), 103) + ') P70 atingido! '

					WHEN (GETDATE() >= c.P60) AND (c.ValidadeP60_StockCodigo IS NOT NULL) THEN
								'!Prevenção[FalsoStock] (' + CONVERT(varchar, GETDATE(), 103) + ') P60 atingido! '

					WHEN (GETDATE() >= c.P50) AND (c.ValidadeP50_StockCodigo IS NOT NULL) THEN
								'!Prevenção[FalsoStock] (' + CONVERT(varchar, GETDATE(), 103) + ') P50 atingido! '

					ELSE c.StockCodigoSubstitutoJustificacao
			   END,

			   GETDATE() AS Criacao

		-- consider only quotations filtered to FakeStock prevention
		FROM [dbo].[CotacoesAtuaisFalsoStockView] c



		-- fill output @info with sucess message
		SET @Info = '[Step4b1_Cautions_FakeStock_Notifications-01] Number of inserted notifications: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step4b1_Cautions_FakeStock_Notifications-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
