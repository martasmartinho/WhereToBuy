/*
	Esta View facilita a vista sobre as cotações incompletas e, lógicamente, não integradas (já que uma cotação só pode ser integrada depois de estar completa)
	(Uma cotação incompleta é aquela que falta informação, por exemplo, mapeamentos..)
	(Uma cotação incompleta é uma cotação que ainda falta sofrer processamento para poder ser integrada!)
*/

CREATE VIEW [dbo].[CotacoesIncompletasView]
	AS 
	SELECT *
	FROM [dbo].[CotacoesAtivasView]
	WHERE [Completo] = CAST('false' as bit)