/*
	Update (refresh) Quotations information that must be changed due to ProdutosMatching or CotacoesRegras updated rules.
	This SP uses CotacoesAtuaisRecalcular to simplify the process.
*/


CREATE PROCEDURE [dbo].[Step4a2_Preparation_OtherFieldsRefresh]
	@Info nvarchar(256) OUTPUT
AS

	BEGIN TRY

		/*
			Refresh values of quotation fields, as needed
		*/
		
		UPDATE c
		
		SET c.PrecoCusto = c.PrecoCustoCalculado,
		    c.PrecoCustoFormula = c.FormulaPrecoCustoCalculado,
			c.Validade = c.ValidadeCalculada,
			c.ValidadeFormula = c.FormulaValidadeCalculada,
			c.Descricao = c.DescricaoUniformizada,
			c.Caracteristicas = c.CaracteristicasUniformizadas,
			c.Link = c.LinkUniformizado,
			c.Imagem = c.ImagemUniformizada,
			c.StockCodigoSubstituto = c.StockCodigoSubstitutoCalculado,
			c.StockCodigoSubstitutoJustificacao = c.JustificacaoStockCodigoSubstitutoCalculado,
			c.Versao = GETDATE()

		FROM [dbo].[CotacoesAtuaisRecalcularView] c




		-- fill output @info with sucess message
		SET @Info = '[Step4a2_Preparation_OtherFieldsRefresh-01] Number of Refreshed Quotations: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step4a2_Preparation_OtherFieldsRefresh-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
