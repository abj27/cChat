using System;

namespace cChat.Data.Services
{
    public interface ITransactionService
    {
        public T InTransaction<T>(Func<IApplicationDbContext, T> action);
    }
}