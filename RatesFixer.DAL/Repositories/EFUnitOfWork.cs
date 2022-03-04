using System;
using System.Data.Entity.Infrastructure;
using RatesFixer.DAL.EF;
using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Interfaces;

namespace RatesFixer.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private RatesContext db;
        private FactoryFloorRepository factoryFloorRepository;
        private DivisionRepository divisionRepository;
        private WorkStationRepository workStationRepository;
        private EmployeeRepository employeeRepository;
        private TeamRepository teamRepository;
        private OperationRepository operationRepository;
        private TariffPayGroupRepository tariffPayGroupRepository;
        private TariffMultiplierRepository tariffMultiplierRepository;
        private PartRepository partRepository;
        private PartOperationRepository partOperationRepository;
        private TariffPayRepository tariffPayRepository;
        private WageRateRepository wageRateRepository;
        private OrderRepository orderRepository;
        private OrderEntryRepository orderEntryRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new RatesContext(connectionString);
        }

        public IRepository<FactoryFloor> FactoryFloors
        {
            get
            {
                if (factoryFloorRepository == null)
                    factoryFloorRepository = new FactoryFloorRepository(db);
                return factoryFloorRepository;
            }
        }

        public IRepository<Division> Divisions
        {
            get
            {
                if (divisionRepository == null)
                    divisionRepository = new DivisionRepository(db);
                return divisionRepository;
            }
        }

        public IRepository<WorkStation> WorkStations
        {
            get
            {
                if (workStationRepository == null)
                    workStationRepository = new WorkStationRepository(db);
                return workStationRepository;
            }
        }

        public IRepository<Employee> Employees
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new EmployeeRepository(db);
                return employeeRepository;
            }
        }
        public IRepository<Team> Teams
        {
            get
            {
                if (teamRepository == null)
                    teamRepository = new TeamRepository(db);
                return teamRepository;
            }
        }

        public IRepository<Operation> Operations
        {
            get
            {
                if (operationRepository == null)
                    operationRepository = new OperationRepository(db);
                return operationRepository;
            }
        }

        public IRepository<TariffMultiplier> TariffMultipliers
        {
            get
            {
                if (tariffMultiplierRepository == null)
                    tariffMultiplierRepository = new TariffMultiplierRepository(db);
                return tariffMultiplierRepository;
            }
        }

        public IRepository<TariffPayGroup> TariffPayGroups
        {
            get
            {
                if (tariffPayGroupRepository == null)
                    tariffPayGroupRepository = new TariffPayGroupRepository(db);
                return tariffPayGroupRepository;
            }
        }
        public IRepository<Part> Parts
        {
            get
            {
                if (partRepository == null)
                    partRepository = new PartRepository(db);
                return partRepository;
            }
        }
        public IRepository<PartOperation> PartOperations
        {
            get
            {
                if (partOperationRepository == null)
                    partOperationRepository = new PartOperationRepository(db);
                return partOperationRepository;
            }
        }
        public IRepository<TariffPay> TariffPays
        {
            get
            {
                if (tariffPayRepository == null)
                    tariffPayRepository = new TariffPayRepository(db);
                return tariffPayRepository;
            }
        }
        public IRepository<WageRate> WageRates
        {
            get
            {
                if (wageRateRepository == null)
                    wageRateRepository = new WageRateRepository(db);
                return wageRateRepository;
            }
        }
        public IRepository<Order> Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(db);
                return orderRepository;
            }
        }
        public IRepository<OrderEntry> OrderEntries
        {
            get
            {
                if (orderEntryRepository == null)
                    orderEntryRepository = new OrderEntryRepository(db);
                return orderEntryRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
