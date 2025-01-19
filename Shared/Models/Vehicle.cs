using CarRentalManagement.Shared.Models.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalManagement.Shared.Models
{
    public class Vehicle : BaseEntity
    {
        public DateTime Year { get; set; }
        public int ModelId { get; set; }
        public virtual Model Model { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        public string Vin { get; set; }
        public string LicencePlateNumber { get; set; }
        public double RentalRate { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
