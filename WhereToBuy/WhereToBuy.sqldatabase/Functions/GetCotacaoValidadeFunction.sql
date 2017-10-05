/*
	This function pretends to set with more accuracy the expiration date of received quotation.
	It returns a table with two fields: first the expiration date and the second with a formula that demonstrates the calculation of that date.
*/


CREATE FUNCTION [dbo].[GetCotacaoValidadeFunction]
(
	@Data datetime,
	@_Validade smalldatetime,
	@_ValidadeDescricao nvarchar(128),
	@CotacoesRegras_HorasValidade smallint,
	@ProdutosMatching_HorasValidade smallint
)
RETURNS @results TABLE
(
	__id integer PRIMARY KEY NOT NULL IDENTITY(1,1),
	Validade smalldatetime NOT NULL,
	Formula nvarchar(256) NOT NULL
)
AS
BEGIN

	-- variables
	DECLARE @horasValidade integer, @validade smalldatetime, @formula nvarchar(256);


	-- if some expiration date has come from supplier then use it
	IF @_Validade <> CAST('01-01-2000' as smalldatetime)
	BEGIN
		
		-- expiration will be the received date
		SET @validade = @_Validade;
		SET @formula = 'validade definida pelo fornecedor: ';
		SET @formula = @formula + LOWER(RTRIM(@_ValidadeDescricao));

		-- prepare data to be send back
		INSERT INTO @results
		SELECT @validade, @formula;

		-- terminate
		RETURN;

	END


	-- the most mandatory rule is when it is defined a specific rule for the product itself.
	-- so, if there is a value defined for the product this rules prevails, but if not, the definition used on
	-- CotacoesRegras table.
	IF @ProdutosMatching_HorasValidade <> 0
	BEGIN
		SET @horasValidade = @ProdutosMatching_HorasValidade;
	END
	ELSE
	BEGIN
		SET @horasValidade = @CotacoesRegras_HorasValidade;
	END


	-- expiration date calculation
	SET @validade = DATEADD(HOUR, @horasValidade, @Data);


	-- set the formula to documentate in future this calculation
	IF @ProdutosMatching_HorasValidade <> 0
	BEGIN
		-- set the formula to future documentation
		SET @formula = 'validade = data + pmhv';
		SET @formula = @formula + ' = ';
		SET @formula = @formula + CONVERT(varchar, @Data, 120);
		SET @formula = @formula + ' + ';
		SET @formula = @formula + CONVERT(varchar, @ProdutosMatching_HorasValidade) + 'horas';
		SET @formula = @formula + ' = ';
		SET @formula = @formula + CONVERT(varchar, @validade, 120);
	END
	ELSE
	BEGIN
		-- set the formula to future documentation
		SET @formula = 'validade = data + crhv';
		SET @formula = @formula + ' = ';
		SET @formula = @formula + CONVERT(varchar, @Data, 120);
		SET @formula = @formula + ' + ';
		SET @formula = @formula + CONVERT(varchar, @CotacoesRegras_HorasValidade) + 'horas';
		SET @formula = @formula + ' = ';
		SET @formula = @formula + CONVERT(varchar, @validade, 120);
	END


	-- prepare data to be send back
	INSERT INTO @results
	SELECT @validade, @formula;


	-- terminate
	RETURN;
END