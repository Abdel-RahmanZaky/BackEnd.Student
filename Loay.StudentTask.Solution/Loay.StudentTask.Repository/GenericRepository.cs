using Loay.StudentTask.Core.Models;
using Loay.StudentTask.Core.Repositories.Contract;
using Loay.StudentTask.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loay.StudentTask.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(StudentSubject))
                return (IReadOnlyList<T>)await _dbContext.Set<StudentSubject>().Include(ss => ss.Student).Include(ss => ss.Subject).ToListAsync();
            return await _dbContext.Set<T>().ToListAsync();
        }
        public async Task<T?> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }
        public async Task AddAsync(T entity)
           => await _dbContext.AddAsync(entity);
        public void Update(T entity)
            => _dbContext.Update(entity);
        public void Delete(T entity)
          => _dbContext.Remove(entity);

    }
}
