/*
	This SP create new Products based on new quotations received from suppliers.
*/


CREATE PROCEDURE [dbo].[Step2c2_MappingProducts_InsertNewProducts]
	@Info nvarchar(256) OUTPUT
AS
	
	-- new rules feeding
	BEGIN TRY

		/*
			Insert new Products
		*/
		INSERT [dbo].[Produtos] 
			   (
					Codigo, MarcaCodigo, CategoriaCodigo, ImpostoCodigo, FornecedorCodigo, Partnumber, Descricao,
					Descontinuado, Inativo, Criacao, Versao
			   )

		SELECT Referencia, MarcaCodigo, CategoriaCodigo, ImpostoCodigo, FornecedorCodigo, Partnumber, '(' + Partnumber + ')',
		       CAST('false' as bit), CAST('false' as bit), GETDATE(), GETDATE()
		FROM [dbo].[CotacoesMatrizCriacaoProdutosView]



		-- fill output @info with sucess message
		SET @Info = '[Step2c2_MappingProducts_InsertNewProducts-01] Number of created products: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2c2_MappingProducts_InsertNewProducts-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
