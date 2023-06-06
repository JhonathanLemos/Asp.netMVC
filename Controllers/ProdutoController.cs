using Microsoft.AspNetCore.Mvc;
using Teste_MVC.Repositories.Produtos;
using Teste_MVC.Dto.Produtos;
using Teste_MVC.Validation;
using Teste_MVC.Models.Produtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Protocol.Core.Types;

namespace Teste_MVC.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoController(ProdutoRepository produtoRepository, IMapper mapper)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        // GET: Produto
        public IActionResult Index()
        {
            var produtos = _produtoRepository.GetAll().Include(x => x.Cliente).OrderBy(x => x.NomeProduto).ToList();
            return View(produtos);
        }

        public IActionResult Details(long? id)
        {
            if (id == null || _produtoRepository.GetAll() == null)
            {
                return NotFound();
            }

            var produto = _produtoRepository.GetAll().Include(x => x.Cliente).Where(x => x.Id == id).FirstOrDefault();
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        public IActionResult Create()
        {
            ViewBag.ClienteId = _produtoRepository.ClientesIdList(null);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,NomeProduto,CodigoProduto,Quantidade,ClienteId")] ProdutoDto produtoDto)
        {
            if (ModelState.IsValid)
            {
                var produto = _mapper.Map<Produto>(produtoDto);
                var result = _produtoRepository.Add(produto);

                if (!result.Item1)
                {
                    ModelState.AddModelError("Erros", result.Item2);
                    return View(produto);
                }

                ViewBag.ClienteId = _produtoRepository.ClientesIdList(produto.ClienteId);
                return RedirectToAction(nameof(Index));
            }

            //ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", produto.ClienteId);
            return View(produtoDto);
        }

        public IActionResult Edit(long? id)
        {
            if (id == null || _produtoRepository.GetAll() == null)
            {
                return NotFound();
            }

            var produto = _produtoRepository.Find((long)id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewBag.Items = _produtoRepository.ClientesIdList(produto.ClienteId);
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id,NomeProduto,CodigoProduto,Quantidade,ClienteId")] ProdutoDto produtoDto)
        {


            if (ModelState.IsValid)
            {
                var produto = _mapper.Map<Produto>(produtoDto);
                var result = _produtoRepository.Update(produto);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(long? id)
        {
            var produto = _produtoRepository.Find((long)id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpPost]
        public IActionResult Delete(IEntity entity)
        {
            if (entity.Id == null)
            {
                ModelState.AddModelError("Erros", "Produto não encontrado!");
                return View();
            }

            var produto = _produtoRepository.Find(entity.Id);
            _produtoRepository.Delete(produto);
            return RedirectToAction("Index");
        }
    }
}
