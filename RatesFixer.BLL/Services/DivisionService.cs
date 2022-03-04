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
    public class DivisionService : BaseService<EFUnitOfWork>, IDivisionService
    {
        public DivisionService(string connectionString) : base(connectionString)
        { }

        public void Create(DivisionVM divisionVM)
        {
            var mapper = AutoMapperConfiguration.DivisionVMToDivision.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                if (database.FactoryFloors.Get(divisionVM.FactoryFloorId) == null)
                    throw new ValidationException("Цех не найден", "FactoryFloorId");
                database.Divisions.Create(mapper.Map<Division>(divisionVM));
                database.Save();
                divisionVM.DivisionId = database.Divisions.Find(p => (p.FactoryFloorId == divisionVM.FactoryFloorId)
                    && (p.Number == divisionVM.Number)).Last().DivisionId;
            }
        }

        public DivisionVM Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер участка", "DivisionId");
            using (database = new EFUnitOfWork(connectionString))
            {
                var division = database.Divisions.Get(id.Value);
                if (division == null) throw new ValidationException("Участок не найден", "Division");
                var mapper = AutoMapperConfiguration.DivisionToDivisionVM.CreateMapper();
                return mapper.Map<DivisionVM>(division);
            }
        }

        public ObservableCollection<DivisionVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.DivisionToDivisionVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<DivisionVM>>(database.Divisions.GetAll());
            }
        }
        public void Update(DivisionVM divisionVM)
        {
            var mapper = AutoMapperConfiguration.DivisionVMToDivision.CreateMapper();
            var division = mapper.Map<Division>(divisionVM);
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Divisions.Update(division);
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер участка", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Divisions.Delete(id.Value);
                database.Save();
            }
        }

        public ObservableCollection<DivisionVM> GetWhere(int? factoryFloorId)
        {
            if (factoryFloorId == null) throw new ValidationException("Не указан номер цеха", "");
            var mapper = AutoMapperConfiguration.DivisionToDivisionVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<DivisionVM>>
                    (database.Divisions.Find(o => o.FactoryFloorId == factoryFloorId.Value));
            }
        }

        public ObservableCollection<DivisionVM> Find(IEnumerable<int> idRange)
        {
            var mapper = AutoMapperConfiguration.DivisionToDivisionVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<DivisionVM>>
                    (database.Divisions.Find(o => idRange.Contains(o.DivisionId)));
            }
        }

        public ObservableCollection<DivisionVM> FindWhere(IEnumerable<int> factoryFloorsIdRange)
        {
            var mapper = AutoMapperConfiguration.DivisionToDivisionVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<DivisionVM>>
                    (database.Divisions.Find(o => factoryFloorsIdRange.Contains(o.FactoryFloorId)));
            }
        }
    }
}
