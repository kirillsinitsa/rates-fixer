using RatesFixer.ViewModels.FactoryFloor;
using System.Windows;

namespace RatesFixer.Views.FactoryFloor
{
    /// <summary>
    /// Логика взаимодействия для FactoryFloorDialogWindow.xaml
    /// </summary>
    public partial class FactoryFloorDialogWindow : Window
    {
        public FactoryFloorDialogWindow()
        {
            InitializeComponent();
        }

        public FactoryFloorDialogWindow(FactoryFloorDialogViewModel viewModel) : this()
        {
            DataContext = viewModel;
        }

    }
}
