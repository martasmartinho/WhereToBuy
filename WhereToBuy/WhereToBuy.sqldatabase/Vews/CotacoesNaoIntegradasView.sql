/*
	Esta View facilita a vista sobre as cotações completas mas ainda não integradas 
	(candidatas a integrar ou seja candidatas a CotacoesAtuais)
*/

CREATE VIEW [dbo].[CotacoesNaoIntegradasView]
	AS 
	SELECT *
	FROM [dbo].[CotacoesAtivasView]
	WHERE [Completo] = CAST('true' as bit)
		  AND [Integrado] = CAST('false' as bit)