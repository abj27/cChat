using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cChat.Data.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public TransactionService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<T> InTransaction<T>(Func<Task<T>> action, Func<Task> onError)
        {
            using (var transaction = _applicationDbContext.Instance.Database.BeginTransaction())
            {
                try
                {
                    var result = await action();
                    _applicationDbContext.Instance.SaveChanges();
                    transaction.Commit();
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    transaction.Rollback();
                    if (onError != null)
                    {
                        await onError();
                    }
                    return default(T);

                }
            }
        }
    }
}
