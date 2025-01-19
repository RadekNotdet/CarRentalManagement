using CarRentalManagement.Shared.Models;

namespace CarRentalManagement.Server.Interfaces.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveAsync();
        IGenericRepository<Brand> BrandsRepository { get; }
        IGenericRepository<Model> ModelsRepository { get; }
        IGenericRepository<Vehicle> VehiclesRepository { get; }
        IGenericRepository<Customer> CustomersRepository { get;}
        IGenericRepository<Color> ColorsRepository { get; }
        IGenericRepository<Booking> BookingsRepository { get; }


    }
}
