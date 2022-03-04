using System.Data.Entity;
using RatesFixer.DAL.Entities;

namespace RatesFixer.DAL.EF
{
    public class RatesContext : DbContext
    {       
        public DbSet<FactoryFloor> FactoryFloors { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<WorkStation> WorkStations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartOperation> PartOperations { get; set; }
        public DbSet<TariffMultiplier> TariffMultipliers { get; set; }
        public DbSet<TariffPayGroup> TariffPayGroups { get; set; }
        public DbSet<TariffPay> TariffPays { get; set; }               
        public DbSet<WageRate> WageRates { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderEntry> OrderEntries { get; set; }

        public RatesContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<RatesContext>(new RatesDbInitializer());
        }

        public class RatesDbInitializer : DropCreateDatabaseIfModelChanges<RatesContext>
        {
            protected override void Seed(RatesContext db)
            {
                var factoryFloor = new FactoryFloor { FactoryFloorId = 1, Name = "МЦ-1" };
                db.FactoryFloors.Add(factoryFloor);
                var division1 = new Division
                {
                    FactoryFloorId = 1,
                    Number = 1,
                    Name = "Токарный",
                };
                db.Divisions.Add(division1);
                var division2 = new Division
                {
                    FactoryFloorId = 1,
                    Number = 2,
                    Name = "Фрезерный",
                };
                db.Divisions.Add(division2);
                var workStation1 = new WorkStation { Number = 1, Name = "Токарный станок", DivisionId = 1 };
                db.WorkStations.Add(workStation1);
                var workStation2 = new WorkStation { Number = 2, Name = "Фрезерный станок", DivisionId = 2 };
                db.WorkStations.Add(workStation2);
                var employee1 = new Employee
                {
                    EmployeeId = 1,
                    LastName = "Пупкин",
                    FirstName = "Василий",
                    Patronymic = "Иванович",
                    JobClass = 3,
                    OccupationCode = 12345,
                    WorkStationId = 1,
                };
                db.Employees.Add(employee1);
                var employee2 = new Employee
                {
                    EmployeeId = 2,
                    LastName = "Кузнецов",
                    FirstName = "Петр",
                    Patronymic = "Петрович",
                    JobClass = 4,
                    OccupationCode = 45677,
                    WorkStationId = 2,
                };
                db.Employees.Add(employee2);
                var operation1 = new Operation
                {
                    OperationCode = "1230",
                    Name = "Токарная",
                    JobClass = 3,
                    TariffPayGroupId = 2
                };
                db.Operations.Add(operation1);
                var operation2 = new Operation
                {
                    OperationCode = "3435",
                    Name = "Фрезерная",
                    JobClass = 4,
                    TariffPayGroupId = 2
                };
                db.Operations.Add(operation2);
                var part1 = new Part { Number = "В-1", Name = "Вал" };
                db.Parts.Add(part1);
                var part2 = new Part { Number = "В-2", Name = "Вал-шестерня" };
                db.Parts.Add(part2);
                db.PartOperations.Add(new PartOperation
                {
                    FactoryFloorId = 1,
                    DivisionId = 1,
                    Number = "005",
                    OperationId = 1,
                    PartId = 1,
                    Percentage = 0.7,
                    RateTime = 0.9,
                    Part = part1
                });
                db.PartOperations.Add(new PartOperation
                {
                    FactoryFloorId = 1,
                    DivisionId = 2,
                    Number = "010",
                    OperationId = 2,
                    PartId = 1,
                    Percentage = 0.8,
                    RateTime = 1.5,
                    Part = part1
                });
                db.PartOperations.Add(new PartOperation
                {
                    FactoryFloorId = 1,
                    DivisionId = 1,
                    Number = "005",
                    OperationId = 1,
                    PartId = 2,
                    Percentage = 0.7,
                    RateTime = 0.9,
                    Part = part2
                });
                db.PartOperations.Add(new PartOperation
                {
                    FactoryFloorId = 1,
                    DivisionId = 2,
                    Number = "010",
                    OperationId = 2,
                    PartId = 2,
                    Percentage = 0.8,
                    RateTime = 1.5,
                    Part = part2
                });
                db.TariffPayGroups.Add(new TariffPayGroup { TariffPayGroupId = 1, Multiplier = 1.0f });
                db.TariffPayGroups.Add(new TariffPayGroup { TariffPayGroupId = 2, Multiplier = 1.1f });
                db.TariffPayGroups.Add(new TariffPayGroup { TariffPayGroupId = 3, Multiplier = 1.2f });
                db.TariffMultipliers.Add(new TariffMultiplier { JobClass = 1, Multiplier = 1.00f });
                db.TariffMultipliers.Add(new TariffMultiplier { JobClass = 2, Multiplier = 1.16f });
                db.TariffMultipliers.Add(new TariffMultiplier { JobClass = 3, Multiplier = 1.35f });
                db.TariffMultipliers.Add(new TariffMultiplier { JobClass = 4, Multiplier = 1.57f });
                db.TariffMultipliers.Add(new TariffMultiplier { JobClass = 5, Multiplier = 1.73f });
                db.TariffMultipliers.Add(new TariffMultiplier { JobClass = 6, Multiplier = 1.90f });
                db.SaveChanges();
            }
        }
    }
}
