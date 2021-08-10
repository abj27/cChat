using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cChat.Data.Entities;

namespace cChat.Data.Repositories
{
    public interface IRepository<T, TT> where T : IEntity<TT>
    {
        IQueryable<T> GetAll();
        T GetById(TT id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(TT id);
    }
}
