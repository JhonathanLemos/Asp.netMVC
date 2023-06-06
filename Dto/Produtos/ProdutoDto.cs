using Teste_MVC.Models.Clientes;

namespace Teste_MVC.Dto.Produtos
{
    public class ProdutoDto
    {
        public string NomeProduto { get; set; }

        public string CodigoProduto { get; set; }

        public int Quantidade { get; set; }

        public long ClienteId { get; set; }

        public string NomeCliente { get; set; }
    }
}
