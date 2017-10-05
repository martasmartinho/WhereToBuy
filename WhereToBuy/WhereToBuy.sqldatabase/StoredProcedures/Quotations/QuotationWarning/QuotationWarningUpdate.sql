CREATE PROCEDURE [dbo].[QuotationWarningUpdate]
	@Id uniqueidentifier,
	@Data smalldatetime,
	@ProdutoCodigo nvarchar(256),
	@ComplementoCodigo nvarchar(128),
	@FornecedorCodigo nvarchar(5),
	@AvisoTipoCodigo nvarchar(5),
	@Descricao nvarchar(20)

AS

	DECLARE @Erro nvarchar(255)


	-- VALIDAR SE O REGISTO EXISTE
	IF NOT EXISTS(SELECT [Id] FROM [dbo].[CotacoesAvisos] WHERE [Id] = @Id)
	BEGIN
		-- DISPARAR UM ERRO
		SET @Erro = '[WhereToBuy-U01] ' + 'O registo que pretende alterar não existe!';
		RAISERROR (@Erro, 15, 1)
		RETURN 0
	END

	-- TUDO OK PARA SEGUIR EM FRENTE
	DECLARE  @NumeroLinhasAfetadas int

	-- ALTERAÇÃO DO REGISTO
	BEGIN TRY

		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF

		-- ALTERAR REGISTO
		UPDATE [dbo].[CotacoesAvisos] SET [Data] = @Data,
										[_ProdutoCodigo]=@ProdutoCodigo,
										[_ComplementoCodigo]=@ComplementoCodigo,
										[FornecedorCodigo] = @FornecedorCodigo,
										[AvisoTipoCodigo] = @AvisoTipoCodigo,
										[Descricao] = @Descricao
									WHERE [Id] = @Id

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
		SET @Erro = '[WhereToBuy-U10] ' + ERROR_MESSAGE();
		RAISERROR (@Erro, 15, 1)
		RETURN 0

	END CATCH
