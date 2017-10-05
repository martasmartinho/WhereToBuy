/*
	O objetivo desta função é limpar uma string com caracteres lixo e espaço em excesso, por exemplo.
*/

CREATE FUNCTION [dbo].CleanBadCharsFunction
(
	@texto nvarchar(256)
)
RETURNS nvarchar(256)
AS
BEGIN

	-- remove trim spaces (left and right)
	SET @texto = LTRIM(RTRIM(@texto));

	-- remove bad chars (unexpected chars)
	SET @texto = REPLACE(@texto, '´', ' ');
	SET @texto = REPLACE(@texto, '`', ' ');
	SET @texto = REPLACE(@texto, '"', ' ');
	SET @texto = REPLACE(@texto, '~', ' ');
	SET @texto = REPLACE(@texto, '^', ' ');
	SET @texto = REPLACE(@texto, '''', ' ');
	SET @texto = REPLACE(@texto, '¨', ' ');

	-- remove extra wrong spaces (leave only one space)
	SET @texto = REPLACE(@texto, '       ', ' ');
	SET @texto = REPLACE(@texto, '      ', ' ');
	SET @texto = REPLACE(@texto, '     ', ' ');
	SET @texto = REPLACE(@texto, '    ', ' ');
	SET @texto = REPLACE(@texto, '   ', ' ');
	SET @texto = REPLACE(@texto, '  ', ' ');

	-- return cleaned text
	RETURN @texto;
END
