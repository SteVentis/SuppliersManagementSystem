using DataAccess.Context;
using Models.IdentityModels;
using Repository.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Persistence.Repositories
{
    public class LoginModelRepository : GenericRepository<LoginModel>, ILoginModelRepository
    {
        public LoginModelRepository(AppDbContext context) : base(context)
        {
        }

        public LoginModel AuthenticateUser(string username, string password)
        {
            
            return _model.FirstOrDefault(u =>
                            (u.UserName == username && u.Password == password));
        }

        public LoginModel AuthenticateWithUserName(string username)
        {
            return _model.SingleOrDefault(u => u.UserName == username);
        }
    }
}
