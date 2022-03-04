using RatesFixer.BLL.Models;
using RatesFixer.ViewModels.BaseViewModels;
using System.Windows;

namespace RatesFixer.Views.WorkStation
{
    /// <summary>
    /// Логика взаимодействия для WorkStationDialogWindow.xaml
    /// </summary>
    public partial class WorkStationDialogWindow : Window
    {
        public WorkStationDialogWindow()
        {
            InitializeComponent();
        }
        public WorkStationDialogWindow(DialogViewModel<WorkStationVM> viewModel) : this()
        {
            DataContext = viewModel;
        }
    }
}
