/*
	This SP set as inactive all expired quotations on Cotacoes table.
	It is executed only in this step, because Validade field could be updated till last instruction (OtherFieldsRefresh).
*/


CREATE PROCEDURE [dbo].[Step4a3_Preparation_SetInactiveExpiredQuotations]
	@Info nvarchar(256) OUTPUT
AS
	
	-- change expired quotations inactive field value
	BEGIN TRY

		-- set inactive all expired quotations
		UPDATE c
		SET c.Inativo = CAST('true' as bit),
		    c.Versao = GETDATE()
		FROM [dbo].[CotacoesAtuaisView] c
		WHERE Validade < GETDATE();


		-- fill output @info with sucess message
		SET @Info = '[Step4a3_Preparation_SetInactiveExpiredQuotations-01] Inativação de quotações expiradas realizada com sucesso';

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step4a3_Preparation_SetInactiveExpiredQuotations-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
