using System;
using System.Linq;
using cChat.Data.Entities;

namespace cChat.Data.Repositories
{
    public class Repository<T, TT> : IRepository<T,TT> where T : class, IEntity<TT>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public Repository(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IQueryable<T> GetAll()
        {
            return _applicationDbContext.Instance.Set<T>();
        }

        public T GetById(TT id)
        {
            return _applicationDbContext.Instance.Set<T>().Find(id);
        }
        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _applicationDbContext.Instance.Add(entity);
            _applicationDbContext.Instance.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _applicationDbContext.Instance.Update(entity);
            _applicationDbContext.Instance.SaveChanges();
        }
        public void Delete(TT id)
        {
            if (id == null) throw new ArgumentNullException("entity");

            var entity = GetById(id);
            _applicationDbContext.Instance.Remove(entity);
            _applicationDbContext.Instance.SaveChanges();
        }
    }
}