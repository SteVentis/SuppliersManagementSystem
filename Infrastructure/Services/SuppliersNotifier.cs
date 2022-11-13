using Infrastructure.Interfaces;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SuppliersNotifier : ISuppliersNotifier
    {
        public Task NotifySupplierForRegistration(Supplier supplier)
        {
            throw new NotImplementedException();
        }
    }
}
