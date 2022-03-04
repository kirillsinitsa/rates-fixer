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
                db.FactoryFloors.Add(new FactoryFloor { FactoryFloorId = 1, Name = "МЦ-1"});
                db.Divisions.Add(new Division
                {
                    FactoryFloorId = 1,
                    Number = 1,
                    Name = "Токарный"
                });
                db.Divisions.Add(new Division
                {
                    FactoryFloorId = 1,
                    Number = 2,
                    Name = "Фрезерный"
                });
                db.WorkStations.Add(new WorkStation { Number = 1, Name = "Токарный станок", DivisionId = 1 });
                db.WorkStations.Add(new WorkStation { Number = 2, Name = "Фрезерный станок", DivisionId = 2 });
                db.Employees.Add(new Employee
                {
                    EmployeeId = 1,
                    LastName = "Пупкин",
                    FirstName = "Василий",
                    Patronymic = "Иванович",
                    JobClass = 3,
                    OccupationCode = 12345,
                    WorkStationId = 1
                });
                db.Employees.Add(new Employee
                {
                    EmployeeId =  2,
                    LastName = "Кузнецов",
                    FirstName = "Петр",
                    Patronymic = "Петрович",
                    JobClass = 4,
                    OccupationCode = 45677,
                    WorkStationId = 2
                });
                db.Operations.Add(new Operation
                {
                    OperationCode = "1230",
                    Name = "Токарная",
                    JobClass = 3,
                    TariffPayGroupId = 2
                });
                db.Operations.Add(new Operation
                {
                    OperationCode = "3435",
                    Name = "Фрезерная",
                    JobClass = 4,
                    TariffPayGroupId = 2
                });
                db.Parts.Add(new Part { Number = "В-1", Name = "Вал" });
                db.Parts.Add(new Part { Number = "В-2", Name = "Вал-шестерня" });
                db.PartOperations.Add(new PartOperation
                {
                    FactoryFloorId = 1,
                    DivisionId = 1,
                    Number = "005",
                    OperationId = 1,
                    PartId = 1,
                    Percentage = 0.7,
                    RateTime = 0.9
                });
                db.PartOperations.Add(new PartOperation
                {
                    FactoryFloorId = 1,
                    DivisionId = 2,
                    Number = "010",
                    OperationId = 2,
                    PartId = 1,
                    Percentage = 0.8,
                    RateTime = 1.5
                });
                db.PartOperations.Add(new PartOperation
                {
                    FactoryFloorId = 1,
                    DivisionId = 1,
                    Number = "005",
                    OperationId = 1,
                    PartId = 2,
                    Percentage = 0.7,
                    RateTime = 0.9
                });
                db.PartOperations.Add(new PartOperation
                {
                    FactoryFloorId = 1,
                    DivisionId = 2,
                    Number = "010",
                    OperationId = 2,
                    PartId = 2,
                    Percentage = 0.8,
                    RateTime = 1.5
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
