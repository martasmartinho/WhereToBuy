/*
	This SP, fills the MapTo field of de ProdutosMatching of suppliers autorized to automatic mapping.
*/


CREATE PROCEDURE [dbo].[Step2c3_MappingProducts_FillMapTo]
	@Info nvarchar(256) OUTPUT
AS
	
	-- begin fill MapTo fields of ProdutosMatching tasble
	BEGIN TRY

		/*
			begin automatic fill of MapTo fields
		*/
		UPDATE [dbo].[ProdutosMatching]
		SET MapTo = c.Referencia,
			Versao = GETDATE()

		/*  CotacoesMatrizPreenchimentoMapToView ensures that only suppliers autorized to automatic Matching are considered int this process
		    and only existing products (in products table) are setted.
		*/
		FROM [dbo].[ProdutosMatching] pm
			 INNER JOIN [dbo].[CotacoesMatrizPreenchimentoMapToView] c
				ON pm.FornecedorCodigo = c.FornecedorCodigo
				   AND pm.ComplementoCodigo = c.ComplementoCodigo
				   AND pm.Codigo = c._ProdutoCodigo

		-- filter only matching rules with a not set MapTo
		WHERE pm.MapTo IS NULL



		-- fill output @info with sucess message
		SET @Info = '[Step2c3_MappingProducts_FillMapTo-01] Number of ProdutosMatching, filled with a MapTo: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2c3_MappingProducts_FillMapTo-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
