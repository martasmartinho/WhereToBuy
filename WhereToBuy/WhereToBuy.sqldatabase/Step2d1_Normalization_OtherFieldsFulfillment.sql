/*
	This SP, perform Quotation other fields fulfillment.
*/


CREATE PROCEDURE [dbo].[Step2d1_Normalization_OtherFieldsFulfillment]
	@Info nvarchar(256) OUTPUT
AS

	BEGIN TRY

		/*
			Normalize and fill the remain Quotation internal fields.
		*/
		UPDATE ci
		SET ci.Descricao = ci.DescricaoUniformizada,
			ci.Caracteristicas = ci.CaracteristicasUniformizadas,
			ci.Link = ci.LinkUniformizado,
			ci.Imagem = ci.ImagemUniformizada,

			-- cost price
			ci.PrecoCusto = ci.PrecoCustoCalculado,
			ci.PrecoCustoFormula = ci.FormulaPrecoCustoCalculado,

			-- expiration date
			ci.Validade = ci.ValidadeCalculada,
			ci.ValidadeFormula = ci.FormulaValidadeCalculada,

			-- stockSubstituto
			ci.StockCodigoSubstituto = ci.StockCodigoSubstitutoCalculado,
			ci.StockCodigoSubstitutoJustificacao = ci.JustificacaoStockCodigoSubstitutoCalculado,
			
			-- version
			ci.Versao = GETDATE()


		/*  Using CotacoesIncompletasNormalizacaoView, I'm ensuring that I am working only with Incomplete Quotations  
			and adicional fields are generated only inside this view to simplify work of set internal fields of Quotations
			in Cotacoes table.
		*/
		FROM [dbo].[CotacoesIncompletasNormalizacaoView] ci



		-- fill output @info with sucess message
		SET @Info = '[Step2d1_Normalization_OtherFieldsFulfillment-01] Number of processed quotations: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step2d1_Normalization_OtherFieldsFulfillment-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
