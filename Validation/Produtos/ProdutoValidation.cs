using FluentValidation;
using Teste_MVC.Models.Produtos;

namespace Teste_MVC.Validation.Produtos
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(u => u.Quantidade).NotEmpty().WithMessage("A quantidade é obrigatória.");
            RuleFor(u => u.NomeProduto).NotEmpty().WithMessage("O nome do produto é obrigatório.");
            RuleFor(u => u.CodigoProduto).NotEmpty().WithMessage("O codigo do produto é obrigatório.");
        }

    }
}
