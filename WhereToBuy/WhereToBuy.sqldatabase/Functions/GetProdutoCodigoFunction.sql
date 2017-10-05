/*
	This function calculates the Product Code for each Quotation.
	It receives the Brand Code, Addon Code and Partnumber and, after minimize the Partnumber, 
	generates a Product Code concatenating the Brand Code with the Partnumber and with Addon Code as shown below
		
		Product Code = Brand Code + Minimized Partnumber + '[' + Addon Code + ']'

	With this function, we can have access to the Product Code, even before the Product record has been created.

	This function reuses the GetCotacaoPartnumberSimplificado function too minimize the Partnumber, thereby improving
	the chances of matching product codes between different suppliers.
	
	This function receives the Addon Code. Addons are the way to differenciate the same product when a supplier offers
	with some specific add like delivery or extra warranty.
*/


CREATE FUNCTION [dbo].[GetProdutoCodigoFunction]
(
	@Partnumber nvarchar(25),
	@MarcaCodigo nvarchar(5),
	@ComplementoCodigo nvarchar(5)
)
RETURNS nvarchar(40)
AS
BEGIN

	-- variables
	DECLARE @complemento nvarchar(7) = ''; -- ComplementoCodigo + Parenteses = 5 + 2 = 7
	DECLARE @partnumberSimplificado nvarchar(25);

	-- simplify partnumber to be used as part of product code
	SET @partnumberSimplificado = [dbo].[GetCotacaoPartnumberSimplificadoFunction](@Partnumber)

	-- define addon part to be concatenated
	IF UPPER(@ComplementoCodigo) <> 'N/A' AND LEN(RTRIM(@ComplementoCodigo)) > 0
	BEGIN
		SET @complemento = '[' + RTRIM(UPPER(@ComplementoCodigo)) + ']';
	END

	-- return the product code to be used
	RETURN RTRIM(@MarcaCodigo) + @partnumberSimplificado + @complemento;
END
