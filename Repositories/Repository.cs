using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teste_MVC.Data;

namespace Teste_MVC.Repositories
{
    public class Repository<T> : Controller, IRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<T> _validator;

        public Repository(ApplicationDbContext context, IValidator<T> validator)
        {
            _context = context;
            _validator = validator;
        }
        public (bool, string) Add(T entity)
        {
            var result = _validator.Validate(entity);

            if (!result.IsValid)
            {
                return (false, result?.Errors?.FirstOrDefault()?.ErrorMessage);
            }

            _context.Add(entity);
            _context.SaveChanges();
            return (true, "");
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public (bool, string) Update(T entity)
        {
            var result = _validator.Validate(entity);

            if (!result.IsValid)
            {
                return (false, result?.Errors?.FirstOrDefault()?.ErrorMessage);
            }

            _context.Update(entity);
            _context.SaveChanges();
            return (true, "");
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsQueryable();
        }

        public T Find(long id)
        {
            var result = _context.Set<T>().Where(x => x.Id == id).FirstOrDefault();
            return result == null ? throw new Exception("Não foi possível fazer essa operação") : result;
        }
    }


}
