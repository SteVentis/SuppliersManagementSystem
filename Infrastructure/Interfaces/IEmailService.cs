using MimeKit;
using Models.Email;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IEmailService
    {
        void SendEmailToNewSupplier(Supplier supplier);
    }
}
