/*
	This function receives the original description, brand name, partnumber, text to add and text to remove,
	and build a uniformed complete final product description
*/


CREATE FUNCTION [dbo].[GetCotacaoDescricaoFunction]
(
	@_Descricao nvarchar(256),
	@MarcaDescricao nvarchar(50),
	@Partnumber nvarchar(25),
	@TermoAcrescentar nvarchar(50),
	@TermosRemover nvarchar(100)
)
RETURNS nvarchar(256)
AS
BEGIN
	
	-- variables
	DECLARE @descricao nvarchar(256);


	-- evaluate pré-requisits
	IF (@TermoAcrescentar IS NULL)
	BEGIN
		SET @TermoAcrescentar = '';
	END

	IF (@TermosRemover IS NULL)
	BEGIN
		SET @TermosRemover = '';
	END


	-- force lowercase to description
	SET @descricao = LOWER(@_Descricao);


	-- ensure that brand name is there
	IF CHARINDEX(RTRIM(LOWER(@MarcaDescricao)), @descricao) = 0
	BEGIN
		SET @descricao = RTRIM(@descricao) + ' ' + RTRIM(LOWER(@MarcaDescricao));
	END


	-- ensure that partnumber is there
	IF CHARINDEX(RTRIM(LOWER(@Partnumber)), @descricao) = 0
	BEGIN
		SET @descricao = RTRIM(@descricao) + ' ' + RTRIM(LOWER(@Partnumber));
	END


	-- remove undesirable terms (if the received string has chars)
	IF LEN(RTRIM(@TermosRemover)) > 0
	BEGIN
		
		-- local variables
		DECLARE @termo as nvarchar(256);
		DECLARE termosCursor CURSOR FOR
			SELECT DISTINCT Element
			FROM [dbo].[SplitStringFunction](LOWER(@TermosRemover),',');

		-- load cursor
		OPEN termosCursor;

		-- first fetch
		FETCH NEXT FROM termosCursor INTO @termo;

		-- iterate all records in cursor
		WHILE @@FETCH_STATUS = 0
		BEGIN

			-- remove term
			SET @descricao = REPLACE(@descricao, RTRIM(LTRIM(@termo)), '');

			-- next fetch
			FETCH NEXT FROM termosCursor INTO @termo;

		END

		-- close cursor
		CLOSE termosCursor;
		DEALLOCATE termosCursor;

	END



	-- concatenate term to add
	IF LEN(@TermoAcrescentar) > 0 
	BEGIN
		SET @descricao = @descricao + ' ' + @TermoAcrescentar;
	END



	-- final bad chars cleaning
	SET @descricao = [dbo].[CleanBadCharsFunction](@descricao);
		
	-- send back the normalized description
	RETURN @descricao;
END
