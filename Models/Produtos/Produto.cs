using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Teste_MVC.Models.Clientes;

namespace Teste_MVC.Models.Produtos;

public class Produto : IEntity
{
    [Required(ErrorMessage = "Informe o nome do produto")]
    [DataType(DataType.Text)]
    public string NomeProduto { get; set; }

    [Required(ErrorMessage = "Informe o Codigo do produto")]
    [DataType(DataType.Text)]
    public string CodigoProduto { get; set; }

    [Required(ErrorMessage = "Informe a quantidade de produtos")]
    [DataType(DataType.Currency)]
    public int Quantidade { get; set; }

    public Cliente Cliente { get; set; }

    public long ClienteId { get; set; }

}
