/*
	This stored procedure entends to inactivate Actual Quotations with irregular amplitude quotations (most irregular price grouping by ProdutoCodigo).
*/


CREATE PROCEDURE [dbo].[Step4b6_Cautions_IrregularAmplitude_Execution]
	@Info nvarchar(256) OUTPUT
AS

	BEGIN TRY

		/*
			Inactivate Actual Quotations that has a irregular price (Out Of Interval)
		*/
		UPDATE c
		
		SET c.Inativo = CAST('true' as bit),
			c.Versao = GETDATE()

		FROM [dbo].[CotacoesAtuaisAmplitudeIrregularView] c



		-- fill output @info with sucess message
		SET @Info = '[Step4b6_Cautions_IrregularAmplitude_Execution-01] Number of executed Irregular Amplitude preventions: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step4b6_Cautions_IrregularAmplitude_Execution-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
