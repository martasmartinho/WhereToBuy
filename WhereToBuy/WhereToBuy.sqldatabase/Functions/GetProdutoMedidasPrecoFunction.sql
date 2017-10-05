/*
	This function generates product statistics:

		> U1VP - last price variation (x = 1 not changed, x > 1 means price was rise and x < 1 means the price was dropped
		> U2VP - penultimate..
		> U3VP - antepenultimate..

		> EEP (Estatistica de Estabilidade de Preço) means Price Stability Statistic.
	          Represents a estimation in days of the stability of actual price.

		> ICP (Indice de Confiança no Preço) means Price Trust Index.
		      Thats a value between [0:1] that represents the current price trust.

			  ICP = 0.5CE + 0.2CT + 0.3CF

		> CE  (Componente Estabilidade) Estability Component
		      Is the price trust in prespective to EEP. 
		
		> CT  (Componente Tendencia) Trend Component
			  Is the price trust in prespective of last evolution of the price.

		> CF  (Confiança Fornecedor) Trust on the Supplier
			  Is the trust that exists on the supplier (supplier table record)

*/

CREATE FUNCTION [dbo].GetProdutoMedidasPrecoFunction
(
	@U3Preco decimal(14,4),
	@U3Data smalldatetime,
	@U2Preco decimal(14,4),
	@U2Data smalldatetime,
	@U1Preco decimal(14,4),
	@U1Data smalldatetime,
	@UPreco decimal(14,4),
	@UData smalldatetime,
	@ConfiancaFornecedor float
)
RETURNS @results TABLE
(
	__idMedidaPreco integer PRIMARY KEY NOT NULL IDENTITY(1,1),
	U3VP decimal(14,4) NULL,
	U2VP decimal(14,4) NULL,
	U1VP decimal(14,4) NULL,
	EEP float NOT NULL,
	ICP float NOT NULL,
	ICPCE float NOT NULL,
	ICPCT float NOT NULL,
	ICPCF float NOT NULL,
	EEPFormula nvarchar(256),
	ICPCEFormula nvarchar(256),
	ICPCTFormula nvarchar(256),
	ICPCFFormula nvarchar(256),
	ICPFormula nvarchar(256)
)
AS
BEGIN

	-- variables
	DECLARE @eepFormula NVARCHAR(256), @icpFormula nvarchar(256), @ceFormula nvarchar(256), @ctFormula nvarchar(256), @cfFormula nvarchar(256),
	        @u3vp float, @u2vp float, @u1vp float, @eep float, @icp float, @ce float, @ct float, @cf float;


	-- initialize variables
	SET @eepFormula = '';
	SET @icpFormula = '';
	SET @ceFormula = '';
	SET @ctFormula = '';
	SET @cfFormula = '';
	SET @u3vp = NULL;
	SET @u2vp = NULL;
	SET @u1vp = NULL;
	SET @eep = 1;	-- minimum stability: 1 day
	SET @icp = 0;
	SET @ce = 0;
	SET @ct = 0;
	SET @cf = 0



	-- trend calculation
	IF @U3Preco IS NOT NULL	-- if this price it is not null, then we have the guarantee that @U2Preco it isn't too.
	BEGIN
		SET @u3vp = @U2Preco / @U3Preco;
	END

	IF @U2Preco IS NOT NULL -- if this price it is not null, then we have the guarantee that @U1Preco it isn't too.
	BEGIN
		SET @u2vp = @U1Preco / @U2Preco;
	END

	IF @U1Preco IS NOT NULL -- if this price it is not null, then we have the guarantee that @UPreco it isn't too.
	BEGIN
		SET @u1vp = @UPreco / @U1Preco;
	END



	-- EEP and EEPFormula calculation
	SELECT @eep =
				CASE WHEN @U3Data IS NOT NULL THEN 
													0.1 * DATEDIFF(DAY, @U3Data, @U2Data) +
													0.4 * DATEDIFF(DAY, @U2Data, @U1Data) +
													0.5 * DATEDIFF(DAY, @U1Data, @UData)

					 WHEN @U2Data IS NOT NULL THEN	
													0.4 * DATEDIFF(DAY, @U2Data, @U1Data) +
													0.5 * DATEDIFF(DAY, @U1Data, @UData)

					 WHEN @U1Data IS NOT NULL THEN	
													0.5 * DATEDIFF(DAY, @U1Data, @UData)

					 ELSE 1
				END,

			@eepFormula = 
				CASE WHEN @U3Data IS NOT NULL THEN 
													'EEP = 0.1(U3Data - U2Data) + ' +
														  '0.4(U2Data - U1Data) + ' +
													      '0.5(U1Data - UData) <=> ' +
													'EEP = 0.1(' + CONVERT(varchar, @U3Data, 103) + ' - ' + CONVERT(varchar, @U2Data, 103)  + ') + ' +
													      '0.4(' + CONVERT(varchar, @U2Data, 103) + ' - ' + CONVERT(varchar, @U1Data, 103)  + ') + ' +
														  '0.5(' + CONVERT(varchar, @U1Data, 103) + ' - ' + CONVERT(varchar, @UData, 103)  + ') <=> ' +
													'EEP = ' + CAST(@eep as varchar)

					 WHEN @U2Data IS NOT NULL THEN	
													'EEP = 0.4(U2Data - U1Data) + ' +
													      '0.5(U1Data - UData) <=> ' +
													'EEP = 0.4(' + CONVERT(varchar, @U2Data, 103) + ' - ' + CONVERT(varchar, @U1Data, 103)  + ') + ' +
														  '0.5(' + CONVERT(varchar, @U1Data, 103) + ' - ' + CONVERT(varchar, @UData, 103)  + ') <=> ' +
													'EEP = ' + CAST(@eep as varchar)

					 WHEN @U1Data IS NOT NULL THEN	
													'EEP = 0.5(U1Data - UData) <=> ' +
													'EEP = 0.5(' + CONVERT(varchar, @U1Data, 103) + ' - ' + CONVERT(varchar, @UData, 103)  + ') <=> ' +
													'EEP = ' + CAST(@eep as varchar)

					 ELSE 'EEP = 1 (default)'
			    END



	
	-- CE (ICP component calculation)
	IF @UData IS NOT NULL
	BEGIN
		
		-- CE calculation
		SELECT @ce = (1 - (DATEDIFF(DAY, @UData, GETDATE()) / @eep));

		-- CE formula calculation
		SELECT @ceFormula = 'CE = (1 - ((Hoje - UData) / EEP)) <=> ' +
							'CE = (1 - ((' + CONVERT(varchar, GETDATE(), 103) + ' - ' + CONVERT(varchar, @UData, 103) + ') / ' + CAST(@eep as varchar)  + ')) <=> ' +
							'CE = ' + CAST(@ce as varchar);
	END
	ELSE
	BEGIN
		-- default
		SELECT @ce = 0, 
			   @ceFormula = 'CE = 0 (default)';
	END




	-- CT (ICP component calculation)
	IF @U1Preco IS NOT NULL -- we have sure that @UPreco is not null too
	BEGIN

		-- CT calculation
		SELECT @ct = 
				   CASE WHEN @UPreco > @U1Preco THEN (@U1Preco / @UPreco) - (50 * (1 - (@U1Preco / @UPreco)))
						ELSE (@UPreco / @U1Preco)
				   END;

		-- CT formula calculation
		SELECT @ctFormula = 
				   CASE WHEN @UPreco > @U1Preco THEN 
														'CT = (U1Preco / UPreco) - ' + 
																	'(50 * (1 - (@U1Preco / @UPreco))) <=> ' +
														'CT = (' + CONVERT(varchar, @U1Preco) + ' / ' + CONVERT(varchar, @UPreco) + ') - ' +
																	'(50 * (1 - (' + CONVERT(varchar, @U1Preco) + ' / ' + CONVERT(varchar, @UPreco) + '))) <=> ' +
														'CT = ' + CAST(@ct as varchar)
						ELSE 
							  'CT = (UPreco / U1Preco) <=> ' +
							  'CT = (' + CONVERT(varchar, @UPreco) + ' / ' + CONVERT(varchar, @U1Preco) + ') <=> ' +
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




	-- ICP calculation
	SELECT @icp = (0.5 * @ce) + (0.2 * @ct) + (0.3 * @cf);

	-- ICP Formula calculation
	SELECT @icpFormula = 'ICP = 0.5CE + 0.2CT + 0.3CF <=> ' + 
						 'ICP = (0.5 * ' + CAST(@ce as varchar) + ') + (0.2 * ' + CAST(@ct as varchar) + ') + (0.3 * ' + CAST(@cf as varchar) + ') <=> ' +
						 'ICP = ' + CAST(@icp as varchar);

	-- ICP must be inside [0:1] interval
	SELECT @icpFormula =
					CASE WHEN @icp < 0 THEN @icpFormula + ', !E [0:1] => ICP = 0'
					     WHEN @icp > 1 THEN @icpFormula + ', !E [0:1] => ICP = 1'
						 ELSE @icpFormula
					END,
			@icp =
					CASE WHEN @icp < 0 THEN 0
					     WHEN @icp > 1 THEN 1
						 ELSE @icp
					END;
		  



	-- send back results
	INSERT INTO @results
	SELECT @u3vp, @u2vp, @u1vp, @eep, @icp, @ce, @ct, @cf, @eepFormula, @ceFormula, @ctFormula, @cfFormula, @icpFormula;


	-- the end
	RETURN;
END
