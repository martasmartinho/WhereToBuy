/*
	Register on CotacoesAvisos, all change that will be sended to database, based on changed rules defined in ProdutosMatching and CotacoesRegras
*/


CREATE PROCEDURE [dbo].[Step4a1_Preparation_Notifications]
	@Info nvarchar(256) OUTPUT
AS

	-- variables
	DECLARE @numberOfAffectedRows integer = 0;


	BEGIN TRY

		/*
			Insert notifications (CotacoesAvisos) of quotations changed values due to changes in rules ProdutosMatching and CotacoesRegras
		*/
				-- cost price update notifications
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID() AS Id, 
			   c.FornecedorCodigo,
			   c.Data,
			   c._ProdutoCodigo,
			   c._ComplementoCodigo,
			   'CAC01' AS AvisoTipoCodigo,
			   'esta informação pode resultar de uma alteração recente de regras (ProdutosMatching ou CotacoesRegras)!' AS Descricao,
			   GETDATE() AS Criacao

		-- consider all quotations on CotacoesAtuaisRecalcularView, because all of them have changes to report
		FROM [dbo].CotacoesAtuaisRecalcularView c
		WHERE c.PrecoCusto <> c.PrecoCustoCalculado;

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;




		-- cost price formula update notifications
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID() AS Id, 
			   c.FornecedorCodigo,
			   c.Data,
			   c._ProdutoCodigo,
			   c._ComplementoCodigo,
			   'CAC02' AS AvisoTipoCodigo,
			   'esta informação pode resultar de uma alteração recente da formula correspondente!' AS Descricao,
			   GETDATE() AS Criacao

		-- consider all quotations on CotacoesAtuaisRecalcularView, because all of them have changes to report
		FROM [dbo].CotacoesAtuaisRecalcularView c
		WHERE c.PrecoCustoFormula <> c.FormulaPrecoCustoCalculado;

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;



		-- expiration date update notifications
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID() AS Id, 
			   c.FornecedorCodigo,
			   c.Data,
			   c._ProdutoCodigo,
			   c._ComplementoCodigo,
			   'CAC03' AS AvisoTipoCodigo,
			   'esta informação pode resultar de uma alteração recente de regras (ProdutosMatching ou CotacoesRegras)!' AS Descricao,
			   GETDATE() AS Criacao

		-- consider all quotations on CotacoesAtuaisRecalcularView, because all of them have changes to report
		FROM [dbo].CotacoesAtuaisRecalcularView c
		WHERE c.Validade <> c.ValidadeCalculada;

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;




		-- expiration date formula update notifications
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID() AS Id, 
			   c.FornecedorCodigo,
			   c.Data,
			   c._ProdutoCodigo,
			   c._ComplementoCodigo,
			   'CAC04' AS AvisoTipoCodigo,
			   'esta informação pode resultar de uma alteração recente da formula correspondente!' AS Descricao,
			   GETDATE() AS Criacao

		-- consider all quotations on CotacoesAtuaisRecalcularView, because all of them have changes to report
		FROM [dbo].CotacoesAtuaisRecalcularView c
		WHERE c.ValidadeFormula <> c.FormulaValidadeCalculada;

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;




		-- description update notifications
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID() AS Id, 
			   c.FornecedorCodigo,
			   c.Data,
			   c._ProdutoCodigo,
			   c._ComplementoCodigo,
			   'CAC05' AS AvisoTipoCodigo,
			   'esta informação pode resultar de uma alteração recente da formula de normalização!' AS Descricao,
			   GETDATE() AS Criacao

		-- consider all quotations on CotacoesAtuaisRecalcularView, because all of them have changes to report
		FROM [dbo].CotacoesAtuaisRecalcularView c
		WHERE c.Descricao <> c.DescricaoUniformizada;

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;




		-- features update notifications
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID() AS Id, 
			   c.FornecedorCodigo,
			   c.Data,
			   c._ProdutoCodigo,
			   c._ComplementoCodigo,
			   'CAC06' AS AvisoTipoCodigo,
			   'esta informação pode resultar de uma alteração recente da formula de normalização!' AS Descricao,
			   GETDATE() AS Criacao

		-- consider all quotations on CotacoesAtuaisRecalcularView, because all of them have changes to report
		FROM [dbo].CotacoesAtuaisRecalcularView c
		WHERE c.Caracteristicas <> c.CaracteristicasUniformizadas;

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;



				
		-- link update notifications
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID() AS Id, 
			   c.FornecedorCodigo,
			   c.Data,
			   c._ProdutoCodigo,
			   c._ComplementoCodigo,
			   'CAC07' AS AvisoTipoCodigo,
			   'esta informação pode resultar de uma alteração recente da formula de normalização!' AS Descricao,
			   GETDATE() AS Criacao

		-- consider all quotations on CotacoesAtuaisRecalcularView, because all of them have changes to report
		FROM [dbo].CotacoesAtuaisRecalcularView c
		WHERE c.Link <> c.LinkUniformizado;

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;




		-- image update notifications
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID() AS Id, 
			   c.FornecedorCodigo,
			   c.Data,
			   c._ProdutoCodigo,
			   c._ComplementoCodigo,
			   'CAC08' AS AvisoTipoCodigo,
			   'esta informação pode resultar de uma alteração recente da formula de normalização!' AS Descricao,
			   GETDATE() AS Criacao

		-- consider all quotations on CotacoesAtuaisRecalcularView, because all of them have changes to report
		FROM [dbo].CotacoesAtuaisRecalcularView c
		WHERE c.Imagem <> c.ImagemUniformizada;

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;
		



		-- substitute stock update notifications
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID() AS Id, 
			   c.FornecedorCodigo,
			   c.Data,
			   c._ProdutoCodigo,
			   c._ComplementoCodigo,
			   'CAC09' AS AvisoTipoCodigo,
			   'esta informação pode resultar de uma alteração recente de regras (ProdutosMatching ou CotacoesRegras)!' AS Descricao,
			   GETDATE() AS Criacao

		-- consider all quotations on CotacoesAtuaisRecalcularView, because all of them have changes to report
		FROM [dbo].CotacoesAtuaisRecalcularView c
		WHERE c.StockCodigoSubstituto <> c.StockCodigoSubstitutoCalculado;

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;
		



		-- substitute stock formula update notifications
		INSERT INTO [dbo].[CotacoesAvisos] 
				    (Id, FornecedorCodigo, Data, _ProdutoCodigo, _ComplementoCodigo, AvisoTipoCodigo, Descricao, Criacao)

		SELECT NEWID() AS Id, 
			   c.FornecedorCodigo,
			   c.Data,
			   c._ProdutoCodigo,
			   c._ComplementoCodigo,
			   'CAC10' AS AvisoTipoCodigo,
			   'esta informação pode resultar de uma alteração recente da formula correspondente!' AS Descricao,
			   GETDATE() AS Criacao

		-- consider all quotations on CotacoesAtuaisRecalcularView, because all of them have changes to report
		FROM [dbo].CotacoesAtuaisRecalcularView c
		WHERE c.StockCodigoSubstitutoJustificacao <> c.JustificacaoStockCodigoSubstitutoCalculado;

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;






		-- fill output @info with sucess message
		SET @Info = '[Step4a1_Preparation_Notifications-01] Number of inserted notifications: ' + CAST(@numberOfAffectedRows as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- fill output @info with occurred error message
		SET @Info = '[Step4a1_Preparation_Notifications-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
