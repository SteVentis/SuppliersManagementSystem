using DataAccess.Context;
using Repository.Core;
using Repository.Core.Repositories;
using Repository.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ISupplierRepository Suppliers { get; private set; }

        public ICountryRepository Countries { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Suppliers = new SupplierRepository(context);
            Countries = new CountryRepository(context);
            Categories = new CategoryRepository(context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
