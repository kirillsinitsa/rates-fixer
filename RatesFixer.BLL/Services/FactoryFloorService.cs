using System.Collections.ObjectModel;
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
    public class FactoryFloorService : BaseService<EFUnitOfWork>, IFactoryFloorService
    {
        public FactoryFloorService(string connectionString) : base(connectionString)
        { }

        public void Create(FactoryFloorVM factoryFloorVM)
        {
            var mapper = AutoMapperConfiguration.FactoryFloorVMToFactoryFloor.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                database.FactoryFloors.Create(mapper.Map<FactoryFloor>(factoryFloorVM));
                database.Save();
            }
        }

        public FactoryFloorVM Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер цеха", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var factoryFloor = database.FactoryFloors.Get(id.Value);
                if (factoryFloor == null)
                    throw new ValidationException("Цех не найден", "");
                var mapper = AutoMapperConfiguration.FactoryFloorToFactoryFloorVM.CreateMapper();
                return mapper.Map<FactoryFloorVM>(factoryFloor);
            }
        }

        public ObservableCollection<FactoryFloorVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.FactoryFloorToFactoryFloorVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<FactoryFloorVM>>
                    (database.FactoryFloors.GetAll());
            }
        }

        public void Update(FactoryFloorVM factoryFloorVM)
        {
            var mapper = AutoMapperConfiguration.FactoryFloorVMToFactoryFloor.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                database.FactoryFloors.Update(mapper.Map<FactoryFloor>(factoryFloorVM));
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер цеха", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.FactoryFloors.Delete(id.Value);

                database.Save();
            }
        }

        public ObservableCollection<FactoryFloorVM> Find(IEnumerable<int> idRange)
        {
            var mapper = AutoMapperConfiguration.FactoryFloorToFactoryFloorVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<FactoryFloorVM>>
                    (database.FactoryFloors.Find(o => idRange.Contains(o.FactoryFloorId)));
            }
        }
    }
}
