CREATE PROC FI_SP_VerficarCpfExisteBeneficiario
	@cpf varchar(11)
AS
BEGIN
	SELECT COUNT(1) as Cpf FROM BENEFICIARIOS WITH(NOLOCK) WHERE CPF = @cpf 
END



