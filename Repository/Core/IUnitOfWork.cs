using Repository.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ISupplierRepository Suppliers { get; }
        ICountryRepository Countries { get; }
        ICategoryRepository Categories { get; }
        ILoginModelRepository LoginModels { get; }
    }
}
