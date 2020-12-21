
CREATE PROC [dbo].[FI_SP_AltBeneficiario]
	@CPF           VARCHAR (11),
    @NOME          VARCHAR (50) ,
    @IDCLIENTE     BIGINT,
	@Id            BIGINT
AS
BEGIN
	UPDATE BENEFICIARIOS 
	SET 
		CPF				= @CPF,
		NOME			= @NOME, 
		IDCLIENTE		= @IDCLIENTE
	WHERE Id			= @Id
END