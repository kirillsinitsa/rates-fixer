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
    public class WorkStationService : BaseService<EFUnitOfWork>, IWorkStationService
    {
        public WorkStationService(string connectionString) : base(connectionString)
        { }

        public void Create(WorkStationVM workStationVM)
        {
            var mapper = AutoMapperConfiguration.WorkStationVMToWorkStation.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                if (database.Divisions.Get(workStationVM.DivisionId) == null)
                    throw new ValidationException("Участок не найден", "");
                database.WorkStations.Create(mapper.Map<WorkStation>(workStationVM));
                database.Save();
                workStationVM.WorkStationId = database.WorkStations.
                    Find(o => (o.DivisionId == workStationVM.DivisionId) && (o.Number == workStationVM.Number)).Last().WorkStationId;
            }
        }

        public WorkStationVM Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер рабочего места", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var WorkStation = database.WorkStations.Get(id.Value);
                if (WorkStation == null) throw new ValidationException("Рабочее место не найдено", "");
                var mapper = AutoMapperConfiguration.WorkStationToWorkStationVM.CreateMapper();
                return mapper.Map<WorkStationVM>(WorkStation);
            }
        }

        public ObservableCollection<WorkStationVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.WorkStationToWorkStationVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<WorkStationVM>>(database.WorkStations.GetAll());
            }
        }

        public void Update(WorkStationVM workStationVM)
        {
            var mapper = AutoMapperConfiguration.WorkStationVMToWorkStation.CreateMapper();
            var workStation = mapper.Map<WorkStation>(workStationVM);
            using (database = new EFUnitOfWork(connectionString))
            {
                database.WorkStations.Update(workStation);
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер рабочего места", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.WorkStations.Delete(id.Value);
                database.Save();
            }
        }

        public ObservableCollection<WorkStationVM> GetWhere(int? divisionId)
        {
            if (divisionId == null) throw new ValidationException("Не указан номер участка", "");
            var mapper = AutoMapperConfiguration.WorkStationToWorkStationVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<WorkStationVM>>
                    (database.WorkStations.Find(o => o.DivisionId == divisionId.Value));
            }
        }

        public ObservableCollection<WorkStationVM> Find(IEnumerable<int> idRange)
        {
            var mapper = AutoMapperConfiguration.WorkStationToWorkStationVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<WorkStationVM>>
                    (database.WorkStations.Find(o => idRange.Contains(o.WorkStationId)));
            }
        }

        public ObservableCollection<WorkStationVM> FindWhere(IEnumerable<int> divisionsIdRange)
        {
            var mapper = AutoMapperConfiguration.WorkStationToWorkStationVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<WorkStationVM>>
                    (database.WorkStations.Find(o => divisionsIdRange.Contains(o.DivisionId)));
            }
        }
    }
}

