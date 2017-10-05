/*
	This SP, set all filled quotations with the complete flat (Completo = true)
*/


CREATE PROCEDURE [dbo].[Step2d2_Normalization_SetComplete]
	@Info nvarchar(256) OUTPUT
AS

	BEGIN TRY

		/*
			Set complete all filled quotations
		*/
		UPDATE ci
		SET ci.Completo = CAST('true' as bit),
			ci.Versao = GETDATE()


		/*  Using CotacoesIncompletasFinalizacaoView, I'm ensuring that I am working only with Incomplete Quotations  
			that are ready to be settled as Completo = true
		*/
		FROM [dbo].[CotacoesIncompletasFinalizacaoView] ci



		-- fill output @info with sucess message
		SET @Info = '[Step2d2_Normalization_SetComplete-01] Number of quotations settled as completed: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2d2_Normalization_SetComplete-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
