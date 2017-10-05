/*
	This function receives the original Partnumber sended by the supplier and minimize it to only keep the essencials chars (alpha-numeric chars).

	The goal of this processing task, is to improve the chances of matching the partnumber between diferent suppliers (external systems)

	Be aware that, the received Partnumber have already been object of cleaning on Quotations Insert process. However, while in the cleaning
	process only undesirable chars were cleaned, now, with this function, the already cleaned partnumber is reduced to the minimum of representative chars
	to improve chances of matching between several external systems..
*/


CREATE FUNCTION [dbo].[GetCotacaoPartnumberSimplificadoFunction]
(
	@Partnumber nvarchar(25)
)
RETURNS nvarchar(25)
AS
BEGIN

	-- variables
	DECLARE @minimizedPartnumber nvarchar(25);

	-- minimize received Partnumber to the minimum of representative chars
	SET @minimizedPartnumber = [dbo].GetTextoOnlyAllowedCharsFunction(@Partnumber, '[A-Za-z0-9]');

	-- send back the minimized Partnumber version
	RETURN @minimizedPartnumber;
END
