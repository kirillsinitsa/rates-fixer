using RatesFixer.BLL.Infrastructure;
using System.Collections.ObjectModel;
using RatesFixer.BLL.Services;
using RatesFixer.Authentication.Interfaces;
using RatesFixer.Authentication.Entities;
using RatesFixer.Authentication.Repositories;
using RatesFixer.Authentication.Model;
using RatesFixer.Authentication.Mappers;

namespace RatesFixer.Authentication.Services
{
    public class CredentialsService : BaseService<CredentialsEFUnitOfWork>, ICredentialsService
    {
        public CredentialsService(string connectionString) : base(connectionString)
        { }

        public void Create(InternalUserData userData)
        {
            using (database = new CredentialsEFUnitOfWork(connectionString))
            {
                database.Credentials.Create(userData);
                database.Save();
            }
        }

        public InternalUserData Get(int? id)
        {
            if (id == null) throw new ValidationException("Не указан табельный номер", "");
            using (database = new CredentialsEFUnitOfWork(connectionString))
            {
                var userData = database.Credentials.Get(id.Value);
                if (userData == null) throw new ValidationException("Пользователь не найден", "");
                return userData;
            }
        }

        public ObservableCollection<InternalUserData> GetAll()
        {
            using (database = new CredentialsEFUnitOfWork(connectionString))
            {
                return (ObservableCollection<InternalUserData>)database.Credentials.GetAll();
            }
        }
        public void Update(InternalUserData userData)
        {
            using (database = new CredentialsEFUnitOfWork(connectionString))
            {
                database.Credentials.Update(userData);
                database.Save();
            }
        }

        public void Delete(int? id)
        {
            if (id == null) throw new ValidationException("Не указан табельный номер", "");
            using (database = new CredentialsEFUnitOfWork(connectionString))
            {
                database.Credentials.Delete(id.Value);
                database.Save();
            }
        }

        public ObservableCollection<UserVM> GetAllUsers()
        {
            var mapper = AutoMapperConfiguration.InternalUserDataToUserVM.CreateMapper();
            using (database = new CredentialsEFUnitOfWork(connectionString))
            {
                return mapper.Map<ObservableCollection<UserVM>>(database.Credentials.GetAll());
            }           
        }
    }
}
