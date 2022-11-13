using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repository.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence.Repositories
{
    public class SupplierRepository : GenericRepository<Supplier>,ISupplierRepository
    {
        public SupplierRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            var suppliers = await _model.Include(x => x.Category).Include(x => x.Country).ToListAsync();
            return suppliers;
        }

        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            var existedSupplier = await _model.Include(x => x.Category)
                                    .Include(x => x.Country)
                                    .FirstOrDefaultAsync(supplier => supplier.Id == id);

            return existedSupplier;
        }
    }
}
