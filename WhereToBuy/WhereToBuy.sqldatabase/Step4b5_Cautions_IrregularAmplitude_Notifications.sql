/*
	This SP makes sure that user is notified via CotacoesAvisos table, that some actual quotations were disabled because the
	it's price is causing an irregular price amplitude (inside the same product)
*/

CREATE PROCEDURE [dbo].[Step4b5_Cautions_IrregularAmplitude_Notifications]
	@Info nvarchar(256) OUTPUT
AS
	
	-- insert notifications on CotacoesAvisos table
	BEGIN TRY

		/*
			Insert notifications rows for all disabled quotations
		*/
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID(), 
			   c.FornecedorCodigo,
			   c.Data,
			   c._ProdutoCodigo,
			   c._ComplementoCodigo,
			   'IAPD2' AS AvisoTipoCodigo,
			   '!Prevenção[Preço Desfasado] (' + CONVERT(varchar, GETDATE(), 103) + ') Preço com amplitude irregular ' +
			   '[' + c.AmplitudeFormulaDetecaoDesfasamento + ']. ' +
			   'O preço desfasado é ' + CONVERT(varchar, c.PrecoDesfasado) + ' ' +
			   '[' + c.PrecoDesfasadoFormulaDecisao + '].',

			   GETDATE() AS Criacao

		-- consider only quotations filtered to Out of Interval prevention
		FROM [dbo].[CotacoesAtuaisAmplitudeIrregularView] c



		-- fill output @info with sucess message
		SET @Info = '[Step4b5_Cautions_IrregularAmplitude_Notifications-01] Number of inserted notifications: ' + CAST(@@ROWCOUNT as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step4b5_Cautions_IrregularAmplitude_Notifications-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
