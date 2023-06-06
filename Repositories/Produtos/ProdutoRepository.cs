using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Teste_MVC.Dto.Produtos;
using Teste_MVC.Models.Clientes;
using Teste_MVC.Models.Produtos;
using Teste_MVC.Data;
using FluentValidation;

namespace Teste_MVC.Repositories.Produtos
{
    public class ProdutoRepository : Repository<Produto>
    {
        private readonly IRepository<Cliente> _clienteRepository;
        public ProdutoRepository(ApplicationDbContext context, IValidator<Produto> validator, IRepository<Cliente> clienteRepository) : base(context, validator)
        {
            _clienteRepository = clienteRepository;
        }

        public SelectList ClientesIdList(long? id)
        {
            var list = _clienteRepository.GetAll().ToList();
            return new SelectList(list, "Id", "Nome", id != null ? id : -1);


        }
    }
}
