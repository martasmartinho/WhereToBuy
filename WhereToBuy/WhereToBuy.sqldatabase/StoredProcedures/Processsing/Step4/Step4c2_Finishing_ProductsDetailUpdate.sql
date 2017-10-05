/*
	This stored procedure will update Products Detail (ProdutosDetalhe) table with recent information.
*/


CREATE PROCEDURE [dbo].[Step4c2_Finishing_ProductsDetailUpdate]
	@Info nvarchar(256) OUTPUT
AS

	-- variables
	DECLARE @numberOfAffectedRows integer = 0;


	-- start a transaction scope
	BEGIN TRANSACTION


	BEGIN TRY

		/*
			Update Products Detail information
		*/
		

	
		-- set new content preocupation index (based on description and features from Quotations table - the new value..)
		UPDATE pd
		SET pd.IndicePreocupacaoConteudo = [dbo].[GetProdutoDetalheIndicePreocupacaoFunction](c.Descricao, c.Caracteristicas),
			pd.Versao = GETDATE()
		FROM [dbo].[ProdutosDetalhe] pd
			 INNER JOIN [dbo].[CotacoesAtuaisView] c
				   ON pd.FornecedorCodigo = c.FornecedorCodigo
					  AND pd.ProdutoCodigo = c.ProdutoCodigo
		-- process only rows that has changes on description or features..
		WHERE pd.AtualizacaoAutomaticaInativa = CAST('false' as bit)
			  AND (
					pd.Descricao <> c.Descricao
					OR pd.Caracteristicas <> c.Caracteristicas
				  )

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;





		-- description
		UPDATE pd
		SET pd.DescricaoPontuacao += 1,
			pd.Descricao = c.Descricao,
			pd.Versao = GETDATE()
		FROM [dbo].[ProdutosDetalhe] pd
			 INNER JOIN [dbo].[CotacoesAtuaisView] c
				   ON pd.FornecedorCodigo = c.FornecedorCodigo
					  AND pd.ProdutoCodigo = c.ProdutoCodigo
		-- process only rows that are settled to update automaticaly and need description update
		WHERE pd.AtualizacaoAutomaticaInativa = CAST('false' as bit)
			  AND pd.Descricao <> c.Descricao

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;





		-- features
		UPDATE pd
		SET pd.CaracteristicasPontuacao += 1,
			pd.Caracteristicas = c.Caracteristicas,
			pd.Versao = GETDATE()
		FROM [dbo].[ProdutosDetalhe] pd
			 INNER JOIN [dbo].[CotacoesAtuaisView] c
				   ON pd.FornecedorCodigo = c.FornecedorCodigo
					  AND pd.ProdutoCodigo = c.ProdutoCodigo
		-- process only rows that are settled to update automaticaly and need features update
		WHERE pd.AtualizacaoAutomaticaInativa = CAST('false' as bit)
			  AND pd.Caracteristicas <> c.Caracteristicas

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;




		-- link
		UPDATE pd
		SET pd.LinkPontuacao += 1,
			pd.Link = c.Link,
			pd.Versao = GETDATE()
		FROM [dbo].[ProdutosDetalhe] pd
			 INNER JOIN [dbo].[CotacoesAtuaisView] c
				   ON pd.FornecedorCodigo = c.FornecedorCodigo
					  AND pd.ProdutoCodigo = c.ProdutoCodigo
		-- process only rows that are settled to update automaticaly and need link update
		WHERE pd.AtualizacaoAutomaticaInativa = CAST('false' as bit)
			  AND pd.Link <> c.Link

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;




		-- image
		UPDATE pd
		SET pd.ImagemPontuacao += 1,
			pd.Imagem = c.Imagem,
			pd.Versao = GETDATE()
		FROM [dbo].[ProdutosDetalhe] pd
			 INNER JOIN [dbo].[CotacoesAtuaisView] c
				   ON pd.FornecedorCodigo = c.FornecedorCodigo
					  AND pd.ProdutoCodigo = c.ProdutoCodigo
		-- process only rows that are settled to update automaticaly and need image update
		WHERE pd.AtualizacaoAutomaticaInativa = CAST('false' as bit)
			  AND pd.Imagem <> c.Imagem

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;





		-- notify user manual update if needed
		UPDATE pd
		SET pd.AtualizacaoManualNecessaria = CAST('true' as bit),
			pd.Versao = GETDATE()
		FROM [dbo].[ProdutosDetalhe] pd
			 INNER JOIN [dbo].[CotacoesAtuaisView] c
				   ON pd.FornecedorCodigo = c.FornecedorCodigo
					  AND pd.ProdutoCodigo = c.ProdutoCodigo
		-- filter rows needing some update and configured to do not update automaticaly
		WHERE pd.AtualizacaoAutomaticaInativa = CAST('true' as bit)
			  AND (
					pd.Descricao <> c.Descricao
					OR pd.Caracteristicas <> c.Caracteristicas
					OR pd.Link <> c.Link
					OR pd.Imagem <> c.Imagem
				  )

		-- sum to @numberOfAffectedRows
		SET @numberOfAffectedRows += @@ROWCOUNT;



		-- commit all changes
		COMMIT TRANSACTION

		-- fill output @info with sucess message
		SET @Info = '[Step4c2_Finishing_ProductsDetailUpdate-01] Total of affected rows (one product detail could be updated by several instructions): ' + CAST(@numberOfAffectedRows as nvarchar);

		-- send true to output
		RETURN 1;

	END TRY

	BEGIN CATCH
		-- rollback transaction
		ROLLBACK TRANSACTION

		-- fill output @info with occurred error message
		SET @Info = '[Step4c2_Finishing_ProductsDetailUpdate-02] ' + CAST(ERROR_NUMBER() as nvarchar) + ' - ' + ERROR_MESSAGE();

		-- send false to output
		RETURN 0;

	END CATCH

RETURN;
