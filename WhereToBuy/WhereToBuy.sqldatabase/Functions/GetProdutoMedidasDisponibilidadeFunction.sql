/*
	This function generates product statistics:

		> U1VD - last availability variation (x = 1 not changed, x > 1 means availability level was rise and x < 1 means the availability level was dropped
		> U2VD - penultimate..
		> U3VD - antepenultimate..

		> EED (Estatistica de Estabilidade da Disponibilidade) means Statistic of Stability of Availability.
	          Represents a estimation in days of the stability of actual Stock Availability level.
			  How much days will the current level of availability stay actual.

		> ICD (Indice de Confiança na Disponibilidade) means Availability Trust Index.
		      Thats a value between [0:1] that represents the current level of availability trust.

			  ICD = 0.5CE + 0.2CT + 0.3CF

		> CE  (Componente Estabilidade) Estability Component
		      Is the availability level trust in prespective to EED. 
		
		> CT  (Componente Tendencia) Trend Component
			  Is the availability trust in prespective of last evolution of stock availability.

		> CF  (Confiança Fornecedor) Trust on the Supplier
			  Is the trust that exists on the supplier (supplier table record)

*/

CREATE FUNCTION [dbo].GetProdutoMedidasDisponibilidadeFunction
(
	@U3Disponivel smallint,
	@U3Data smalldatetime,
	@U2Disponivel smallint,
	@U2Data smalldatetime,
	@U1Disponivel smallint,
	@U1Data smalldatetime,
	@UDisponivel smallint,
	@UData smalldatetime,
	@ConfiancaFornecedor float
)
RETURNS @results TABLE
(
	__idMedidaDisponibilidade integer PRIMARY KEY NOT NULL IDENTITY(1,1),
	U3VD float NULL,
	U2VD float NULL,
	U1VD float NULL,
	EED float NOT NULL,
	ICD float NOT NULL,
	ICDCE float NOT NULL,
	ICDCT float NOT NULL,
	ICDCF float NOT NULL,
	EEDFormula nvarchar(256),
	ICDCEFormula nvarchar(256),
	ICDCTFormula nvarchar(256),
	ICDCFFormula nvarchar(256),
	ICDFormula nvarchar(256)
)
AS
BEGIN

	-- variables
	DECLARE @eedFormula nvarchar(256), @icdFormula nvarchar(256), @ceFormula nvarchar(256), @ctFormula nvarchar(256), @cfFormula nvarchar(256),
	        @u3vd float, @u2vd float, @u1vd float, @eed float, @icd float, @ce float, @ct float, @cf float;


	-- initialize variables
	SET @eedFormula = '';
	SET @icdFormula = '';
	SET @ceFormula = '';
	SET @ctFormula = '';
	SET @cfFormula = '';
	SET @u3vd = NULL;
	SET @u2vd = NULL;
	SET @u1vd = NULL;
	SET @eed = 1;	-- minimum stability: 1 day
	SET @icd = 0;
	SET @ce = 0;
	SET @ct = 0;
	SET @cf = 0



	-- trend calculation
	IF @U3Disponivel IS NOT NULL	-- if this level it is not null, then we have the guarantee that @U2Disponivel it isn't too.
	BEGIN
		SET @u3vd = CAST(@U2Disponivel as float) - @U3Disponivel;
	END

	IF @U2Disponivel IS NOT NULL	-- if this level it is not null, then we have the guarantee that @U1Disponivel it isn't too.
	BEGIN
		SET @u2vd = CAST(@U1Disponivel as float) - @U2Disponivel;
	END

	IF @U1Disponivel IS NOT NULL	-- if this level it is not null, then we have the guarantee that @UDisponivel it isn't too.
	BEGIN
		SET @u1vd = CAST(@UDisponivel as float) - @U1Disponivel;
	END



	-- EED and EEDFormula calculation
	SELECT @eed =
				CASE WHEN @U3Data IS NOT NULL THEN 
													0.1 * DATEDIFF(DAY, @U3Data, @U2Data) +
													0.2 * DATEDIFF(DAY, @U2Data, @U1Data) +
													0.7 * DATEDIFF(DAY, @U1Data, @UData)

					 WHEN @U2Data IS NOT NULL THEN	
													0.2 * DATEDIFF(DAY, @U2Data, @U1Data) +
													0.7 * DATEDIFF(DAY, @U1Data, @UData)

					 WHEN @U1Data IS NOT NULL THEN	
													0.7 * DATEDIFF(DAY, @U1Data, @UData)

					 ELSE 1
				END,

			@eedFormula = 
				CASE WHEN @U3Data IS NOT NULL THEN 
													'EED = 0.1(U3Data - U2Data) + ' +
														  '0.2(U2Data - U1Data) + ' +
													      '0.7(U1Data - UData) <=> ' +
													'EED = 0.1(' + CONVERT(varchar, @U3Data, 103) + ' - ' + CONVERT(varchar, @U2Data, 103)  + ') + ' +
													      '0.2(' + CONVERT(varchar, @U2Data, 103) + ' - ' + CONVERT(varchar, @U1Data, 103)  + ') + ' +
														  '0.7(' + CONVERT(varchar, @U1Data, 103) + ' - ' + CONVERT(varchar, @UData, 103)  + ') <=> ' +
													'EED = ' + CAST(@eed as varchar)

					 WHEN @U2Data IS NOT NULL THEN	
													'EED = 0.2(U2Data - U1Data) + ' +
													      '0.7(U1Data - UData) <=> ' +
													'EED = 0.2(' + CONVERT(varchar, @U2Data, 103) + ' - ' + CONVERT(varchar, @U1Data, 103)  + ') + ' +
														  '0.7(' + CONVERT(varchar, @U1Data, 103) + ' - ' + CONVERT(varchar, @UData, 103)  + ') <=> ' +
													'EED = ' + CAST(@eed as varchar)

					 WHEN @U1Data IS NOT NULL THEN	
													'EED = 0.7(U1Data - UData) <=> ' +
													'EED = 0.7(' + CONVERT(varchar, @U1Data, 103) + ' - ' + CONVERT(varchar, @UData, 103)  + ') <=> ' +
													'EED = ' + CAST(@eed as varchar)

					 ELSE 'EED = 1 (default)'
			    END



	
	-- CE (ICD component calculation)
	IF @UData IS NOT NULL
	BEGIN
		
		-- CE calculation
		SELECT @ce = (1 - (DATEDIFF(DAY, @UData, GETDATE()) / @eed));

		-- CE formula calculation
		SELECT @ceFormula = 'CE = (1 - ((Hoje - UData) / EEP)) <=> ' +
							'CE = (1 - ((' + CONVERT(varchar, GETDATE(), 103) + ' - ' + CONVERT(varchar, @UData, 103) + ') / ' + CAST(@eed as varchar)  + ')) <=> ' +
							'CE = ' + CAST(@ce as varchar);
	END
	ELSE
	BEGIN
		-- default
		SELECT @ce = 0, 
			   @ceFormula = 'CE = 0 (default)';
	END




	-- CT (ICD component calculation)
	IF @U1Disponivel IS NOT NULL -- we have sure that @UDisponivel is not null too
	BEGIN

		-- CT calculation
		SELECT @ct = 
				   CASE WHEN @UDisponivel < @U1Disponivel THEN (1 - ABS(((CAST(@UDisponivel as float) - @U1Disponivel) / 11) * 5))
						ELSE (1 - ABS((CAST(@UDisponivel as float) - @U1Disponivel) / 11))
				   END;

		-- CT formula calculation
		SELECT @ctFormula = 
				   CASE WHEN @UDisponivel < @U1Disponivel THEN 
														'CT = (1 - |((UDisponivel - U1Disponivel) / 11) * 5|) <=> ' + 
														'CT = (1 - |(((' + CONVERT(varchar, @UDisponivel) + ') - (' + CONVERT(varchar, @U1Disponivel) + ')) / 11) * 5|) <=> ' +
														'CT = ' + CAST(@ct as varchar)
						ELSE 
							  'CT = (1 - |(UDisponivel - U1Disponivel) / 11|) <=> ' + 
							  'CT = (1 - |((' + CONVERT(varchar, @UDisponivel) + ') - (' + CONVERT(varchar, @U1Disponivel) + ')) / 11|) <=> ' +
							  'CT = ' + CAST(@ct as varchar)
				   END;
	END
	ELSE
	BEGIN
		-- default
		SELECT @ct = 0, 
			   @ctFormula = 'CT = 0 (default)';
	END




	-- CF (ICP component calculation)
	IF @ConfiancaFornecedor IS NOT NULL
	BEGIN
		
		-- CF calculation
		SELECT @cf = @ConfiancaFornecedor / 100;

		-- CF formula calculation
		SELECT @cfFormula = 'CF = (ConfiancaFornecedor / 100) <=> ' +
							'CF = (' + CAST(@ConfiancaFornecedor as varchar) + ' / 100) <=> ' +
							'CF = ' + CAST(@cf as varchar);

	END
	ELSE
	BEGIN
		-- default
		SELECT @cf = 0, 
			   @cfFormula = 'CF = 0 (default)';
	END




	-- ICD calculation
	SELECT @icd = (0.5 * @ce) + (0.2 * @ct) + (0.3 * @cf);

	-- ICD Formula calculation
	SELECT @icdFormula = 'ICD = 0.5CE + 0.2CT + 0.3CF <=> ' + 
						 'ICD = (0.5 * ' + CAST(@ce as varchar) + ') + (0.2 * ' + CAST(@ct as varchar) + ') + (0.3 * ' + CAST(@cf as varchar) + ') <=> ' +
						 'ICD = ' + CAST(@icd as varchar);

	-- ICD must be inside [0:1] interval
	SELECT @icdFormula =
					CASE WHEN @icd < 0 THEN @icdFormula + ', !E [0:1] => ICP = 0'
					     WHEN @icd > 1 THEN @icdFormula + ', !E [0:1] => ICP = 1'
						 ELSE @icdFormula
					END,
			@icd =
					CASE WHEN @icd < 0 THEN 0
					     WHEN @icd > 1 THEN 1
						 ELSE @icd
					END;
		  


	-- send back results
	INSERT INTO @results
	SELECT @u3vd, @u2vd, @u1vd, @eed, @icd, @ce, @ct, @cf, @eedFormula, @ceFormula, @ctFormula, @cfFormula, @icdFormula;


	-- the end
	RETURN;
END
