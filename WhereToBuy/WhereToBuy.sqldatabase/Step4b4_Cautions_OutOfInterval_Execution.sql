/*
	This stored procedure entends to inactivate Actual Quotations that has it's price out of the interval defined on correspondent category.
*/


CREATE PROCEDURE [dbo].[Step4b4_Cautions_OutOfInterval_Execution]
	@Info nvarchar(256) OUTPUT
AS

	BEGIN TRY

		/*
			Inactivate Actual Quotations that has a irregular price (Out Of Interval)
		*/
		UPDATE c
		
		SET c.Inativo = CAST('true' as bit),
			c.Versao = GETDATE()

		FROM [dbo].[CotacoesAtuaisPrecosForaIntervaloView] c



		-- fill output @info with sucess message
		SET @Info = '[Step4b2_Cautions_FakeStock_Execution-01] Number of executed Out Of Interval preventions: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step4b4_Cautions_OutOfInterval_Execution-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
