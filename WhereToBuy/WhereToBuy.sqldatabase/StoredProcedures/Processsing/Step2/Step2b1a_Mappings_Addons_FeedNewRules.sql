/*
	This SP feeds Addons Matching table with new rules, considering the new incoming quotations
*/


CREATE PROCEDURE [dbo].[Step2b1a_Mappings_Addons_FeedNewRules]
	@Info nvarchar(256) OUTPUT
AS
	
	-- new rules feeding
	BEGIN TRY

		/*
			Feed (..)Matching table with new incomplete rules, to simplify mapping work to the user.
		*/
		INSERT [dbo].[ComplementosMatching] (FornecedorCodigo, Codigo, Descricao, MapTo, Inativo, Criacao, Versao)
	
		SELECT um.FornecedorCodigo, 
			   um.Codigo,
			   (
					-- get 1st existing description in CotacoesIncompletasView for this entity
					SELECT TOP 1 CASE WHEN ci._ComplementoDescricao IS NULL THEN '(vazio)'
									  ELSE ci._ComplementoDescricao
								 END
					FROM [dbo].[CotacoesIncompletasView] ci
					WHERE ci.FornecedorCodigo = um.FornecedorCodigo
						  AND ci._ComplementoCodigo = um.Codigo
				    ORDER BY ci._ComplementoDescricao DESC  -- prevents the existence of some _ComplementoDescricao = NULL
		      ) AS [Descricao],
			  NULL, -- MapTo will be filled later by user
			  CAST('false' as bit),
			  GETDATE(),
			  GETDATE()
		FROM
			  (
					-- un-existing but needed matching rules
					SELECT DISTINCT ci.FornecedorCodigo, ci._ComplementoCodigo AS Codigo
					FROM [dbo].[CotacoesIncompletasView] ci
					EXCEPT
					SELECT cm.FornecedorCodigo, cm.Codigo
					FROM [dbo].[ComplementosMatching] cm
			  ) um

		



		-- fill output @info with sucess message
		SET @Info = '[Step2b1a_Mappings_Addons_FeedNewRules-01] Number of created matching rules: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2b1a_Mappings_Addons_FeedNewRules-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
