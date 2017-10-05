/*
	This function receives some text and, after analyse it, send back a value [0:9] representing the danger of terms..
	Zero (0) represents a no preocupation at all and nine (9) represents text with extremely danger terms.
*/


CREATE FUNCTION [dbo].[GetIndicePreocupacaoFunction]
(
	@texto nvarchar(MAX)
)
RETURNS tinyint
AS
BEGIN
	
	-- variables
	DECLARE @indice tinyint = 0;

	-- search for the greather preocupation index present in @texto
	SELECT TOP 1 @indice = Indice
	FROM [dbo].[TermosPreocupantes]
	WHERE LOWER(@texto) LIKE LOWER(Termo)
	ORDER BY Indice DESC;

	-- send back preocupation index
	return @indice;

END
