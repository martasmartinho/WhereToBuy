/*
	This function receives the original Product Image URL and normalize it.
*/


CREATE FUNCTION [dbo].[GetCotacaoImagemFunction]
(
	@_Imagem nvarchar(1024)
)
RETURNS nvarchar(1024)
AS
BEGIN

	-- variables
	DECLARE @image nvarchar(1024);

	-- force lowercase
	SET @image = LOWER(@_Imagem);

	-- clean newline chars
	SET @image = REPLACE(@image, CHAR(10) + CHAR(13), '');
	SET @image = REPLACE(@image, CHAR(10), '');
	SET @image = REPLACE(@image, CHAR(13), '');
	SET @image = REPLACE(@image, CHAR(9), '');

	-- send back the normalized link
	RETURN @image;
END