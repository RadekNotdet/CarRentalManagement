using CarRentalManagement.Server.Data;
using CarRentalManagement.Server.Interfaces.IRepository;
using CarRentalManagement.Shared.Models;
using CarRentalManagement.Shared.Models.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Brand> _brands;
        private IGenericRepository<Model> _models;
        private IGenericRepository<Customer> _customers;
        private IGenericRepository<Color> _colors;
        private IGenericRepository<Booking> _bookings;
        private IGenericRepository<Vehicle> _vehicles;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Brand> BrandsRepository => _brands ??= new GenericRepository<Brand>(_context);
        public IGenericRepository<Model> ModelsRepository => _models ??= new GenericRepository<Model>(_context);
        public IGenericRepository<Color> ColorsRepository => _colors ??= new GenericRepository<Color>(_context);
        public IGenericRepository<Booking> BookingsRepository => _bookings ??= new GenericRepository<Booking>(_context);
        public IGenericRepository<Customer> CustomersRepository => _customers ??= new GenericRepository<Customer>(_context);
        public IGenericRepository<Vehicle> VehiclesRepository => _vehicles ??= new GenericRepository<Vehicle>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                q.State == EntityState.Added);
            foreach (var entry in entries)
            {

                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).DateCreated = DateTime.UtcNow;
                    ((BaseEntity)entry.Entity).CreatedBy = "Radek";
                }
                else
                {
                    ((BaseEntity)entry.Entity).DateUpdated = DateTime.UtcNow;
                    ((BaseEntity)entry.Entity).UpdatedBy = "Radek";
                }
                    



            }
            await _context.SaveChangesAsync();
        }
    }
}
