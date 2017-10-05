/*
	This SP remove from database all expired and inactive quotations
*/


CREATE PROCEDURE [dbo].[Step2a2_Preparation_DeleteExpiredQuotations]
	@Info nvarchar(256) OUTPUT
AS
	
	-- clean old quotations
	BEGIN TRY

		-- remove all expired and inactive Cotacoes
		DELETE FROM [dbo].[Cotacoes] WHERE Inativo = CAST('true' as bit) AND Validade < DATEADD(MONTH, -3, GETDATE())

		-- fill output @info with sucess message
		SET @Info = '[Step2a2_Preparation_DeleteExpiredQuotations-01] Limpeza com sucesso das cotacoes inativas fora de validade (+3 meses)';

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2a2_Preparation_DeleteExpiredQuotations-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
