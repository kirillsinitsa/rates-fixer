using System.Collections.ObjectModel;
using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Infrastructure;
using RatesFixer.BLL.Models;
using RatesFixer.DAL.Entities;
using RatesFixer.DAL.Repositories;
using RatesFixer.DAL.Enums;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using RatesFixer.BLL.Mappers;

namespace RatesFixer.BLL.Services
{
    public class TariffPayService : BaseService<EFUnitOfWork>, ITariffPayService
    {
        public TariffPayService(string connectionString) : base(connectionString)
        { }

        public void Create(TariffPayVM tariffPayVM)
        {
            var mapper = AutoMapperConfiguration.TariffPayVMToTariffPay.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                database.TariffPays.Create(mapper.Map<TariffPay>(tariffPayVM));
                database.Save();
            }
        }

        public TariffPayVM Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер тарифной ставки", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var tariffPay = database.TariffPays.Get(id.Value);
                if (tariffPay == null) throw new ValidationException("Тарифная ставка не найдена", "");
                var mapper = AutoMapperConfiguration.TariffPayToTariffPayVM.CreateMapper();
                return mapper.Map<TariffPayVM>(tariffPay);
            }
        }

        public ObservableCollection<TariffPayVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.TariffPayToTariffPayVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<TariffPayVM>>
                    (database.TariffPays.GetAll());
            }
        }

        public void Update(TariffPayVM tariffPayVM)
        {
            var mapper = AutoMapperConfiguration.TariffPayVMToTariffPay.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                database.TariffPays.Update(mapper.Map<TariffPay>(tariffPayVM));
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер тарифной ставки", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.TariffPays.Delete(id.Value);
                database.Save();
            }
        }
        public ObservableCollection<TariffPayVM> Find(int jobClass, int tariffPayGroupId, TariffPayType tariffPayType, int tariffPayCode)
        {
            var mapper = AutoMapperConfiguration.TariffPayToTariffPayVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<TariffPayVM>>(database.TariffPays.Find(o => o.JobClass == jobClass &&
                                                                                                   o.TariffPayGroupId == tariffPayGroupId &&
                                                                                                   o.TariffPayType == tariffPayType &&
                                                                                                   o.TariffPayCode == tariffPayCode));
            }
        }

        public void UpdateCollection(ICollection<TariffPayVM> tariffPayVMs)
        {
            var mapper = AutoMapperConfiguration.TariffPayVMToTariffPay.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                foreach (TariffPayVM tariffPayVM in tariffPayVMs)
                {
                    database.TariffPays.Update(mapper.Map<TariffPay>(tariffPayVM));
                }
                database.Save();
            }
        }
    }
}
