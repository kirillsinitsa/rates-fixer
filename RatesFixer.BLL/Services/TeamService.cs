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
    public class TeamService : BaseService<EFUnitOfWork>, ITeamService
    {
        public TeamService(string connectionString) : base(connectionString)
        { }

        public void Create(TeamVM divisionVM)
        {
            var mapper = AutoMapperConfiguration.TeamVMToTeam.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Teams.Create(mapper.Map<Team>(divisionVM));
                database.Save();
            }
        }

        public TeamVM Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер бригады", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                var division = database.Teams.Get(id.Value);
                if (division == null)
                    throw new ValidationException("Бригада не найдена", "");
                var mapper = AutoMapperConfiguration.TeamToTeamVM.CreateMapper();
                return mapper.Map<TeamVM>(division);
            }
        }

        public ObservableCollection<TeamVM> GetAll()
        {
            var mapper = AutoMapperConfiguration.TeamToTeamVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<TeamVM>>(database.Teams.GetAll());
            }
        }
        public void Update(TeamVM divisionVM)
        {
            var mapper = AutoMapperConfiguration.TeamVMToTeam.CreateMapper();
            var division = mapper.Map<Team>(divisionVM);
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Teams.Update(division);
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан номер бригады", "");
            using (database = new EFUnitOfWork(connectionString))
            {
                database.Teams.Delete(id.Value);
                database.Save();
            }
        }

        public ObservableCollection<TeamVM> GetWhere(int? divisionId)
        {
            if (divisionId == null) throw new ValidationException("Не указан номер участка", "");
            var mapper = AutoMapperConfiguration.TeamToTeamVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<TeamVM>>
                    (database.Teams.Find(o => o.DivisionId == divisionId.Value));
            }
        }

        public ObservableCollection<TeamVM> FindWhere(IEnumerable<int> divisionsIdRange)
        {
            var mapper = AutoMapperConfiguration.TeamToTeamVM.CreateMapper();
            using (database = new EFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<TeamVM>>
                    (database.Teams.Find(o => divisionsIdRange.Contains(o.DivisionId)));
            }
        }
    }
}
