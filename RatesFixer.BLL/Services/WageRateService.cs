using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Models;
using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Repositories;
using System.Linq;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Mappers;
using System.Collections.Generic;

namespace RatesFixer.BLL.Services
{
    public class WageRateService : BaseService<EFUnitOfWork>, IWageRateService
    {
        public WageRateService(string connectionString) : base(connectionString)
        { }

        public void Create(WageRateVM wageRateVM)
        {
            var mapper = AutoMapperConfiguration.WageRateVMToWageRate.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                database.WageRates.Create(mapper.Map<WageRate>(wageRateVM));
                database.Save();
                wageRateVM.WageRateId = database.WageRates.Find(o => o.PartOperationId == wageRateVM.PartOperationId).Last().WageRateId;
            }
        }

        public WageRateVM Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер расценки", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var WageRate = database.WageRates.Get(id.Value);
                if (WageRate == null) throw new ValidationException("Расценка не найдена", "");
                var mapper = AutoMapperConfiguration.WageRateToWageRateVM.CreateMapper();
                return mapper.Map<WageRateVM>(WageRate);
            }
        }

        public ObservableCollection<WageRateVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.WageRateToWageRateVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<WageRateVM>>
                    (database.WageRates.GetAll());
            }
        }

        public void Update(WageRateVM WageRateVM)
        {
            var mapper = AutoMapperConfiguration.WageRateVMToWageRate.CreateMapper();
            var WageRate = mapper.Map<WageRate>(WageRateVM);
            using (database = new EFUnitOfWork(connectionString))
            {
                database.WageRates.Update(WageRate);
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер расценки", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.WageRates.Delete(id.Value);
                database.Save();
            }
        }

        public int GetIdWhere(int partOperationId)
        {
            using (database = new EFUnitOfWork(connectionString))
            {
                var wageRate = database.WageRates.Find(o => o.PartOperationId == partOperationId);
                return wageRate == null ? 0 : wageRate.First().WageRateId;
            }
        }

        public ObservableCollection<WageRateVM> Find(IEnumerable<int> idRange)
        {
            var mapper = AutoMapperConfiguration.WageRateToWageRateVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<WageRateVM>>
                    (database.WageRates.Find(o => idRange.Contains(o.WageRateId)));
            }
        }
    }
}
