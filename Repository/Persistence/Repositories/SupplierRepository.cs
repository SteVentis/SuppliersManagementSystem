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
            return await _model.ToListAsync();
        }
    }
}
