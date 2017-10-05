/*
	this function receives a string and returns a table with all splitted terms.
*/

CREATE FUNCTION [dbo].[SplitStringFunction]
(
	@stringToSplit nvarchar(256),
	@splitChar char(1)
)
RETURNS @list TABLE 
(
	[Element] nvarchar(256)
)
BEGIN
	
	-- variables
	DECLARE @name nvarchar(256), @pos integer;

	-- build list of splitted terms
	WHILE CHARINDEX(@splitChar, @stringToSplit) > 0
	BEGIN

		-- keep term and current position
		SELECT @pos = CHARINDEX(@splitChar, @stringToSplit);
		SELECT @name = SUBSTRING(@stringToSplit, 1, @pos-1);

		-- insert extracted term into @list
		INSERT INTO @list
		SELECT @name;

		-- new stringToSplit is reduced
		SELECT @stringToSplit = SUBSTRING(@stringToSplit, @pos+1, LEN(@stringToSplit)-@pos);

	END

	-- finally
	INSERT INTO @list
	SELECT @stringToSplit;

	-- get out
	RETURN
END
