using Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Core.Repositories
{
    public interface ILoginModelRepository : IGenericRepository<LoginModel>
    {
        LoginModel AuthenticateUser(string username, string password);

        LoginModel AuthenticateWithUserName(string username);
    }
}
