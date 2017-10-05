/*
	This function pretends to show what will be the substitute StockCodigo, if applicable.
	Formula will show wy there is a substitute StockCodigo
*/


CREATE FUNCTION [dbo].[GetCotacaoStockSubstitutoFunction]
(
	@StockCodigo nvarchar(5),
	@CotacoesRegras_StockCodigo nvarchar(5),
	@ProdutosMatching_StockCodigo nvarchar(5)
)
RETURNS @results TABLE
(
	__id integer PRIMARY KEY NOT NULL IDENTITY(1,1),
	StockCodigo nvarchar(5) NULL,
	Justificacao nvarchar(256) NULL
)
AS
BEGIN

	-- variables
	DECLARE @stockCodigoNovo nvarchar(5), @justificacao nvarchar(256);


	-- if there is a custom define StockCodigo for the product, use it
	SELECT 
			@stockCodigoNovo =
				CASE WHEN @ProdutosMatching_StockCodigo IS NOT NULL THEN @ProdutosMatching_StockCodigo
					 WHEN @CotacoesRegras_StockCodigo IS NOT NULL THEN @CotacoesRegras_StockCodigo
					 ELSE NULL
				END,

			@justificacao =
				CASE WHEN @ProdutosMatching_StockCodigo IS NOT NULL THEN '!ProdutosMatching - regra de utilizador'
					 WHEN @CotacoesRegras_StockCodigo IS NOT NULL THEN '!CotacoesRegras - regra de utilizador'
					 ELSE NULL
				END


	-- prepare data to be send back
	INSERT INTO @results
	SELECT @stockCodigoNovo, @justificacao;


	-- terminate
	RETURN;
END