/*
	This SP, perform Addons Mapping of incomplete Quotations.
*/


CREATE PROCEDURE [dbo].[Step2b1b_Mappings_Addons_Map]
	@Info nvarchar(256) OUTPUT
AS
	
	-- begin matching
	BEGIN TRY

		/*
			Map entities considering (...)Matching rules.
		*/
		UPDATE [dbo].[CotacoesIncompletasView]
		SET ComplementoCodigo = cm.MapTo,
			Versao = GETDATE()

		/*  Using (...)IncompletasView I am assuring that I'm only working with Cotacoes where Inactivo='false' and Completo='false'
			(although, with this mapping lack, never the quotation whould be completed - so this control is redundant)
		*/
		FROM [dbo].[CotacoesIncompletasView] ci
			INNER JOIN [dbo].[ComplementosMatching] cm
				ON ci.FornecedorCodigo = cm.FornecedorCodigo
					AND ci._ComplementoCodigo = cm.Codigo

		-- filter only active matching rules with MapTo already settled, and not mapped incomplete quotations
		WHERE cm.Inativo = CAST('false' as bit)
			  AND ci.ComplementoCodigo IS NULL
			  AND cm.MapTo IS NOT NULL



		-- fill output @info with sucess message
		SET @Info = '[Step2b1b_Mappings_Addons_Map-01] Number of mapped quotations: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2b1b_Mappings_Addons_Map-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
