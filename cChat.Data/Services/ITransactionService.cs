using System;
using System.Threading.Tasks;

namespace cChat.Data.Services
{
    public interface ITransactionService
    {
        public Task<T> InTransaction<T>(Func<Task<T>> action, Func<Task> onError =null);
    }
}