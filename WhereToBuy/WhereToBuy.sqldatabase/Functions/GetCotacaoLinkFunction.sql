/*
	This function receives the original Product Link and normalize it.
*/


CREATE FUNCTION [dbo].[GetCotacaoLinkFunction]
(
	@_Link nvarchar(1024)
)
RETURNS nvarchar(1024)
AS
BEGIN

	-- variables
	DECLARE @link nvarchar(1024), @pos integer;

	-- force lowercase
	SET @link = LOWER(@_Link);

	-- get position of :// to analyse used protocol
	SELECT @pos = PATINDEX('%://%', @link);

	-- test if a authorized protocol it used
	-- if :// exists in the string, mean's that there is a protocol defined
	-- and if protocol is not in list, then @link is forbidden
	IF @pos > 1 AND (LOWER(LEFT(@link, @pos-1)) NOT IN ('http', 'https', 'ftp'))
	BEGIN
		-- ilegal protocol
		SET @link = '';
	END
	ELSE IF @pos < 1 AND @link <> '(vazio)'				-- :// it's not present in the string
	BEGIN
		-- add default protocol to link
		SET @link = 'http://' + @link;
	END
	ELSE
	BEGIN
		-- else
		SET @link = '(vazio)';
	END

	-- clean newline chars
	SET @link = REPLACE(@link, CHAR(10) + CHAR(13), '');
	SET @link = REPLACE(@link, CHAR(10), '');
	SET @link = REPLACE(@link, CHAR(13), '');
	SET @link = REPLACE(@link, CHAR(9), '');

	-- send back the normalized link
	RETURN @link;
END