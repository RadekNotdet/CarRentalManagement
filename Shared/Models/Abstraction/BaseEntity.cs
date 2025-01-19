using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalManagement.Shared.Models.Abstraction
{
     public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        
    }
}
