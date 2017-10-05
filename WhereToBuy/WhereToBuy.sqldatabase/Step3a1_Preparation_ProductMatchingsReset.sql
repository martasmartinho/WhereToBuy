/*
	This SP, resets all Product Matching rules (ProdutosMatching table) to it's defaults when date on DataReset field is reached.
*/


CREATE PROCEDURE [dbo].[Step3a1_Preparation_ProductMatchingsReset]
	@Info nvarchar(256) OUTPUT
AS

	BEGIN TRY

		/*
			Reset to default Product Matching rules with DataReset date <= GETDATE()
		*/
		
		UPDATE pm

			-- restablish defaults
		SET pm.HorasValidadeCotacao = NULL,
		    pm.StockCodigoSubstituto = NULL,
			pm.DispensaPrevencaoPrecosDesfasados = CAST('false' as bit),
			
			-- if there are some notes already settled on row do not remove them (append text at begining)
			pm.Notas =
					CASE WHEN pm.Notas IS NULL THEN '!reset (' + CONVERT(varchar, GETDATE(), 120) + ')'
					     ELSE SUBSTRING('!reset (' + CONVERT(varchar, GETDATE(), 120) + ') | ' + pm.Notas, 1, 256)
					END,

			-- set DataReset to default to
			pm.DataReset = NULL,

			-- thats a new row version
			pm.Versao = GETDATE()


		/*  Join to Suppliers table is not necessary at the moment, but could make sense in future to retreive defaults from
			Supplier data
		*/
		FROM [dbo].[ProdutosMatching] pm
			 INNER JOIN [dbo].[Fornecedores] f
				   ON pm.FornecedorCodigo = f.Codigo

		-- filter all rules with a DataReset settled with a date <= today...
		WHERE pm.DataReset IS NOT NULL
			  AND pm.DataReset <= GETDATE()




		-- fill output @info with sucess message
		SET @Info = '[Step3a1_Preparation_ProductMatchingsReset-01] Number of ProductMatching rules reseted to default: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step3a1_Preparation_ProductMatchingsReset-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
