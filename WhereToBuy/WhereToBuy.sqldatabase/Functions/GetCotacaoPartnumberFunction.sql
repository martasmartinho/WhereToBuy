/*
	This function, also used on step 1 of wheretobuy processing, it's used to clean received Partnumber of incorrect spaces 
	(imagine that instead of one space inside the partnumber, external user put two or more spaces wrongly), and prevents too
	from wrong accented chars.. etc.

	We want to best normalize the Partnumber to make it more cross comun between several suppliers.

	We decid too that all Partnumbers will be in uppercase.
*/



CREATE FUNCTION [dbo].[GetCotacaoPartnumberFunction]
(
	@_Partnumber nvarchar(25)
)
RETURNS nvarchar(25)
AS
BEGIN

	-- variables
	DECLARE @partnumber nvarchar(25);

	-- partnumber will be allways stored on uppercase
	SET @partnumber = UPPER(@_Partnumber);

	-- remove accentuation
	SET @partnumber = [dbo].[RemoveAccentuationFunction](@partnumber);

	-- remove bad chars
	SET @partnumber = [dbo].[CleanBadCharsFunction](@partnumber);

	-- correct chars
	SET @partnumber = REPLACE(@partnumber, '\', '/');
	SET @partnumber = REPLACE(@partnumber, ',', '.');
	SET @partnumber = REPLACE(@partnumber, 'º', '');
	SET @partnumber = REPLACE(@partnumber, 'ª', '');

	-- remove all sepaces inside partnumber
	SET @partnumber = REPLACE(@partnumber, ' ','');

	-- devolver o partnumber limpo e normalizado
	RETURN @partnumber;
END
