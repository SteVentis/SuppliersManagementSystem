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
        public void Delete(int id)
        {
            var existingModel = _model.Find(id);
            _model.Remove(existingModel);
            Save();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _model.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _model.ToListAsync();
        }

        public void Insert(T obj)
        {
            _model.Add(obj);
            Save();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Update(T obj)
        {
            _model.Attach(obj);       
            Save();

        }
    }
}
