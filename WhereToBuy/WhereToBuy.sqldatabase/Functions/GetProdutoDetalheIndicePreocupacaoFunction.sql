/*
	This function receives description and features fields from a ProdutoDetalhe and returns it's content preocupation index.
*/


CREATE FUNCTION [dbo].[GetProdutoDetalheIndicePreocupacaoFunction]
(
	@descricao nvarchar(256),
	@caracteristicas nvarchar(2048)
)
RETURNS tinyint
AS
BEGIN
	
	-- variables
	DECLARE @indice tinyint = 0;

	-- search for the greather preocupation index present either in @description and features
	SELECT @indice = MAX(Indice)
	FROM
		(

			-- get content preocupation index from description
			SELECT [dbo].GetIndicePreocupacaoFunction(@descricao) as Indice

			UNION

			-- get content preocupation index from features
			SELECT [dbo].GetIndicePreocupacaoFunction(@caracteristicas) as Indice

		) indices;


	-- send back preocupation index
	return @indice;

END
