CREATE PROC FI_SP_VerficarCpfExiste
	@cpf varchar(11)
AS
BEGIN
	SELECT COUNT(1) as Cpf FROM CLIENTES WITH(NOLOCK) WHERE CPF = @cpf 
END



