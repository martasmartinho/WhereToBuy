﻿CREATE PROCEDURE [dbo].[WorryingTermDelete]
	@Termo nvarchar(5),
	@Versao datetime
AS
	DECLARE @Erro nvarchar(255)


	-- VALIDAR SE O CODIGO EXISTE
	IF NOT EXISTS(SELECT [Termo] FROM [dbo].[TermosPreocupantes] WHERE [Termo] = @Termo)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-D01] ' + 'O registo que pretende eliminar não existe!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	-- VALIDAR SE A VERSÃO AINDA É A MESMA
	IF NOT EXISTS(SELECT [Termo] FROM [dbo].[TermosPreocupantes] WHERE [Termo] = @Termo AND [Versao] = @Versao)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-D02] ' + 'O registo que pretende eliminar, já foi alterado desde que o abriu!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END


	-- TUDO OK PARA SEGUIR EM FRENTE
	DECLARE @NumeroLinhasAfetadas int


	-- ELIMINAR O REGISTO
	BEGIN TRY

		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF
		 
		-- ELIMINAR REGISTO
		DELETE FROM [dbo].[TermosPreocupantes] Where [Termo] = @Termo

		SET @NumeroLinhasAfetadas = @@ROWCOUNT -- GUARDAR O NUMERO DE LINHAS AFETADAS

		-- ACABA CONTAGEM DE @@ROWCOUNT
		SET NOCOUNT ON

		-- RETORNAR O NUMERO DE LINHAS AFETADAS
		-- Quando usado o ExecuteNonQuery é o @@ROWCOUNT que é lido (SET NOCOUNT OFF/ON), e não esta variável
		RETURN @NumeroLinhasAfetadas

	END TRY


	-- SE EXISTIRAM ERROS
	BEGIN CATCH
		
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-D10] ' + ERROR_MESSAGE();
		RAISERROR (@Erro, 15, 1)
		RETURN 0

	END CATCH
