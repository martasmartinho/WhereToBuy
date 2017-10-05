/*
	This function receives the original features and deal with it to clean and normalize it.
*/


CREATE FUNCTION [dbo].[GetCotacaoCaracteristicasFunction]
(
	@_Caracteristicas nvarchar(2048)
)
RETURNS nvarchar(2048)
AS
BEGIN

	-- variables
	DECLARE @caracteristicas nvarchar(2048);

	-- force lowercase
	SET @caracteristicas = LOWER(@_Caracteristicas);

	-- strip html tags
	SET @caracteristicas = [dbo].[StripHtmlTagsFunction](@caracteristicas);

	-- correct some chars
	SET @caracteristicas = REPLACE(@caracteristicas, '\', '/');
	SET @caracteristicas = REPLACE(@caracteristicas, '|', '/');

	-- force some linguistic rules (if extra bad spaces generated, they will be removed in CleanBadCharsFunction..
	SET @caracteristicas = REPLACE(@caracteristicas, '.', '. ');
	SET @caracteristicas = REPLACE(@caracteristicas, ',', ', ');
	SET @caracteristicas = REPLACE(@caracteristicas, ':', ': ');
	SET @caracteristicas = REPLACE(@caracteristicas, ';', '; ');
	SET @caracteristicas = REPLACE(@caracteristicas, '!', '! ');
	SET @caracteristicas = REPLACE(@caracteristicas, '?', '? ');

	-- clean bad chars
	SET @caracteristicas = [dbo].[CleanBadCharsFunction](@caracteristicas);

	-- send back the normalized features
	RETURN @caracteristicas;
END