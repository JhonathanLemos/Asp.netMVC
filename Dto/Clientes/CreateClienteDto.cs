using Teste_MVC.Models;

namespace Teste_MVC.Dto.Clientes
{
    public class CreateClienteDto : Entity
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Telefone { get; set; }
        public string Cnpj { get; set; }
    }
}
