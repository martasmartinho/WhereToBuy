/*
	This function exist to normalize external coding system.
	The function receives an external code na normalize it to obey allways to the same internal rules of external codification.
	All external codes are normalized with this function when they are inserted on Quotations table.
	We want to prevent little externa changes on code strategy and so minimize not fake not matched situations. As an example, imagine some code that
	leaves lowercase and becomes uppercase. or the code is improved removing an extra space..
*/



CREATE FUNCTION [dbo].[GetCodigoExternoNormalizadoFunction]
(
	@codigo nvarchar(256)
)
RETURNS nvarchar(256)
AS
BEGIN

	-- change received separator chars by a space char. later will be changed again to underscore
	SET @codigo = REPLACE(@codigo, '-', ' ');
	SET @codigo = REPLACE(@codigo, '+', ' ');
	SET @codigo = REPLACE(@codigo, '*', ' ');
	SET @codigo = REPLACE(@codigo, '.', ' ');
	SET @codigo = REPLACE(@codigo, ':', ' ');
	SET @codigo = REPLACE(@codigo, ';', ' ');
	SET @codigo = REPLACE(@codigo, '#', ' ');
	SET @codigo = REPLACE(@codigo, '?', ' ');
	SET @codigo = REPLACE(@codigo, '!', ' ');
	SET @codigo = REPLACE(@codigo, 'º', ' ');
	SET @codigo = REPLACE(@codigo, 'ª', ' ');

	-- normalize external code
	SET @codigo = REPLACE(@codigo, '\', '/');
	SET @codigo = REPLACE(@codigo, '|', '/');
	
	-- remove accentuation
	SET @codigo = [dbo].[RemoveAccentuationFunction](@codigo);

	-- remove bad chars
	SET @codigo = [dbo].[CleanBadCharsFunction](@codigo);

	-- external codes will allways lowercase
	SET @codigo = LOWER(@codigo);

	-- change space chars to underscore (wrong multiple spaces were normalized behind)
	SET @codigo = REPLACE(@codigo, ' ', '_');

	-- devolver o codigo normalizado e limpo
	RETURN @codigo;
END
