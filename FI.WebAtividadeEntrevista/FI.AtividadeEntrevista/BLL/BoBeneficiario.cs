using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo Beneficiario
        /// </summary>
        /// <param name="Beneficiario">Objeto de Beneficiario</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um Beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de Beneficiario</param>
        public void Alterar(DML.Beneficiario beneficiario)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            benef.Alterar(beneficiario);
        }

        /// <summary>
        /// Consulta o Beneficiario pelo id
        /// </summary>
        /// <param name="id">id do Beneficiario</param>
        /// <returns></returns>
        public DML.Beneficiario Consultar(long id)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.Consultar(id);
        }

        /// <summary>
        /// Excluir o Beneficiario pelo id
        /// </summary>
        /// <param name="id">id do Beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            benef.Excluir(id);
        }

        /// <summary>
        /// Lista os Beneficiarios
        /// </summary>
        public List<DML.Beneficiario> Listar(long? id)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.Listar(id);
        }

        /// <summary>
        /// Lista os Beneficiarios
        /// </summary>
        public List<DML.Beneficiario> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.Pesquisa(iniciarEm,  quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// Validar se o CPF foi cadastrado no sistema
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public bool VerficarCpfExiste(string cpf)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.VerficarCpfExiste(cpf);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF)
        {
            DAL.DaoBeneficiario benef = new DAL.DaoBeneficiario();
            return benef.VerificarExistencia(CPF);
        }
    }
}
