/*
	This SP, set as not descontinued all descontinued products that back to have Actual Quotations (CotacoesAtuais).
	If a product no longer have quotations (CotacoesAtuais), that means that the product is been descontinued. Hoever, if it
	comes back having quotations later, the product must be ressurected..
*/


CREATE PROCEDURE [dbo].[Step4a5_Finishing_ProductResurrection]
	@Info nvarchar(256) OUTPUT
AS

	BEGIN TRY

		/*
			Set as ressurected (not descontinued) all descontinued products that have Actual Quotations (again).
		*/

		UPDATE p
		SET p.Descontinuado = CAST('false' as bit),
			p.Versao = GETDATE()

		FROM [dbo].[Produtos] p
		WHERE p.Inativo = CAST('false' as bit)
			  AND p.Descontinuado = CAST('true' as bit)
			  AND p.Codigo IN 
						  (
							   SELECT DISTINCT ProdutoCodigo FROM [dbo].[CotacoesAtuaisView]
						  )


		-- fill output @info with sucess message
		SET @Info = '[Step4a5_Finishing_ProductResurrection-01] Number of Resurrected Products: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step4a5_Finishing_ProductResurrection-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
