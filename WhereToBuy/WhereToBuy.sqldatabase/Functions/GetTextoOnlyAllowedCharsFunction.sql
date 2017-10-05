/*
	This function receives two arguments. The first one it's the text to be stripped off and the second
	is the pattern of allowed chars.

	All chars not allowed by pattern are excluded from the text and, the result string, is sended
	back to calling point.

	Example of use:
	SELECT [dbo].GetTextoOnlyAllowedCharsFunction('Th.-is is the t-es+t num$ber 93!','[A-Za-z0-9 ]');
*/

CREATE FUNCTION [dbo].GetTextoOnlyAllowedCharsFunction
(
	@texto nvarchar(256),
	@allowedPattern nvarchar(256)
)
RETURNS nvarchar(256)
AS
BEGIN

	-- variaveis
	DECLARE @i AS INTEGER = 1, @n AS INTEGER = LEN(@texto), @result nvarchar(256) = '';

	-- percorrer cada caracter e concatenar apenas os caracteres permitidos
	WHILE @i <= @n
	BEGIN
		
		-- avalia o caracter atual (em processamento)
		IF SUBSTRING(@texto, @i, 1) LIKE @allowedPattern
		BEGIN
			-- se o caracter é válido na expressão regular, adiciona-o à expressão resultante
			SET @result += SUBSTRING(@texto, @i, 1);
		END

		-- incrementar contador
		SET @i += 1;

	END

	-- devolver texto stripped
	RETURN @result;
END
