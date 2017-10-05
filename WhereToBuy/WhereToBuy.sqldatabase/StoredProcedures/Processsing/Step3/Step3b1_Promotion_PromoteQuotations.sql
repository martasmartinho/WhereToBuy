/*
	This SP, disable old Actual Quotations and Promote just completed quotations to Actual Quotations
	This procedure is executed inside a transaction to ensure data integrity.
*/


CREATE PROCEDURE [dbo].[Step3b1_Promotion_PromoteQuotations]
	@Info nvarchar(256) OUTPUT
AS

	-- begin transacion
	BEGIN TRANSACTION


		BEGIN TRY

			/*
				disable all Actual Quotations that have a newly not integrated quotation (CotacoesNaoIntegradas)
			*/

			WITH CotacoesAtuaisDespromover AS
			(
				/*	Intersect all existing Atual Quotations with Not Integrated Quotations.
					The resulting set of this query represents all quotations that must be disabled, because
					there are newly quotations to take their places
				*/
				SELECT FornecedorCodigo, ProdutoCodigo FROM CotacoesAtuaisView
				INTERSECT
				SELECT FornecedorCodigo, ProdutoCodigo FROM CotacoesNaoIntegradasView
			)

			UPDATE c
			SET c.Inativo = CAST('true' as bit),
				c.Versao = GETDATE()
			FROM [dbo].[CotacoesAtuaisView] c
				 INNER JOIN CotacoesAtuaisDespromover cd
					   ON c.FornecedorCodigo = cd.FornecedorCodigo
					      AND c.ProdutoCodigo = cd.ProdutoCodigo


			-- fill output @info with sucess message
			SET @Info = '[Step3b1_Promotion_PromoteQuotations-01] Disabled Quotations: ' + CAST(@@ROWCOUNT as nvarchar);
			
			
			/*
				set all Not Integrated Quotations (CotacoesNaoIntegradasView) to integrated status.
				
				(at this moment, we have total sure that we can promote all Not Integrated Quotations (CotacoesNaoIntegradasView)
				ensuring that will exist one Actual Quotation for each FornecedorCodigo / ProdutoCodigo)
			*/

			UPDATE c
			SET c.Integrado = CAST('true' as bit),
				c.Versao = GETDATE()
			FROM [dbo].[CotacoesNaoIntegradasView] c


			-- fill output @info with sucess message
			SET @Info += '; Promoted Quotations: ' + CAST(@@ROWCOUNT as nvarchar);


			-- commit transaction
			COMMIT TRANSACTION;

			-- send true to output
			RETURN 1;

		END TRY

		BEGIN CATCH
			-- fill output @info with occurred error message
			SET @Info = '[Step3b1_Promotion_PromoteQuotations-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

			-- rollback transaction;
			ROLLBACK TRANSACTION;

			-- send false to output
			RETURN 0;

		END CATCH

RETURN;
