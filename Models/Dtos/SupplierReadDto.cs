using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class SupplierReadDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
        
        public string CategoryName { get; set; }

        public int TaxIdentNumber { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public bool IsActive { get; set; }

    }
}
