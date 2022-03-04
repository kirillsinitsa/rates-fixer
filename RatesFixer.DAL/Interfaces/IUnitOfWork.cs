using System;
using RatesFixer.DAL.Entities;

namespace RatesFixer.DAL.Interfaces
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IRepository<FactoryFloor> FactoryFloors { get; }
        IRepository<Division> Divisions { get; }
        IRepository<WorkStation> WorkStations { get; }
        IRepository<Employee> Employees { get; }
        IRepository<Team> Teams { get; }
        IRepository<Operation> Operations { get; }
        IRepository<TariffPayGroup> TariffPayGroups { get; }
        IRepository<TariffMultiplier> TariffMultipliers { get; }
        IRepository<TariffPay> TariffPays { get; }
        IRepository<Part> Parts { get; }
        IRepository<PartOperation> PartOperations { get; }       
        IRepository<WageRate> WageRates { get; }
        IRepository<Order> Orders { get; }
        IRepository<OrderEntry> OrderEntries { get; }
    }
}
