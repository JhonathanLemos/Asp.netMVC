using Microsoft.AspNetCore.Mvc;

namespace Teste_MVC.Validation
{
    public interface IRepository<T>
    {
        (bool, string) Add(T entity);
        (bool, string) Update(T entity);
        void Delete(T entity);
        public IQueryable<T> GetAll();

        public T Find(long id);
    }
}
