/*
	This function receives a date interval and calculates the date correspondent to percentil received too.

	k = (percentil / 100) * n

*/


CREATE FUNCTION [dbo].[GetValidadePercentilFunction]
(
	@Data datetime,
	@Validade smalldatetime,
	@Percentil integer
)
RETURNS datetime
AS
BEGIN
	
	-- variables
	DECLARE @n integer, @k integer;

	-- interval calculation in days
	SET @n = DATEDIFF(DAY, @Data, @Validade); 

	-- percentil calculation
	SET @k = (@Percentil / 100.0) * @n;

	-- send back the date that belongs to interval that matches the percentil received as argument
	RETURN DATEADD(DAY, @k, @Data); 
END
