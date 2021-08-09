using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cChat.Data.Services
{
    public interface ITransactionService
    {
        T InTransaction<T>(Func<T> action);
    }

    public class TransactionService : ITransactionService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public TransactionService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public T InTransaction<T>(Func<T> action)
        {
            using (var transaction = _applicationDbContext.Instance.Database.BeginTransaction())
            {
                try
                {
                    var result = action();
                    _applicationDbContext.Instance.SaveChanges();
                    transaction.Commit();
                    return result;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                    return default(T);

                }
                finally
                {
                    _applicationDbContext.Instance.Dispose();
                }
            }
        }
    }
}
