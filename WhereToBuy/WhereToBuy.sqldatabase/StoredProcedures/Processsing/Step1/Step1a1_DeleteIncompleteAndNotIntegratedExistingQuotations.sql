/*
	This SP deletes all Incomplete and Not Integrated Quotations from database
*/


CREATE PROCEDURE [dbo].[Step1a1_DeleteIncompleteAndNotIntegratedExistingQuotations]
	@FornecedorCodigo nvarchar(20),
	@Info nvarchar(256) OUTPUT
AS
	
	-- variables
	DECLARE @numberOfAffectedRows integer = 0;


	-- delete incomplete quotations
	BEGIN TRY

		-- remove all not integrated quotations from the supplier received on @FornecedorCodigo
		DELETE FROM [dbo].[CotacoesNaoIntegradasView] WHERE FornecedorCodigo = @FornecedorCodigo;

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;



		-- remove all incomplete quotations from the supplier too
		DELETE FROM [dbo].CotacoesIncompletasView WHERE [FornecedorCodigo] = @FornecedorCodigo

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;



		-- fill output @info with sucess message
		SET @Info = '[Step1a1_DeleteIncompleteAndNotIntegratedExistingQuotations-01] Were removed sucessfully, ' + CAST(@numberOfAffectedRows as nvarchar) + ' not integrated and incomplete quotations of the supplier ' + @FornecedorCodigo +'.';

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step1a1_DeleteIncompleteAndNotIntegratedExistingQuotations-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
