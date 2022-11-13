using DataAccess.Context;
using Models.Entities;
using Repository.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository 
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {

        }
    }
}
