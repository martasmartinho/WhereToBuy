CREATE PROCEDURE [dbo].[UserInsert]
	@Username nvarchar(20),
	@Password nvarchar(250),
	@Nome nvarchar(50),
	@Email nvarchar(100),
	@Mobile nvarchar(15),
	@Sms nvarchar(15),
	@Administrador bit,
	@IdiomaCodigo nvarchar(5),
	@Inativo bit
AS
	
	DECLARE @Criacao smalldatetime, @Versao datetime, @NumeroLinhasAfetadas int, @Erro nvarchar(255)

	SET @Criacao = GETDATE()
	SET @Versao = GETDATE()


	-- INSERÇÃO DO REGISTO
	BEGIN TRY
		 
		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF

		-- INSERIR REGISTO
		INSERT INTO [dbo].[Utilizadores] ([Username], [Password], [Nome], [Email], [Mobile], [Sms], [Administrador], [IdiomaCodigo], [Inativo], [Criacao], [Versao]) 
					VALUES (@Username, HASHBYTES('SHA2_512', @Password), @Nome, @Email, @Mobile, @Sms, @Administrador, @IdiomaCodigo, @Inativo, @Criacao, @Versao);
		
		SET @NumeroLinhasAfetadas = @@ROWCOUNT -- GUARDAR O NUMERO DE LINHAS AFETADAS

		-- ACABA CONTAGEM DE @@ROWCOUNT
		SET NOCOUNT ON

		-- RETORNAR O NUMERO DE LINHAS AFETADAS
		-- Quando usado o ExecuteNonQuery é o @@ROWCOUNT que é lido (SET NOCOUNT OFF/ON),  e não esta variável
		RETURN @NumeroLinhasAfetadas

	END TRY


	-- SE EXISTIRAM ERROS, DESFAZER TRANSACÇÃO
	BEGIN CATCH

		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-I10] ' + ERROR_MESSAGE();
		RAISERROR (@Erro, 15, 1)
		RETURN 0

	END CATCH

