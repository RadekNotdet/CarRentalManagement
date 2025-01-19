using CarRentalManagement.Shared.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalManagement.Shared.Models
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
    }
}
