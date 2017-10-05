CREATE PROCEDURE [dbo].[QuotationWarningInsert]
	@Id uniqueidentifier,
	@Data smalldatetime,
	@ProdutoCodigo nvarchar(256),
	@ComplementoCodigo nvarchar(128),
	@FornecedorCodigo nvarchar(5),
	@AvisoTipoCodigo nvarchar(5),
	@Descricao nvarchar(20)
AS
	
	DECLARE @Criacao smalldatetime, @NumeroLinhasAfetadas int, @Erro nvarchar(255)

	SET @Criacao = GETDATE()


	-- INSERÇÃO DO REGISTO
	BEGIN TRY
		 
		-- INICIA CONTAGEM DE @@ROWCOUNT (para quando usado o ExecuteNonQuery())
		SET NOCOUNT OFF

		-- INSERIR REGISTO
		INSERT INTO [dbo].[CotacoesAvisos] ([Id], [Data], [_ProdutoCodigo], [_ComplementoCodigo], [FornecedorCodigo], [AvisoTipoCodigo], [Descricao], [Criacao]) 
					VALUES (@Id, @Data, @ProdutoCodigo, @ComplementoCodigo, @FornecedorCodigo, @AvisoTipoCodigo, @Descricao, @Criacao);
		
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
