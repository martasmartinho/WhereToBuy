/*
	Notifications SP: 3 of 3   (inactive products)

	This SP makes sure that user is notified via CotacoesAvisos table, that althought the quotation is mapped
	the product record is inactive...
*/

CREATE PROCEDURE [dbo].[Step2c5_MappingProducts_Notifications3]
	@Info nvarchar(256) OUTPUT
AS
	
	-- insert notifications on CotacoesAvisos table
	BEGIN TRY

		/*
			Insert notifications rows for all inactive products with active quotations
		*/
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID(), 
			   c.FornecedorCodigo,
			   c.Data,
			   c._ProdutoCodigo,
			   c._ComplementoCodigo,
			   'CIPM4',	-- product is configured as inative (not active)
			   'produto configurado como inativo mas com cotações ativas, verifque!' AS Descricao,
			   GETDATE() AS Criacao

		-- consider again only quotations missing yet (after processing) ProdutoCodigo mapping.
		FROM [dbo].[CotacoesAtivasView] c
			 INNER JOIN [dbo].[Produtos] p
				ON c.ProdutoCodigo = p.Codigo

		-- consider only quotations that are in Partnumbers ambiguous list
		WHERE p.Inativo = CAST('true' as bit)



		-- fill output @info with sucess message
		SET @Info = '[Step2c5_MappingProducts_Notifications3-01] Number of inserted notifications: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2c5_MappingProducts_Notifications3-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
