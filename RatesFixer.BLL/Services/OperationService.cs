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
    public class OperationService : BaseService<EFUnitOfWork>, IOperationService
    {
        public OperationService(string connectionString) : base(connectionString)
        { }

        public void Create(OperationVM operationVM)
        {
            var mapper = AutoMapperConfiguration.OperationVMToOperation.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Operations.Create(mapper.Map<Operation>(operationVM));
                database.Save();
                operationVM.OperationId = database.Operations.Find(o => o.OperationCode == operationVM.OperationCode).Last().OperationId;
            }
        }

        public OperationVM Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер операции", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var Operation = database.Operations.Get(id.Value);
                if (Operation == null)
                    throw new ValidationException("Операция не найдена", "");
                var mapper = AutoMapperConfiguration.OperationToOperationVM.CreateMapper();
                return mapper.Map<OperationVM>(Operation);
            }
        }

        public ObservableCollection<OperationVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.OperationToOperationVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<OperationVM>>(database.Operations.GetAll());
            }
        }

        public void Update(OperationVM OperationVM)
        {
            var mapper = AutoMapperConfiguration.OperationVMToOperation.CreateMapper();
            var Operation = mapper.Map<Operation>(OperationVM);
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Operations.Update(Operation);
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер операции", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Operations.Delete(id.Value);
                database.Save();
            }
        }

        public ObservableCollection<OperationVM> Find(IEnumerable<int> idRange)
        {
            var mapper = AutoMapperConfiguration.OperationToOperationVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<OperationVM>>
                    (database.Operations.Find(o => idRange.Contains(o.OperationId)));
            }
        }
    }
}
