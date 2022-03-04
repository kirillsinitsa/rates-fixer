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
    public class PartOperationService : BaseService<EFUnitOfWork>, IPartOperationService
    {
        public PartOperationService(string connectionString) : base(connectionString)
        { }

        public void Create(PartOperationVM partOperationVM)
        {
            var mapper = AutoMapperConfiguration.PartOperationVMToPartOperation.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                if (database.Parts.Get(partOperationVM.OperationId) == null)
                    throw new ValidationException("Деталь не найдена", "");
                database.PartOperations.Create(mapper.Map<PartOperation>(partOperationVM));
                database.Save();
                partOperationVM.PartOperationId = database.PartOperations.
                    Find(o => o.PartId == partOperationVM.PartId).Last().PartOperationId;
            }
        }

        public PartOperationVM Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер операции детали", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var PartOperation = database.PartOperations.Get(id.Value);
                if (PartOperation == null)
                    throw new ValidationException("Операция детали не найдена", "");
                var mapper = AutoMapperConfiguration.PartOperationToPartOperationVM.CreateMapper();
                return mapper.Map<PartOperationVM>(PartOperation);
            }
        }

        public ObservableCollection<PartOperationVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.PartOperationToPartOperationVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<PartOperationVM>>
                    (database.PartOperations.GetAll());
            }
        }

        public void Update(PartOperationVM partOperationVM)
        {
            var mapper = AutoMapperConfiguration.PartOperationVMToPartOperation.CreateMapper();
            var PartOperation = mapper.Map<PartOperation>(partOperationVM);
            using (database = new EFUnitOfWork(connectionString))
            {
                database.PartOperations.Update(PartOperation);
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер операции детали", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.PartOperations.Delete(id.Value);
                database.Save();
            }
        }

        public ObservableCollection<PartOperationVM> GetWhere(int? partId)
        {
            if (partId == null) throw new ValidationException("Не указан номер детали", "");
            var mapper = AutoMapperConfiguration.PartOperationToPartOperationVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<PartOperationVM>>
                    (database.PartOperations.Find(o => o.PartId == partId.Value));
            }
        }

        public List<int> FindParts(int? divisionId)
        {
            if (divisionId == null) throw new ValidationException("Не указан номер участка", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                return database.PartOperations.Find(o => o.DivisionId == divisionId.Value).Select(p => p.PartId).ToList();
            }
        }

        public ObservableCollection<PartOperationVM> Find(IEnumerable<int> idRange)
        {
            var mapper = AutoMapperConfiguration.PartOperationToPartOperationVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<PartOperationVM>>
                    (database.PartOperations.Find(o => idRange.Contains(o.PartOperationId)));
            }
        }

        public ObservableCollection<PartOperationVM> FindWhere(IEnumerable<int> partsIdRange)
        {
            var mapper = AutoMapperConfiguration.PartOperationToPartOperationVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<PartOperationVM>>
                    (database.PartOperations.Find(o => partsIdRange.Contains(o.PartId)));
            }
        }
    }
}
