/*
	This SP cleans all rows from CotacoesAvisos Table
*/


CREATE PROCEDURE [dbo].[Step2a1_Preparation_CleanAllExistingWarnings]
	@Info nvarchar(256) OUTPUT
AS
	
	-- CLEAN TABLE
	BEGIN TRY

		-- clean CotacoesAvisos
		TRUNCATE Table [dbo].[CotacoesAvisos]

		-- fill output @info with sucess message
		SET @Info = '[Step2a1_Preparation_CleanAllExistingWarnings-01] Limpeza com sucesso da tabela CotacoesAvisos';

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2a1_Preparation_CleanAllExistingWarnings-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' +ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
