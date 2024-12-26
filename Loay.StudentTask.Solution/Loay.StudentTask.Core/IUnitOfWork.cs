using Loay.StudentTask.Core.Models;
using Loay.StudentTask.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loay.StudentTask.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseModel;

        Task<int> CompleteAsync();
    }
}
