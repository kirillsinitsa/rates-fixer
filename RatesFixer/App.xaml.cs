using RateFixer.ViewModels;
using RateFixer.ViewModels.Authentication;
using RatesFixer.Authentication.Principal;
using RatesFixer.Authentication.Services;
using System;
using System.Configuration;
using System.Windows;

namespace RatesFixer
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            CustomPrincipal customPrincipal = new CustomPrincipal();
            AppDomain.CurrentDomain.SetThreadPrincipal(customPrincipal);

            base.OnStartup(e);

            string connectionString = ConfigurationManager.ConnectionStrings["CredentialsDefaultConnection"].ConnectionString;
            AuthenticationViewModel viewModel = new AuthenticationViewModel(null, connectionString);
            Window loginWindow = new LoginWindow(viewModel);
            loginWindow.Show();
        }
    }
}
