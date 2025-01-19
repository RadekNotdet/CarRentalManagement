using CarRentalManagement.Shared.Models.Abstraction;

namespace CarRentalManagement.Shared.Models
{
    public class Booking : BaseEntity
    {
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public DateTime DateOut { get; set; }
        public DateTime DateIn { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}