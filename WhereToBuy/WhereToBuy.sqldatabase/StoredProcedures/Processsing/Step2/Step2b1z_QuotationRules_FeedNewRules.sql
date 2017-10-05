/*
	This SP feeds Quotations Rues (CotacoesRegras) table with new rules, considering the new just mapped quotations
*/


CREATE PROCEDURE [dbo].[Step2b1z_QuotationRules_FeedNewRules]
	@Info nvarchar(256) OUTPUT
AS
	
	-- new rules feeding
	BEGIN TRY

		/*
			Feed CotacoesRegras table with new incomplete rules. 
			Consider only Quotations that are already mapped.
		*/
		
		INSERT [dbo].[CotacoesRegras] (FornecedorCodigo, MarcaCodigo, CategoriaCodigo, StockCodigo, HorasValidade, StockCodigoSubstituto, DataReset, Notas, Versao)

		SELECT um.FornecedorCodigo, 
			   um.MarcaCodigo,
			   um.CategoriaCodigo,
			   um.StockCodigo, 
			   f.HorasValidadeSugestao,
			   NULL,
			   NULL,
			   NULL,
			   GETDATE()

		FROM
			  (
					-- un-existing but needed Quotation Rules (CotacoesRegras)
					SELECT DISTINCT ci.FornecedorCodigo, ci.MarcaCodigo, CategoriaCodigo, StockCodigo
					FROM [dbo].[CotacoesIncompletasView] ci
					WHERE MarcaCodigo IS NOT NULL
						  AND CategoriaCodigo IS NOT NULL
						  AND StockCodigo IS NOT NULL
						  AND ImpostoCodigo IS NOT NULL
						  AND ComplementoCodigo IS NOT NULL
						  AND EstadoCodigo IS NOT NULL

					EXCEPT

					SELECT cr.FornecedorCodigo, cr.MarcaCodigo, cr.CategoriaCodigo, cr.StockCodigo
					FROM [dbo].[CotacoesRegras] cr
			  ) um

			INNER JOIN [dbo].[Fornecedores] f
				ON um.FornecedorCodigo = f.Codigo



		-- fill output @info with sucess message
		SET @Info = '[Step2b1z_QuotationRules_FeedNewRules-01] Number of created rules: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2b1z_QuotationRules_FeedNewRules-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
