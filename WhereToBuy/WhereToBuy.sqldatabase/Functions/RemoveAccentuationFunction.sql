/*
	O objetivo desta função é limpar uma string com caracteres lixo e espaço em excesso, por exemplo.
*/

CREATE FUNCTION [dbo].RemoveAccentuationFunction
(
	@texto nvarchar(256)
)
RETURNS nvarchar(256)
AS
BEGIN

	-- remove accents
	SET @texto = REPLACE(@texto, 'À' COLLATE Latin1_General_CS_AS, 'A'); -- CS -> Case Sensitive
	SET @texto = REPLACE(@texto, 'Á' COLLATE Latin1_General_CS_AS, 'A');
	SET @texto = REPLACE(@texto, 'Ã' COLLATE Latin1_General_CS_AS, 'A');
	SET @texto = REPLACE(@texto, 'Â' COLLATE Latin1_General_CS_AS, 'A');
	SET @texto = REPLACE(@texto, 'Ä' COLLATE Latin1_General_CS_AS, 'A');
	SET @texto = REPLACE(@texto, 'È' COLLATE Latin1_General_CS_AS, 'E');
	SET @texto = REPLACE(@texto, 'É' COLLATE Latin1_General_CS_AS, 'E');
	SET @texto = REPLACE(@texto, 'Ê' COLLATE Latin1_General_CS_AS, 'E');
	SET @texto = REPLACE(@texto, 'Ë' COLLATE Latin1_General_CS_AS, 'E');
	SET @texto = REPLACE(@texto, 'Ì' COLLATE Latin1_General_CS_AS, 'I');
	SET @texto = REPLACE(@texto, 'Í' COLLATE Latin1_General_CS_AS, 'I');
	SET @texto = REPLACE(@texto, 'Î' COLLATE Latin1_General_CS_AS, 'I');
	SET @texto = REPLACE(@texto, 'Ï' COLLATE Latin1_General_CS_AS, 'I');
	SET @texto = REPLACE(@texto, 'Ò' COLLATE Latin1_General_CS_AS, 'O');
	SET @texto = REPLACE(@texto, 'Ó' COLLATE Latin1_General_CS_AS, 'O');
	SET @texto = REPLACE(@texto, 'Õ' COLLATE Latin1_General_CS_AS, 'O');
	SET @texto = REPLACE(@texto, 'Ô' COLLATE Latin1_General_CS_AS, 'O');
	SET @texto = REPLACE(@texto, 'Ö' COLLATE Latin1_General_CS_AS, 'O');
	SET @texto = REPLACE(@texto, 'Ù' COLLATE Latin1_General_CS_AS, 'U');
	SET @texto = REPLACE(@texto, 'Ú' COLLATE Latin1_General_CS_AS, 'U');
	SET @texto = REPLACE(@texto, 'Û' COLLATE Latin1_General_CS_AS, 'U');
	SET @texto = REPLACE(@texto, 'Ü' COLLATE Latin1_General_CS_AS, 'U');
	SET @texto = REPLACE(@texto, 'Ç' COLLATE Latin1_General_CS_AS, 'C');

	SET @texto = REPLACE(@texto, 'à' COLLATE Latin1_General_CS_AS, 'a');
	SET @texto = REPLACE(@texto, 'á' COLLATE Latin1_General_CS_AS, 'a');
	SET @texto = REPLACE(@texto, 'ã' COLLATE Latin1_General_CS_AS, 'a');
	SET @texto = REPLACE(@texto, 'â' COLLATE Latin1_General_CS_AS, 'a');
	SET @texto = REPLACE(@texto, 'ä' COLLATE Latin1_General_CS_AS, 'a');
	SET @texto = REPLACE(@texto, 'è' COLLATE Latin1_General_CS_AS, 'e');
	SET @texto = REPLACE(@texto, 'é' COLLATE Latin1_General_CS_AS, 'e');
	SET @texto = REPLACE(@texto, 'ê' COLLATE Latin1_General_CS_AS, 'e');
	SET @texto = REPLACE(@texto, 'ë' COLLATE Latin1_General_CS_AS, 'e');
	SET @texto = REPLACE(@texto, 'ì' COLLATE Latin1_General_CS_AS, 'i');
	SET @texto = REPLACE(@texto, 'í' COLLATE Latin1_General_CS_AS, 'i');
	SET @texto = REPLACE(@texto, 'î' COLLATE Latin1_General_CS_AS, 'i');
	SET @texto = REPLACE(@texto, 'ï' COLLATE Latin1_General_CS_AS, 'i');
	SET @texto = REPLACE(@texto, 'ò' COLLATE Latin1_General_CS_AS, 'o');
	SET @texto = REPLACE(@texto, 'ó' COLLATE Latin1_General_CS_AS, 'o');
	SET @texto = REPLACE(@texto, 'õ' COLLATE Latin1_General_CS_AS, 'o');
	SET @texto = REPLACE(@texto, 'ô' COLLATE Latin1_General_CS_AS, 'o');
	SET @texto = REPLACE(@texto, 'ö' COLLATE Latin1_General_CS_AS, 'o');
	SET @texto = REPLACE(@texto, 'ù' COLLATE Latin1_General_CS_AS, 'u');
	SET @texto = REPLACE(@texto, 'ú' COLLATE Latin1_General_CS_AS, 'u');
	SET @texto = REPLACE(@texto, 'û' COLLATE Latin1_General_CS_AS, 'u');
	SET @texto = REPLACE(@texto, 'ü' COLLATE Latin1_General_CS_AS, 'u');
	SET @texto = REPLACE(@texto, 'ç' COLLATE Latin1_General_CS_AS, 'c');

	-- return text cleaned
	RETURN @texto;
END
