using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Models;
using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Mappers;

namespace RatesFixer.BLL.Services
{
    public class EmployeeService : BaseService<EFUnitOfWork>, IEmployeeService
    {
        public EmployeeService(string connectionString) : base(connectionString)
        { }

        public void Create(EmployeeVM employeeVM)
        {
            var mapper = AutoMapperConfiguration.EmployeeVMToEmployee.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Employees.Create(mapper.Map<Employee>(employeeVM));
                database.Save();
                employeeVM.EmployeeId = database.Employees.Find(o => (o.FirstName == employeeVM.FirstName)
                    && (o.LastName == employeeVM.LastName) && (o.Patronymic == employeeVM.Patronymic)).Last().EmployeeId;
            }
        }

        public EmployeeVM Get(int? id)
        {
            if (id == null)
                throw new ValidationException("Не указан табельный номер", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var employee = database.Employees.Get(id.Value);
                if (employee == null) throw new ValidationException("Работник не найден", "");
                var mapper = AutoMapperConfiguration.EmployeeToEmployeeVM.CreateMapper();
                return mapper.Map<EmployeeVM>(employee);
            }
        }

        public ObservableCollection<EmployeeVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.EmployeeToEmployeeVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<EmployeeVM>>(database.Employees.GetAll());
            }
        }

        public void Update(EmployeeVM employeeVM)
        {
            var mapper = AutoMapperConfiguration.EmployeeVMToEmployee.CreateMapper();
            var employee = mapper.Map<Employee>(employeeVM);
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Employees.Update(employee);
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан табельный номер", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Employees.Delete(id.Value);
                database.Save();
            }
        }

        public ObservableCollection<EmployeeVM> GetWhere(int? workStationId)
        {
            if (workStationId == null) throw new ValidationException("Не указан номер рабочего места", "");
            var mapper = AutoMapperConfiguration.EmployeeToEmployeeVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<EmployeeVM>>
                    (database.Employees.Find(o => o.WorkStationId == workStationId.Value));
            }
        }
        public ObservableCollection<EmployeeVM> FindWhere(IEnumerable<int> workStationsIdRange)
        {
            var mapper = AutoMapperConfiguration.EmployeeToEmployeeVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<EmployeeVM>>
                    (database.Employees.Find(o => workStationsIdRange.Contains(o.WorkStationId)));
            }
        }
        public ObservableCollection<EmployeeVM> Find(IEnumerable<int> idRange)
        {
            var mapper = AutoMapperConfiguration.EmployeeToEmployeeVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<EmployeeVM>>
                    (database.Employees.Find(o => idRange.Contains(o.EmployeeId)));
            }
        }
    }
}
