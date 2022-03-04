using System;
using System.Threading;
using System.Windows.Controls;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.BaseViewModels;
using RatesFixer;
using RatesFixer.Authentication.Services;
using RatesFixer.Authentication.Model;
using RatesFixer.Authentication.Principal;
using Prism.Events;

namespace RateFixer.ViewModels.Authentication
{
    public class AuthenticationViewModel : ClosingBaseViewModel
    {
        private readonly IAuthenticationService authenticationService;
        private int userId;
        private string status = string.Empty;

        public AuthenticationViewModel(IEventAggregator eventAggregator, string connectionString) : base(eventAggregator)
        {
            authenticationService = new AuthenticationService(connectionString);
        }

        #region Properties
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
        public string Status
        {
            get
            {
                if (IsAuthenticated)
                    return string.Format("Вам {0}",
                        Thread.CurrentPrincipal.IsInRole("Administrators") ? "доступно редактирование."
                            : "доступен только просмотр.");

                return status == string.Empty ? "Вход не выполнен!" : status;
            }
            set 
            {
                status = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        private DelegateCommand loginCommand = null;
        public DelegateCommand LoginCommand => loginCommand ?? (loginCommand = new DelegateCommand(Login, CanLogin));

        private DelegateCommand logoutCommand = null;
        public DelegateCommand LogoutCommand => logoutCommand ?? (logoutCommand = new DelegateCommand(Logout, CanLogout));
        #endregion

        private void Login(object parameter)
        {
            PasswordBox passwordBox = parameter as PasswordBox;
            string clearTextPassword = passwordBox.Password;
            try
            {
                UserVM user = authenticationService.AuthenticateUser(userId, clearTextPassword);

                if (!(Thread.CurrentPrincipal is CustomPrincipal customPrincipal))
                    throw new ArgumentException("The application's default thread principal must be set to a CustomPrincipal object on startup.");

                customPrincipal.Identity = new CustomIdentity(user.UserId, user.Role);

                OnPropertyChanged("AuthenticatedUser");
                OnPropertyChanged("IsAuthenticated");
                loginCommand.RaiseCanExecuteChanged();
                UserId = string.Empty;
                passwordBox.Password = string.Empty;
                Status = string.Empty;
                MainWindow view = new MainWindow();
                view.Show();

                App.Current.Windows[0].Close();
            }
            catch (UnauthorizedAccessException)
            {
                Status = "Вход не выполнен! Введите корректные табельный номер и пароль.";
            }
            catch (Exception ex)
            {
                Status = string.Format("Ошибка: {0}", ex.Message);
            }
        }

        private bool CanLogin(object parameter)
        {
            return !IsAuthenticated;
        }

        public bool IsAuthenticated => Thread.CurrentPrincipal.Identity.IsAuthenticated;

        private void Logout(object parameter)
        {
            CustomPrincipal customPrincipal = Thread.CurrentPrincipal as CustomPrincipal;
            if (customPrincipal != null)
            {
                customPrincipal.Identity = new AnonymousIdentity();
                OnPropertyChanged("AuthenticatedUser");
                OnPropertyChanged("IsAuthenticated");
                loginCommand.RaiseCanExecuteChanged();
                logoutCommand.RaiseCanExecuteChanged();
                Status = string.Empty;
            }
        }

        private bool CanLogout(object parameter)
        {
            return IsAuthenticated;
        }
    }
}
