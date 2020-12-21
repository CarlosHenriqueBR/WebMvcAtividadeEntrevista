using System.ComponentModel.DataAnnotations;
using WebAtividadeEntrevista.Models.Validator;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Beneficiario
    /// </summary>
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        [Cpf(ErrorMessage = "O valor '{0}' é inválido para CPF")]
        public string Cpf { get; set; }

        /// <summary>
        /// CEP
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        ///  IdCliente
        /// </summary>
        [Required]
        public long IdCliente { get; set; }
    }
}