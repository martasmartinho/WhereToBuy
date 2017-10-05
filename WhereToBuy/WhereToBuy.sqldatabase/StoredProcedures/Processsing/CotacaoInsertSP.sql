/*
	Esta Stored Procedure é tanto usada nas inserções unitárias (por exemplo a partir de um formulário na web), como
	nas inserções em lote..

	No caso das inserções em lote, antes da importação própriamente dita, as CotacoesIncompletas do fornecedor em questão 
	têm que ser removidas. Assim, o argumento @PrevineDuplicacoes deve ser passado com o valor 'false' já que esta prevenção
	foi efetuada com antecedência.

	No caso das inserções unitárias, o parametro @PrevineDuplicacoes deve ser passado com o valor 'true' para que a remoção
	de qualquer cotação incompleta, seja feita pela própria SP.

	!!! IMPORTANTE !!!
	> Existem campo que têm que ser obrigatóriamente indicados e outros que, se forem null (ou seja não indicados) serão sugeridos
	  automáticamente com (vazio).
	
	> Os campos Validade e ValidadeFormula são recebidos porque os prazos de validade de uma cotação podem ser
	  também comunicados pelo fornecedor. Se forem recebidos como NULL, então é o WhereToBuy que vai atribuir a validade.
*/

CREATE PROCEDURE [dbo].[CotacaoInsertSP]
	@PrevineDuplicacoes bit,
	@FornecedorCodigo nvarchar(20),
	@Data datetime,
	@_ProdutoCodigo nvarchar(256),
	@_ComplementoCodigo nvarchar(128),
	@_ComplementoDescricao nvarchar(128),
	@_Partnumber nvarchar(25),
	@_MarcaCodigo nvarchar(128),
	@_MarcaDescricao nvarchar(128),
	@_CategoriaCodigo nvarchar(128),
	@_CategoriaDescricao nvarchar(128),
	@_StockCodigo nvarchar(128),
	@_StockDescricao nvarchar(128),
	@_ImpostoCodigo nvarchar(128),
	@_ImpostoDescricao nvarchar(128),
	@_EstadoCodigo nvarchar(128),
	@_EstadoDescricao nvarchar(128),
	@_Descricao nvarchar(256),
	@_Link nvarchar(1024),
	@_Caracteristicas nvarchar(2048),
	@_Imagem nvarchar(1024),
	@_Preco decimal(14,4),
	@_OutrosCustos decimal(14,4),
	@_OutrosCustosDescricao nvarchar(128),
	@_Validade smalldatetime,
	@_ValidadeDescricao nvarchar(128),
	@Info nvarchar(256) OUTPUT
AS

	-- variaveis
	DECLARE @Partnumber nvarchar(25);


	/*
		Como esta SP é executada automaticamente para cada linha com o SSIS, pretende-se preencher duas colunas adicionais no resulset
		com o objetivo de, linha a linha, ser possivel perceber o problema encontrado.
	
		Para atingir este objetivo existem dois parametros a ter em conta:
			> @Info é um parametro de saída e deve conter a mensagem a colocar na linha / coluna em processamento no SSIS
			> @RETURN_VALUE que é o interno enviado à frente do RETURN e que corresponde ao numero de registos inseridos.

		Assim, se a SP não for executada pelo SSIS mas sim por uma outra aplicação, caso o RETURN_VALUE seja 0 (zero), o programador 
		deve considerar a mensagem de erro que existirá no parametro de saída @info.
	*/



	-- @PrevineDuplicacoes
	IF @PrevineDuplicacoes IS NULL
	BEGIN
		SET @Info = '[CotacaoInsertSP-01] (StoredProcedure)' + 'o valor do parametro @PrevineDuplicacoes não pode ser nulo!';
		RETURN 0
	END




	-- @FornecedorCodigo
	IF @FornecedorCodigo IS NULL
	BEGIN
		SET @Info = '[CotacaoInsertSP-02] (StoredProcedure)' + 'o valor do parametro @FornecedorCodigo não pode ser nulo!';
		RETURN 0
	END

	IF LEN(RTRIM(@FornecedorCodigo)) < 1
	BEGIN
		SET @Info ='[CotacaoInsertSP-03] (StoredProcedure)' + 'o valor do parametro @FornecedorCodigo tem um tamanho inválido!';
		RETURN 0
	END



	-- @Data
	IF @Data IS NULL
	BEGIN
		SET @Info = '[CotacaoInsertSP-04] (StoredProcedure)' + 'o valor do parametro @Data não pode ser nulo!';
		RETURN 0
	END



	-- @_ProdutoCodigo
	/*
		SERÁ AVALIADO APENAS NO FINAL DOS OUTROS PARAMETROS, PORQUE PRECISAMOS DO VALOR DA MARCA E PARTNUMBER PARA CONSTRUIR UM 
		CÓDIGO DE FORNECEDOR CASO ESTE NÃO ENVIE UM.
	*/



	-- @_ComplementoCodigo
	IF @_ComplementoCodigo IS NULL
	BEGIN
		SET @_ComplementoCodigo = '(vazio)';
	END

	IF LEN(RTRIM(@_ComplementoCodigo)) < 1
	BEGIN
		SET @_ComplementoCodigo = '(vazio)';
	END



	-- @_ComplementoDescricao
	IF @_ComplementoDescricao IS NULL
	BEGIN
		SET @_ComplementoDescricao = @_ComplementoCodigo;
	END

	IF LEN(RTRIM(@_ComplementoDescricao)) < 1
	BEGIN
		SET @_ComplementoDescricao = @_ComplementoCodigo;
	END



	-- @_Partnumber
	IF @_Partnumber IS NULL
	BEGIN
		SET @Info = '[CotacaoInsertSP-11] (StoredProcedure)' + 'o valor do parametro @_Partnumber não pode ser nulo!';
		RETURN 0
	END

	IF LEN(RTRIM(@_Partnumber)) < 1
	BEGIN
		SET @Info = '[CotacaoInsertSP-12] (StoredProcedure)' + 'o valor do parametro @_Partnumber tem um tamanho inválido!';
		RETURN 0
	END



	-- @_MarcaCodigo
	IF @_MarcaCodigo IS NULL
	BEGIN
		SET @Info ='[CotacaoInsertSP-13] (StoredProcedure)' + 'o valor do parametro @_MarcaCodigo não pode ser nulo!';
		RETURN 0
	END

	IF LEN(RTRIM(@_MarcaCodigo)) < 1
	BEGIN
		SET @Info = '[CotacaoInsertSP-14] (StoredProcedure)' + 'o valor do parametro @_MarcaCodigo tem um tamanho inválido!';
		RETURN 0
	END



	-- @_MarcaDescricao
	IF @_MarcaDescricao IS NULL
	BEGIN
		SET @_MarcaDescricao = @_MarcaCodigo;
	END

	IF LEN(RTRIM(@_MarcaDescricao)) < 1
	BEGIN
		SET @_MarcaDescricao = @_MarcaCodigo;
	END



	-- @_CategoriaCodigo
	IF @_CategoriaCodigo IS NULL
	BEGIN
		SET @Info = '[CotacaoInsertSP-17] (StoredProcedure)' + 'o valor do parametro @_CategoriaCodigo não pode ser nulo!';
		RETURN 0
	END

	IF LEN(RTRIM(@_CategoriaCodigo)) < 1
	BEGIN
		SET @Info = '[CotacaoInsertSP-18] (StoredProcedure)' + 'o valor do parametro @_CategoriaCodigo tem um tamanho inválido!';
		RETURN 0
	END




	-- @_CategoriaDescricao
	IF @_CategoriaDescricao IS NULL
	BEGIN
		SET @_CategoriaDescricao = @_CategoriaCodigo;
	END

	IF LEN(RTRIM(@_CategoriaDescricao)) < 1
	BEGIN
		SET @_CategoriaDescricao = @_CategoriaCodigo;
	END




	-- @_StockCodigo
	IF @_StockCodigo IS NULL
	BEGIN
		SET @_StockCodigo = '(vazio)';
	END

	IF LEN(RTRIM(@_StockCodigo)) < 1
	BEGIN
		SET @_StockCodigo = '(vazio)';
	END



	
	-- @_StockDescricao
	IF @_StockDescricao IS NULL
	BEGIN
		SET @_StockDescricao = @_StockCodigo;
	END

	IF LEN(RTRIM(@_StockDescricao)) < 1
	BEGIN
		SET @_StockDescricao = @_StockCodigo;
	END




	-- @_ImpostoCodigo
	IF @_ImpostoCodigo IS NULL
	BEGIN
		SET @_ImpostoCodigo = '(vazio)';
	END

	IF LEN(RTRIM(@_ImpostoCodigo)) < 1
	BEGIN
		SET @_ImpostoCodigo = '(vazio)';
	END



	
	-- @_ImpostoDescricao
	IF @_ImpostoDescricao IS NULL
	BEGIN
		SET @_ImpostoDescricao = @_ImpostoCodigo;
	END

	IF LEN(RTRIM(@_ImpostoDescricao)) < 1
	BEGIN
		SET @_ImpostoDescricao = @_ImpostoCodigo;
	END




	-- @_EstadoCodigo
	IF @_EstadoCodigo IS NULL
	BEGIN
		SET @_EstadoCodigo = '(vazio)';
	END

	IF LEN(RTRIM(@_EstadoCodigo)) < 1
	BEGIN
		SET @_EstadoCodigo = '(vazio)';
	END



	
	-- @_EstadoDescricao
	IF @_EstadoDescricao IS NULL
	BEGIN
		SET @_EstadoDescricao = @_EstadoCodigo;
	END

	IF LEN(RTRIM(@_EstadoDescricao)) < 1
	BEGIN
		SET @_EstadoDescricao = @_EstadoCodigo;
	END




	-- @_Descricao
	IF @_Descricao IS NULL
	BEGIN
		SET @Info = '[CotacaoInsertSP-29] (StoredProcedure)' + 'o valor do parametro @_Descricao não pode ser nulo!';
		RETURN 0
	END

	IF LEN(RTRIM(@_Descricao)) < 1
	BEGIN
		SET @Info = '[CotacaoInsertSP-30] (StoredProcedure)' + 'o valor do parametro @_Descricao tem um tamanho inválido!';
		RETURN 0
	END




	-- @_Link
	IF @_Link IS NULL
	BEGIN
		SET @_Link = '(vazio)';
	END

	IF LEN(RTRIM(@_Link)) < 1
	BEGIN
		SET @_Link = '(vazio)';
	END




	-- @_Caracteristicas
	IF @_Caracteristicas IS NULL
	BEGIN
		SET @_Caracteristicas = '(vazio)';
	END

	IF LEN(RTRIM(@_Caracteristicas)) < 1
	BEGIN
		SET @_Caracteristicas = '(vazio)';
	END




	-- @_Imagem
	IF @_Imagem IS NULL
	BEGIN
		SET @_Imagem = '(vazio)';
	END

	IF LEN(RTRIM(@_Imagem)) < 1
	BEGIN
		SET @_Imagem = '(vazio)';
	END





	-- @_Preco
	IF @_Preco IS NULL
	BEGIN
		SET @Info = '[CotacaoInsertSP-37] (StoredProcedure)' + 'o valor do parametro @_Preco não pode ser nulo!';
		RETURN 0
	END





	-- @_OutrosCustos
	IF @_OutrosCustos IS NULL
	BEGIN
		SET @_OutrosCustos = 0.0;
	END





	-- @_OutrosCustosDescricao
	IF @_OutrosCustosDescricao IS NULL
	BEGIN
		SET @_OutrosCustosDescricao = '(vazio)';
	END

	IF LEN(RTRIM(@_OutrosCustosDescricao)) < 1
	BEGIN
		SET @_OutrosCustosDescricao = '(vazio)';
	END




	-- @_Validade
	IF @_Validade IS NULL
	BEGIN
		SET @_Validade = CAST('01-01-2000' AS smalldatetime);
	END





	-- @_ValidadeDescricao
	IF @_ValidadeDescricao IS NULL
	BEGIN
		SET @_ValidadeDescricao = '(vazio)';
	END

	IF LEN(RTRIM(@_ValidadeDescricao)) < 1
	BEGIN
		SET @_OutrosCustosDescricao = '(vazio)';
	END





	-- PREPARAR @Partnumber (INTERNO)
	SET @Partnumber = dbo.GetCotacaoPartnumberFunction(@_Partnumber);



	-- impor regras para códigos externos (sistemas operacionais externos)
	SET @_ComplementoCodigo = dbo.GetCodigoExternoNormalizadoFunction(@_ComplementoCodigo);
	SET @_MarcaCodigo = dbo.GetCodigoExternoNormalizadoFunction(@_MarcaCodigo);
	SET @_CategoriaCodigo = dbo.GetCodigoExternoNormalizadoFunction(@_CategoriaCodigo);
	SET @_StockCodigo = dbo.GetCodigoExternoNormalizadoFunction(@_StockCodigo);
	SET @_ImpostoCodigo = dbo.GetCodigoExternoNormalizadoFunction(@_ImpostoCodigo);
	SET @_EstadoCodigo = dbo.GetCodigoExternoNormalizadoFunction(@_EstadoCodigo);



	-- @_ProdutoCodigo
	IF @_ProdutoCodigo IS NULL
	BEGIN
		SET @_ProdutoCodigo = '(' + @_MarcaCodigo + ')' + @Partnumber; -- uso o @Partnumber interno porque está limpo de caracteres anómalos
	END

	IF LEN(RTRIM(@_ProdutoCodigo)) < 1
	BEGIN
		SET @_ProdutoCodigo = '(' + @_MarcaCodigo + ')' + @Partnumber; -- uso o @Partnumber interno porque está limpo de caracteres anómalos
	END




	-- PREVINE DUPLICAÇÕES (remove a CotacaoNaoIntegrada que possa existir para este FornecedorCodigo / _ProdutoCodigo
	IF @PrevineDuplicacoes = CAST('true' as bit)
	BEGIN
		-- remove cotacoes já completas mas ainda não integradas
		DELETE FROM [dbo].CotacoesNaoIntegradasView
			   WHERE [FornecedorCodigo] = @FornecedorCodigo
					 AND [_ProdutoCodigo] = @_ProdutoCodigo

		-- remove cotações incompletas
		DELETE FROM [dbo].CotacoesIncompletasView
			   WHERE [FornecedorCodigo] = @FornecedorCodigo
					 AND [_ProdutoCodigo] = @_ProdutoCodigo

	END




	-- INSERIR
	SET NOCOUNT ON; -- não há necessidade de contar os registos (é sempre 1)


	BEGIN TRY
	
		INSERT INTO [dbo].[Cotacoes]
			   (
				   [FornecedorCodigo], [Data], [_ProdutoCodigo], [_ComplementoCodigo], [_ComplementoDescricao],
				   [_Partnumber], [_MarcaCodigo], [_MarcaDescricao], [_CategoriaCodigo], [_CategoriaDescricao], [_StockCodigo], [_StockDescricao],
				   [_ImpostoCodigo], [_ImpostoDescricao], [_EstadoCodigo], [_EstadoDescricao], [_Descricao], [_Link], [_Caracteristicas], [_Imagem], 
				   [_Preco], [_OutrosCustos], [_OutrosCustosDescricao], [_Validade], [_ValidadeDescricao],
				   [ComplementoCodigo], [Partnumber], [MarcaCodigo], [CategoriaCodigo], 
				   [StockCodigo], [StockCodigoSubstituto], [StockCodigoSubstitutoJustificacao], 
				   [ImpostoCodigo], [EstadoCodigo], [Descricao], [Link], [Caracteristicas], [PrecoCusto], [PrecoCustoFormula],
				   [Validade], [ValidadeFormula], [ProdutoCodigo], 
				   [Completo], [Integrado],
				   [Inativo], [Versao]
				)
				VALUES
				(
				   @FornecedorCodigo, @Data, @_ProdutoCodigo, @_ComplementoCodigo, @_ComplementoDescricao,
				   @_Partnumber, @_MarcaCodigo, @_MarcaDescricao, @_CategoriaCodigo, @_CategoriaDescricao, @_StockCodigo, @_StockDescricao,
				   @_ImpostoCodigo, @_ImpostoDescricao, @_EstadoCodigo, @_EstadoDescricao, @_Descricao, @_Link, @_Caracteristicas, @_Imagem, 
				   @_Preco, @_OutrosCustos, @_OutrosCustosDescricao, @_Validade, @_ValidadeDescricao,
				   null, @Partnumber, null, null, 
				   null, null, null, 
				   null, null, null, null, null, null, null, 
				   null, null, null, 
				   CAST('false' as bit), CAST('false' as bit),
				   CAST('false' as bit), GETDATE()
				);


		-- preencher output com informação - sucesso
		SET @Info = 'Cotação inserida com sucesso.';

		-- devolver informação que foi inserido um registo
		RETURN 1;

	END TRY
	BEGIN CATCH

		-- preencher output com informação - sucesso
		SET @Info = '[CotacaoInsertSP-50] ' +CAST(ERROR_NUMBER() as nvarchar) + ' - ' +ERROR_MESSAGE();

		-- devolver informação que foi inserido um registo
		RETURN 1;

	END CATCH

RETURN;
