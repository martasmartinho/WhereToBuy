/*
	This SP, resets all Quotation Rules (CotacoesRegras table) to it's defaults when date on DataReset field is reached.
*/


CREATE PROCEDURE [dbo].[Step3a2_Preparation_QuotationRulesReset]
	@Info nvarchar(256) OUTPUT
AS

	BEGIN TRY

		/*
			Reset to default Quotation Rules with DataReset date <= GETDATE()
		*/
		
		UPDATE cr

			-- restablish defaults
		SET cr.HorasValidade = f.HorasValidadeSugestao,
			cr.StockCodigoSubstituto = NULL,
			
			-- if there are some notes already settled on row do not remove them (append text at begining)
			cr.Notas =
					CASE WHEN cr.Notas IS NULL THEN '!reset (' + CONVERT(varchar, GETDATE(), 120) + ')'
					     ELSE SUBSTRING('!reset (' + CONVERT(varchar, GETDATE(), 120) + ') | ' + cr.Notas, 1, 256)
					END,
		
			-- set DataReset to default to
			cr.DataReset = NULL,

			-- thats a new row version
			cr.Versao = GETDATE()


		FROM [dbo].[CotacoesRegras] cr
			 INNER JOIN [dbo].[Fornecedores] f
				   ON cr.FornecedorCodigo = f.Codigo

		-- filter all rules with a DataReset settled with a date <= today...
		WHERE cr.DataReset IS NOT NULL
			  AND cr.DataReset <= GETDATE()





		-- fill output @info with sucess message
		SET @Info = '[Step3a2_Preparation_QuotationRulesReset-01] Number of Quotation Rules reseted to default: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step3a2_Preparation_QuotationRulesReset-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
