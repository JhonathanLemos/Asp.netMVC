using System.ComponentModel.DataAnnotations;
using Teste_MVC.Models.Produtos;

namespace Teste_MVC.Models.Clientes
{
    public class Cliente : IEntity
    {

        [Required(ErrorMessage = "Informe o nome")]
        [DataType(DataType.Text)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a idade")]
        [DataType(DataType.Currency)]
        public int Idade { get; set; }


        [Required(ErrorMessage = "Informe o telefone")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe o cnpj")]
        [DataType(DataType.Currency)]
        public string Cnpj { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}
