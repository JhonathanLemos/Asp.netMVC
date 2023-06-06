using FluentValidation;
using Teste_MVC.Dto.Clientes;
using Teste_MVC.Models.Clientes;

namespace Teste_MVC.Validation.Clientes
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {

        public ClienteValidation()
        {
            RuleFor(u => u.Nome).NotEmpty().WithMessage("O nome é obrigatório.");
            RuleFor(u => u.Telefone).NotEmpty().WithMessage("O telefone é obrigatório.");
            RuleFor(u => u.Telefone).MinimumLength(15);
            //RuleFor(u => u.Idade).NotEmpty().WithMessage("A idade é obrigatória.");
            //RuleFor(u => u.Idade).Null();
            RuleFor(u => u.Cnpj).NotEmpty().WithMessage("o cnpj é obrigatória.");
            RuleFor(u => u.Cnpj).Length(14);


        }
    }
}
