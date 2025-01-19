using CarRentalManagement.Shared.Models.Abstraction;

namespace CarRentalManagement.Shared.Models
{
    public class Customer : BaseEntity
    {
        public string TaxId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}