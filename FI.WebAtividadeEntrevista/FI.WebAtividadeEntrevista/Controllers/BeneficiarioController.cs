using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        private void CarregarCombo(long? idCliente)
        {
            BoCliente boCliente = new BoCliente();

            var listaCliente = boCliente.Listar();

            if (idCliente == null)
            {
                ViewBag.IdCliente = new SelectList(listaCliente, "Id", "Nome");
            }
            else
            {
                ViewBag.IdCliente = new SelectList(listaCliente.Where(x => x.Id == idCliente), "Id", "Nome", idCliente);
            }
            
        }

        public ActionResult Incluir()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            try
            {

                BoBeneficiario bo = new BoBeneficiario();

                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, erros));
                }

                var cpfCadastrado = ValidarCpfCadastrado(model.Cpf);

                if (cpfCadastrado)
                {
                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, "CPF jรก cadastrado no sistema!"));
                }

                model.Id = bo.Incluir(new Beneficiario()
                {
                    Cpf = model.Cpf,
                    Nome = model.Nome,
                    IdCliente = model.IdCliente
                });

                return Json("Cadastro Efetuado com Sucesso");
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }

        private bool ValidarCpfCadastrado(string cpf)
        {
            BoBeneficiario bo = new BoBeneficiario();

            var cpfExiste = bo.VerficarCpfExiste(cpf);
            return cpfExiste;
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            try
            {
                BoBeneficiario bo = new BoBeneficiario();

                Beneficiario beneficiario = bo.Consultar(id);
                Models.BeneficiarioModel model = null;

                if (beneficiario != null)
                {
                    model = new BeneficiarioModel()
                    {
                        Id = beneficiario.Id,
                        Cpf = beneficiario.Cpf,
                        Nome = beneficiario.Nome,
                        IdCliente = beneficiario.IdCliente
                    };
                }

                return PartialView(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
        {
            try
            {
                BoBeneficiario bo = new BoBeneficiario();

                if (!this.ModelState.IsValid)
                {
                    List<string> erros = (from item in ModelState.Values
                                          from error in item.Errors
                                          select error.ErrorMessage).ToList();

                    Response.StatusCode = 400;
                    return Json(string.Join(Environment.NewLine, erros));
                }

                bo.Alterar(new Beneficiario()
                {
                    Id = model.Id,
                    Cpf = model.Cpf,
                    Nome = model.Nome,
                    IdCliente = model.IdCliente
                });

                return Json("Cadastro alterado com sucesso");

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult BeneficiarioList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = String.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Beneficiario> beneficiarios = new BoBeneficiario().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = beneficiarios, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult Deletar(long id)
        {
            BoBeneficiario bo = new BoBeneficiario();
            bo.Excluir(id);
            return Json("Beneficiário deletado com sucesso!!");
        }

        [HttpGet]
        public JsonResult CarregarComboCliente()
        {
            try
            {
                BoCliente boCliente = new BoCliente();
                var listaCliente = boCliente.Listar();
                return Json(listaCliente, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

    }
}



