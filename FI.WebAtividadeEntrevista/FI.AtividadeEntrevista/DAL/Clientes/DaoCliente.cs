using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FI.AtividadeEntrevista.DML;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Cliente
    /// </summary>
    internal class DaoCliente : AcessoDados
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal long Incluir(DML.Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            RemoverCaracteres(cliente);

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", cliente.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Sobrenome", cliente.Sobrenome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nacionalidade", cliente.Nacionalidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CEP", cliente.CEP));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Estado", cliente.Estado));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cidade", cliente.Cidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Logradouro", cliente.Logradouro));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Email", cliente.Email));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Telefone", cliente.Telefone));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cpf", cliente.Cpf));

            DataSet ds = base.Consultar("FI_SP_IncClienteV2", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal DML.Cliente Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            return cli.FirstOrDefault();
        }

        internal bool VerificarExistencia(string CPF)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));

            DataSet ds = base.Consultar("FI_SP_VerificaCliente", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new System.Data.SqlClient.SqlParameter("quantidade", quantidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("crescente", crescente));

            DataSet ds = base.Consultar("FI_SP_PesqCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return cli;
        }

        internal bool VerficarCpfExiste(string cpf)
        {
            cpf = !string.IsNullOrEmpty(cpf) ? cpf.Replace(".", "").Replace("-", ""): default;
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();
            parametros.Add(new System.Data.SqlClient.SqlParameter("cpf", cpf));

            DataSet ds = base.Consultar("FI_SP_VerficarCpfExiste", parametros);

            DML.Cliente cli = ConverterVerificarCpfExiste(ds);

            return cli.Cpf == "1";
        }

        /// <summary>
        /// Método responsável de remover os caracteres
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        private Cliente RemoverCaracteres(Cliente cliente)
        {
            
            if (!string.IsNullOrEmpty(cliente.Cpf))
            {
                cliente.Cpf = cliente.Cpf.Replace(".", "").Replace("-", "");
            }

            if (!string.IsNullOrEmpty(cliente.CEP))
            {
                cliente.CEP = cliente.CEP.Replace("-", "");
            }

            if (!string.IsNullOrEmpty(cliente.Telefone))
            {
                cliente.Telefone = cliente.Telefone.Replace("(", "").Replace(")", "").Replace(" ", "");
            }

            return cliente;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<DML.Cliente> Listar()
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            long id = 0;
            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", id));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);
            List<DML.Cliente> cli = Converter(ds);

            return cli;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(DML.Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            RemoverCaracteres(cliente);

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", cliente.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Sobrenome", cliente.Sobrenome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nacionalidade", cliente.Nacionalidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CEP", cliente.CEP));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Estado", cliente.Estado));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cidade", cliente.Cidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Logradouro", cliente.Logradouro));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Email", cliente.Email));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Telefone", cliente.Telefone));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cpf", cliente.Cpf));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", cliente.Id));

            base.Executar("FI_SP_AltCliente", parametros);
        }


        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("FI_SP_DelCliente", parametros);
        }

        private List<DML.Cliente> Converter(DataSet ds)
        {
            List<DML.Cliente> lista = new List<DML.Cliente>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Cliente cli = new DML.Cliente();
                    cli.CEP = row.Field<string>("CEP");
                    cli.Cidade = row.Field<string>("Cidade");
                    cli.Email = row.Field<string>("Email");
                    cli.Estado = row.Field<string>("Estado");
                    cli.Logradouro = row.Field<string>("Logradouro");
                    cli.Nacionalidade = row.Field<string>("Nacionalidade");
                    cli.Nome = row.Field<string>("Nome");
                    cli.Sobrenome = row.Field<string>("Sobrenome");
                    cli.Telefone = row.Field<string>("Telefone");
                    cli.Id = row.Field<long>("Id");
                    cli.Cpf = row.Field<string>("Cpf");
                    lista.Add(cli);
                }
            }

            return lista;
        }

        private DML.Cliente ConverterVerificarCpfExiste(DataSet ds)
        {
            DML.Cliente cli = new DML.Cliente();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    cli.Cpf = row.Field<int>("Cpf").ToString();
                }
            }

            return cli;
        }
    }
}
