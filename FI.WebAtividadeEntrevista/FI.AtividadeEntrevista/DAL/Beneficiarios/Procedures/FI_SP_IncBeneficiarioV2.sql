CREATE PROC [dbo].[FI_SP_IncBeneficiarioV2]
    @CPF           VARCHAR (11),
	@NOME          VARCHAR (50) ,
    @IDCLIENTE     BIGINT
  
AS
BEGIN
	INSERT INTO BENEFICIARIOS (CPF, NOME, IDCLIENTE) 
	VALUES (@CPF, @NOME, @IDCLIENTE)

	SELECT SCOPE_IDENTITY()
END