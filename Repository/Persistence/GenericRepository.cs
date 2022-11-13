using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public AppDbContext _context;
        public DbSet<T> _model;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _model = _context.Set<T>();
        }
        public async Task DeleteAsync(int id)
        {
            var existingModel = await _model.FindAsync(id);
            _context.Entry(existingModel).State = EntityState.Deleted; 
            await SaveAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _model.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _model.ToListAsync();
        }

        public async Task InsertAsync(T obj)
        {
            await _model.AddAsync(obj);
            await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task UpdateAsync(T obj)
        {
            _model.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            await SaveAsync();

        }
    }
}
