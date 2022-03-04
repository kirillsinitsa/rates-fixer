using Prism.Events;
using RatesFixer.Authentication.Model;
using RatesFixer.Authentication.Services;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.BaseViewModels;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace RatesFixer.ViewModels.Authentication
{
    public class UsersViewModel : ClosingBaseViewModel
    {
        private readonly CredentialsService credentialsService;
        public ObservableCollection<UserVM> Users { get; protected set; }

        public UserVM SelectedUser { get; set; }
        public UsersViewModel(IEventAggregator eventAggregator, string connectionString) : base(eventAggregator)
        {
            credentialsService = new CredentialsService(connectionString);
            RefreshUsers();
        }

        private void RefreshUsers()
        {
            Users = credentialsService.GetAllUsers();
        }

        private ICommand deleteUserCommand = null;

        public ICommand DeleteUserCommand => deleteUserCommand ?? (deleteUserCommand = new RelayCommand(DeleteUser));

        private void DeleteUser(object obj)
        {
            var res = MessageBox.Show("Вы уверены?", "Удаление", MessageBoxButton.OKCancel);
            if (res == MessageBoxResult.Cancel) return;
            try
            {
                int userId = SelectedUser.UserId;
                credentialsService.Delete(userId);
                Users.Remove(SelectedUser);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка удаления пользователя");
            }
        }
    }
}
