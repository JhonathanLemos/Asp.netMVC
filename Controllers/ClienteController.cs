using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teste_MVC.Dto.Clientes;
using Teste_MVC.Validation;
using Teste_MVC.Models.Clientes;

namespace Teste_MVC.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IRepository<Cliente> _repository;

        public ClienteController(IRepository<Cliente> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var clientes = _repository.GetAll().ToList();
            return View(clientes);
        }

        public IActionResult Details(long id)
        {
            if (!ModelState.IsValid)
                return View();

            var entity = _repository.Find(id);

            return entity != null ? View(entity) : View();
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateClienteDto clienteDto)
        {
            var cliente = _mapper.Map<Cliente>(clienteDto);
            if (ModelState.IsValid)
            {
                var result = _repository.Add(cliente);

                if (!result.Item1)
                {
                    ModelState.AddModelError("Erros", result.Item2);
                    return View(cliente);
                }

                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public IActionResult Edit(long id)
        {

            if (ModelState.IsValid)
            {
                var cliente = _repository.Find(id);

                if (cliente == null)
                {
                    ModelState.AddModelError("Erros", "Nenhum cliente foi encontrado!");
                    return View();

                }
                return View(cliente);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id,Nome,Idade,Telefone,Cnpj")] UpdateClienteDto clienteDto)
        {
            if (ModelState.IsValid)
            {
                var cliente = _repository.Find(id);
                var result = _repository.Update(cliente);

                if (!result.Item1)
                {
                    ModelState.AddModelError("Erros", result.Item2);
                    return View(cliente);
                }

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(long id)
        {
            var entity = _repository.Find(id);

            if (entity is null)
            {
                return View();
            }

            return View(entity);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(IEntity entity)
        {

            if (entity.Id == null)
            {
                ModelState.AddModelError("Erros", "Cliente não encontrado!");
                return View();
            }

            var cliente = _repository.Find(entity.Id);
            _repository.Delete(cliente);
            return RedirectToAction("Index");
        }
    }
}
