﻿/*
	This SP feeds MarcasMatching table with new rules, considering the new incoming quotations
*/


CREATE PROCEDURE [dbo].[Step2b1a_Mappings_Brands_FeedNewRules]
	@Info nvarchar(256) OUTPUT
AS
	
	-- new rules feeding
	BEGIN TRY

		/*
			Feed (...)Matching table with new incomplete rules, to simplify mapping work to the user.
		*/

		INSERT [dbo].[MarcasMatching] (FornecedorCodigo, Codigo, Descricao, MapTo, Inativo, Criacao, Versao)
	
		SELECT um.FornecedorCodigo, 
			   um.Codigo,
			   (
					-- get 1st existing description in CotacoesIncompletasView for this entity
					SELECT TOP 1 CASE WHEN ci._MarcaDescricao IS NULL THEN '(vazio)'
									  ELSE ci._MarcaDescricao
								 END
					FROM [dbo].[CotacoesIncompletasView] ci
					WHERE ci.FornecedorCodigo = um.FornecedorCodigo
						  AND ci._MarcaCodigo = um.Codigo
				    ORDER BY ci._MarcaDescricao DESC  -- prevents the existence of some _MarcaDescricao = NULL
		      ) AS [Descricao],
			  NULL, -- MapTo will be filled later by user
			  CAST('false' as bit),
			  GETDATE(),
			  GETDATE()
		FROM
			  (
					-- un-existing but needed matching rules
					SELECT DISTINCT ci.FornecedorCodigo, ci._MarcaCodigo AS Codigo
					FROM [dbo].[CotacoesIncompletasView] ci
					EXCEPT
					SELECT mm.FornecedorCodigo, mm.Codigo
					FROM [dbo].[MarcasMatching] mm
			  ) um
		



		-- fill output @info with sucess message
		SET @Info = '[Step2b1a_Mappings_Brands_FeedNewRules-01] Number of created matching rules: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2b1a_Mappings_Brands_FeedNewRules-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
