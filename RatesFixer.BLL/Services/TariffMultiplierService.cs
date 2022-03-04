using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Models;
using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Repositories;
using System.Collections.ObjectModel;
using System.Linq;
using RatesFixer.BLL.Mappers;

namespace RatesFixer.BLL.Services
{
    public class TariffMultiplierService : BaseService<EFUnitOfWork>, ITariffMultiplierService
    {
        public TariffMultiplierService(string connectionString) : base(connectionString)
        { }

        public void Create(TariffMultiplierVM tariffMultiplierVM)
        {
            var mapper = AutoMapperConfiguration.TariffMultiplierVMToTariffMultiplier.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                database.TariffMultipliers.Create(mapper.Map<TariffMultiplier>(tariffMultiplierVM));
                database.Save();
            }
        }

        public TariffMultiplierVM Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан разряд", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var tariffMultiplier = database.TariffMultipliers.Get(id.Value);
                if (tariffMultiplier == null)
                    throw new ValidationException("Тарифный коэффициент не найден", "");
                var mapper = AutoMapperConfiguration.TariffMultiplierToTariffMultiplierVM.CreateMapper();
                return mapper.Map<TariffMultiplierVM>(tariffMultiplier);
            }
        }

        public ObservableCollection<TariffMultiplierVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.TariffMultiplierToTariffMultiplierVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<TariffMultiplierVM>>
                    (database.TariffMultipliers.GetAll());
            }
        }

        public void Update(TariffMultiplierVM tariffMultiplierVM)
        {
            var mapper = AutoMapperConfiguration.TariffMultiplierVMToTariffMultiplier.CreateMapper();
            var TariffMultiplier = mapper.Map<TariffMultiplier>(tariffMultiplierVM);
            using (database = new EFUnitOfWork(connectionString))
            {
                database.TariffMultipliers.Update(TariffMultiplier);
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан разряд", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.TariffMultipliers.Delete(id.Value);
                database.Save();
            }
        }
    }
}
