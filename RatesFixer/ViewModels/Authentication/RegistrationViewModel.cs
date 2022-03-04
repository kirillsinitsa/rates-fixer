using System.Windows.Controls;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer.Authentication.Services;
using System.Windows.Input;
using System;
using System.Windows;
using Prism.Events;

namespace RatesFixer.ViewModels.Authentication
{
    public class RegistrationViewModel : ClosingBaseViewModel
    {
        private readonly AuthenticationService authenticationService;
        private int userId;
        public string UserId
        {
            get => userId == 0 ? string.Empty : userId.ToString();
            set
            {
                if (value == string.Empty)
                {
                    userId = 0;
                }
                else
                {
                    int id;
                    if (int.TryParse(value, out id)) userId = id;
                }
                OnPropertyChanged();
            }
        }

        private string role;
        public string Role
        {
            get => role;
            set
            {
                role = value;
                OnPropertyChanged();
            }
        }

        private bool isPasswordConfirmed = false;

        public bool IsPasswordConfirmed
        {
            get => isPasswordConfirmed;
            set
            {
                isPasswordConfirmed = value;
                OnPropertyChanged();
            }
        }

        public RegistrationViewModel(IEventAggregator eventAggregator, string connectionString): base(eventAggregator)
        {
            authenticationService = new AuthenticationService(connectionString);
        }

        private ICommand clearConfirmPasswordBoxCommand = null;
        public ICommand ClearConfirmPasswordBoxCommand => clearConfirmPasswordBoxCommand ?? (clearConfirmPasswordBoxCommand = new RelayCommand(ClearConfirmPasswordBox));
        private void ClearConfirmPasswordBox(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            passwordBox.Password = string.Empty;
        }

        private ICommand logonCommand = null;
        public ICommand LogonCommand => logonCommand ?? (logonCommand = new RelayCommand(Logon));
        private void Logon(object parameter)
        {
            if (!IsPasswordConfirmed) return;
            PasswordBox passwordBox = parameter as PasswordBox;
            if (passwordBox.Password == string.Empty) return;
            try
            {                           
                authenticationService.CreateNewUser(userId, passwordBox.Password, Role);
                userId = 0;
                passwordBox.Password = string.Empty;
                Role = string.Empty;
                IsPasswordConfirmed = false;
                MessageBox.Show("Пользователь успешно добавлен в базу.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                NLog.LogManager.GetCurrentClassLogger().Error(e, "Ошибка регистрации пользователя");
            }          
        }

        public void ConfirmPassword_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        public void ConfirmPassword_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            PasswordBox passwordBox1 = e.Parameter as PasswordBox;
            PasswordBox passwordBox2 = e.Source as PasswordBox;
            IsPasswordConfirmed = (passwordBox1.Password == passwordBox2.Password);
        }
    }
}
