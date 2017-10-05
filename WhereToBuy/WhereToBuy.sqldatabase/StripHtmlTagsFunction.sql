/*
	This function strips all http tags from string
*/

CREATE FUNCTION [dbo].StripHtmlTagsFunction
(
	@text nvarchar(MAX)
)
RETURNS nvarchar(MAX)
AS
BEGIN

	-- variables
	DECLARE @start integer, @end integer, @length integer;

	-- set initial values
	SET @start = CHARINDEX('<', @text);			-- get position of first occurrence of <
	SET @end = CHARINDEX('>', @text, @start);	-- get position of first occurrence of > after @start
	SET @length = (@end - @start) + 1;

	-- iterate while there is at least one > and <
	WHILE @start > 0 AND @end > 0 AND @length > 0
	BEGIN
		
		-- replace set of text inside @text
		SET @text = STUFF(@text, @start, @length, '');
		
		-- prepare a new iteration
		SET @start = CHARINDEX('<', @text);
		SET @end = CHARINDEX('>', @text, @start);
		SET @length = (@end - @start) + 1;

	END

	-- remove extra long line feeding
	SET @text = REPLACE(@text, CHAR(10) + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + CHAR(13), CHAR(10) + CHAR(13));
	SET @text = REPLACE(@text, CHAR(10) + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + CHAR(13), CHAR(10) + CHAR(13));
	SET @text = REPLACE(@text, CHAR(10) + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10) + CHAR(13), CHAR(10) + CHAR(13));
	SET @text = REPLACE(@text, CHAR(10) + CHAR(13) + CHAR(10) + CHAR(13), CHAR(10) + CHAR(13));

	-- remove other html stuff
	SET @text = REPLACE(@text, '&nbsp;', '');
	SET @text = REPLACE(@text, '&tab;', '');
	SET @text = REPLACE(@text, '&newline;', '');
	SET @text = REPLACE(@text, '&quot;', '');
	SET @text = REPLACE(@text, '&num;', '');
	SET @text = REPLACE(@text, '&percnt;', '%');
	SET @text = REPLACE(@text, '&amp;', '&');
	SET @text = REPLACE(@text, '&apos;', '');
	SET @text = REPLACE(@text, '&comma;', ',');
	SET @text = REPLACE(@text, '&period;', '.');
	SET @text = REPLACE(@text, '&sol;', '/');
	SET @text = REPLACE(@text, '&colon;', ':');
	SET @text = REPLACE(@text, '&semi;', ';');
	SET @text = REPLACE(@text, '&plus;', '+');
	SET @text = REPLACE(@text, '&lt;', '<');
	SET @text = REPLACE(@text, '&equals;', '=');
	SET @text = REPLACE(@text, '&gt;', '>');
	SET @text = REPLACE(@text, '&quest;', '?');
	SET @text = REPLACE(@text, '&commat;', '@');
	SET @text = REPLACE(@text, '&bsol;', '\');
	SET @text = REPLACE(@text, '&reg;', '(R)');
	SET @text = REPLACE(@text, '&copy;', '(C)');
	SET @text = REPLACE(@text, '&deg;', 'º');

	-- clean leading spaces
	SET @text = LTRIM(RTRIM(@text));

	-- return stripped text
	RETURN @text;

END
