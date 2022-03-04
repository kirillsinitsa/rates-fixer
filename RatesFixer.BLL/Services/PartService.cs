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
    public class PartService : BaseService<EFUnitOfWork>, IPartService
    {
        public PartService(string connectionString) : base(connectionString)
        { }

        public void Create(PartVM partVM)
        {
            var mapper = AutoMapperConfiguration.PartVMToPart.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Parts.Create(mapper.Map<Part>(partVM));
                database.Save();
                partVM.PartId = database.Parts.Find(o => o.Number == partVM.Number).Last().PartId;
            }
        }

        public PartVM Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер детали", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var Part = database.Parts.Get(id.Value);
                if (Part == null)
                    throw new ValidationException("Деталь не найдена", "");
                var mapper = AutoMapperConfiguration.PartToPartVM.CreateMapper();
                return mapper.Map<PartVM>(Part);
            }
        }

        public ObservableCollection<PartVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.PartToPartVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<PartVM>>(database.Parts.GetAll());
            }
        }

        public void Update(PartVM partVM)
        {
            var mapper = AutoMapperConfiguration.PartVMToPart.CreateMapper();
            var Part = mapper.Map<Part>(partVM);
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Parts.Update(Part);
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер детали", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Parts.Delete(id.Value);
                database.Save();
            }
        }

        public ObservableCollection<PartVM> Find(IEnumerable<int> idRange)
        {
            var mapper = AutoMapperConfiguration.PartToPartVM.CreateMapper();

            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<PartVM>>
                    (database.Parts.Find(o => idRange.Contains(o.PartId)));
            }
        }
    }
}
