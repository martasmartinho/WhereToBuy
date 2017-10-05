/*
	This SP, set as descontinued all products that don't have any Actual Quotation (CotacoesAtuais).
	If a product doesn't have any Actual Quotation, that means that it is not possible to sell it any more.
*/


CREATE PROCEDURE [dbo].[Step4a4_Finishing_ProductDescontinuation]
	@Info nvarchar(256) OUTPUT
AS

	BEGIN TRY

		/*
			Set descontinued all products without Actual Quotations.
		*/
		UPDATE p
		SET p.Descontinuado = CAST('true' as bit),
			p.Versao = GETDATE()

		FROM [dbo].[ProdutosAtuaisView] p

		-- it is not necessary to filter only inativo='false' and descontinuado='false' because ProdutosAtuaisView already filter it
		WHERE p.Codigo NOT IN 
							  (
									SELECT DISTINCT ProdutoCodigo FROM [dbo].[CotacoesAtuaisView]
							  )



		-- fill output @info with sucess message
		SET @Info = '[Step4a4_Finishing_ProductDescontinuation-01] Number of Descontinued Products: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step4a4_Finishing_ProductDescontinuation-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
