using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int TaxIdentNumber { get; set; }

        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }



        //NavigationProperties
        
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        

    }
}
