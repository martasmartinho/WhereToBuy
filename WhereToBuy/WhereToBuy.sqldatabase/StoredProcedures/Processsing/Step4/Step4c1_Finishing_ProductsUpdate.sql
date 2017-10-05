/*
	This stored procedure will update Products table with recent processed information.
	This SP will use CotacoesVencedoras view to support product updating..
*/


CREATE PROCEDURE [dbo].[Step4c1_Finishing_ProductsUpdate]
	@Info nvarchar(256) OUTPUT
AS

	-- variables
	DECLARE @numberOfAffectedRows integer = 0;


	-- start a transaction scope
	BEGIN TRANSACTION


	BEGIN TRY

		/*
			Update Product information
		*/
		-- new day prices update (do update price history) (update prices if needed of products not updated today already)
		UPDATE p
		SET p.PrecoCusto_U3 = p.PrecoCusto_U2,
			p.PrecoCusto_U3Data = p.PrecoCusto_U2Data,
			p.PrecoCusto_U2 = p.PrecoCusto_U1,
			p.PrecoCusto_U2Data = p.PrecoCusto_U1Data,
			p.PrecoCusto_U1 = p.PrecoCusto,
			p.PrecoCusto_U1Data = p.PrecoCusto_Data,
			p.PrecoCusto = c.PrecoCusto,
			p.PrecoCusto_Data = CAST(GETDATE() AS DATE),
			p.Versao = GETDATE()

		FROM [dbo].[Produtos] p
			 INNER JOIN [dbo].[CotacoesVencedorasView] c
				   ON p.Codigo = c.ProdutoCodigo

		-- we want to update only different prices (product vs quotations) and last update date was not today..
		WHERE COALESCE(p.PrecoCusto, -999999999) <> c.PrecoCusto
			  AND COALESCE(p.PrecoCusto_Data, CAST('01-01-2000' as date)) <> CAST(GETDATE() AS DATE)


		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;





		-- same day prices update (do NOT update price history)
		UPDATE p
		SET p.PrecoCusto = c.PrecoCusto,
			p.Versao = GETDATE()

		FROM [dbo].[Produtos] p
			 INNER JOIN [dbo].[CotacoesVencedorasView] c
				   ON p.Codigo = c.ProdutoCodigo

		-- we want to update only different prices (product vs quotations) and last update date was today..
		WHERE COALESCE(p.PrecoCusto, -999999999) <> c.PrecoCusto
			  AND COALESCE(p.PrecoCusto_Data, CAST('01-01-2000' as date)) = CAST(GETDATE() AS DATE)


		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;





		-- new day stock update (do update stock history) (update stocks if needed of products not updated today already)
		UPDATE p
		SET p.StockCodigo_U3 = p.StockCodigo_U2,
			p.StockCodigo_U3Data = p.StockCodigo_U2Data,
			p.StockCodigo_U2 = p.StockCodigo_U1,
			p.StockCodigo_U2Data = p.StockCodigo_U1Data,
			p.StockCodigo_U1 = p.StockCodigo,
			p.StockCodigo_U1Data = p.StockCodigo_Data,
			p.StockCodigo = c.StockCodigoEfetivo,
			p.StockCodigo_Data = CAST(GETDATE() AS DATE),
			p.Versao = GETDATE()

		FROM [dbo].[Produtos] p
			 INNER JOIN [dbo].[CotacoesVencedorasView] c
				   ON p.Codigo = c.ProdutoCodigo

		-- we want to update only different stocks (product vs quotations) and last update date was not today..
		WHERE COALESCE(p.StockCodigo, '') <> c.StockCodigoEfetivo
			  AND COALESCE(p.StockCodigo_Data, CAST('01-01-2000' as date)) <> CAST(GETDATE() AS DATE)


		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;





		-- same day stocks update (do NOT update stock history)
		UPDATE p
		SET p.StockCodigo = c.StockCodigoEfetivo,
			p.Versao = GETDATE()

		FROM [dbo].[Produtos] p
			 INNER JOIN [dbo].[CotacoesVencedorasView] c
				   ON p.Codigo = c.ProdutoCodigo

		-- we want to update only different stocks (product vs quotations) and last update date was today..
		WHERE COALESCE(p.StockCodigo, '') <> c.StockCodigoEfetivo
			  AND COALESCE(p.StockCodigo_Data, CAST('01-01-2000' as date)) = CAST(GETDATE() AS DATE)


		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;





		-- update other fields of quotation
		UPDATE p
		SET p.Descricao = c.Descricao,
			p.Partnumber = c.Partnumber,
			p.FornecedorCodigo = c.FornecedorCodigo,
			p.Versao = GETDATE()

		FROM [dbo].[Produtos] p
			 INNER JOIN [dbo].[CotacoesVencedorasView] c
				   ON p.Codigo = c.ProdutoCodigo

		-- filter only records with info to be updated
		WHERE COALESCE(p.Descricao,'') <> c.Descricao
			  OR COALESCE(p.Partnumber, '') <> c.Partnumber
			  OR COALESCE(p.FornecedorCodigo, '') <> c.FornecedorCodigo


		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;




		-- commit all changes
		COMMIT TRANSACTION

		-- fill output @info with sucess message
		SET @Info = '[Step4c1_Finishing_ProductsUpdate-01] Total of affected rows (one product could be updated by several instructions): ' + CAST(@numberOfAffectedRows as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- rollback transaction
		ROLLBACK TRANSACTION

		-- fill output @info with occurred error message
		SET @Info = '[Step4c1_Finishing_ProductsUpdate-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
