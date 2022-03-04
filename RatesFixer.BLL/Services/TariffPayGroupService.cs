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
    public class TariffPayGroupService : BaseService<EFUnitOfWork>, ITariffPayGroupService
    {
        public TariffPayGroupService(string connectionString) : base(connectionString)
        { }

        public void Create(TariffPayGroupVM tariffPayGroupVM)
        {
            var mapper = AutoMapperConfiguration.TariffPayGroupVMToTariffPayGroup.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                database.TariffPayGroups.Create(mapper.Map<TariffPayGroup>(tariffPayGroupVM));
                database.Save();
            }
        }

        public TariffPayGroupVM Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер группы ставок", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var tariffPayGroup = database.TariffPayGroups.Get(id.Value);
                if (tariffPayGroup == null)
                    throw new ValidationException("Группа ставок не найдена", "");
                var mapper = AutoMapperConfiguration.TariffPayGroupToTariffPayGroupVM.CreateMapper();
                return mapper.Map<TariffPayGroupVM>(tariffPayGroup);
            }
        }

        public ObservableCollection<TariffPayGroupVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.TariffPayGroupToTariffPayGroupVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<TariffPayGroupVM>>
                    (database.TariffPayGroups.GetAll());
            }
        }

        public void Update(TariffPayGroupVM tariffPayGroupVM)
        {
            var mapper = AutoMapperConfiguration.TariffPayGroupVMToTariffPayGroup.CreateMapper();
            var TariffPayGroup = mapper.Map<TariffPayGroup>(tariffPayGroupVM);
            using (database = new EFUnitOfWork(connectionString))
            {
                database.TariffPayGroups.Update(TariffPayGroup);
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер группы ставок", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.TariffPayGroups.Delete(id.Value);
                database.Save();
            }
        }
    }
}
