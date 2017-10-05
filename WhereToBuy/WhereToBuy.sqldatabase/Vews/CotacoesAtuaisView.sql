/*
	Esta View facilita a vista sobre as cotações já integradas (ativas). O conjunto de CotacoesAtuais representa o conjunto de cotações
	atualmente válidas de cada fornecedor para cada produto.
	(Uma cotação atual está completa e integrada e significad que pode ser usada como base para o algoritmo de eleição da melhor cotação)
*/

CREATE VIEW [dbo].[CotacoesAtuaisView]
	AS 
	SELECT *
	FROM [dbo].[CotacoesAtivasView]
	WHERE [Integrado] = CAST('true' as bit)