/*
	This function pretends to calculate final price of quotation.
	Formula will permit to know later how the final price was calculated.
*/


CREATE FUNCTION [dbo].[GetCotacaoPrecoFunction]
(
	@_Preco decimal(14,4),
	@_OutrosCustos decimal(14,4),
	@_OutrosCustosDescricao nvarchar(128)
)
RETURNS @results TABLE
(
	__id integer PRIMARY KEY NOT NULL IDENTITY(1,1),
	PrecoCusto decimal(14,4) NOT NULL,
	Formula nvarchar(256) NOT NULL
)
AS
BEGIN

	-- variables
	DECLARE @precoCusto decimal(14,4), @formula nvarchar(256);


	-- final cost price is calculated as the sum of Preco with OutrosCustos
	SET @precoCusto = @_Preco + @_OutrosCustos;


	-- now, if @_OutrosCustosDescricao has text inside it, that text it is the justification of existing adicional costs (@_OutrosCustos)
	-- however, if @_OutrosCustos = 0, that description justifies the @_Preco (maybe more expensive because..)
	IF COALESCE(@_OutrosCustosDescricao, '(vazio)') = '(vazio)'
	BEGIN
		SET @formula = 'PrecoCusto = Preco';
	END
	ELSE
	BEGIN
		
		IF @_OutrosCustos <> 0
		BEGIN
			
			-- formula must show that the PrecoCusto is the sum of Preço and OutrosCustos
			SET @formula = 'PrecoCusto = Preco + @OutrosCustos';
			SET @formula = @formula + ' = ';
			SET @formula = @formula + CONVERT(varchar, @_Preco);
			SET @formula = @formula + ' + ';
			SET @formula = @formula + ' (';
			SET @formula = @formula + LOWER(RTRIM(@_OutrosCustosDescricao));
			SET @formula = @formula + ': ';
			SET @formula = @formula + CONVERT(varchar, @_OutrosCustos);;
			SET @formula = @formula + ') ';
			SET @formula = @formula + ' = ';
			SET @formula = @formula + CONVERT(varchar, @precoCusto);

		END
		ELSE
		BEGIN

			-- formula must show that the price itself include other associated costs
			SET @formula = 'PrecoCusto = Preco + @OutrosCustos';
			SET @formula = @formula + ' <=> ';
			SET @formula = @formula + CONVERT(varchar, @precoCusto);
			SET @formula = @formula + ' = ';
			SET @formula = @formula + ' Preco + (';
			SET @formula = @formula + LOWER(RTRIM(@_OutrosCustosDescricao));
			SET @formula = @formula + ': ';
			SET @formula = @formula + CONVERT(varchar, @_OutrosCustos);
			SET @formula = @formula + ')';

		END
	END


	-- prepare data to be send back
	INSERT INTO @results
	SELECT @precoCusto, @formula;


	-- terminate
	RETURN;
END