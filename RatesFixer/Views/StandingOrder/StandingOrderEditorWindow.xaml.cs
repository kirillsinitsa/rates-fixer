using RatesFixer.ViewModels.StandingOrder;
using System;
using System.Windows;

namespace RatesFixer.Views.StandingOrder
{
    /// <summary>
    /// Логика взаимодействия для StandingOrderEditorWindow.xaml
    /// </summary>
    public partial class StandingOrderEditorWindow : Window
    {
        public StandingOrderEditorWindow()
        {
            InitializeComponent();
        }

        public StandingOrderEditorWindow(StandingOrderEditorViewModel viewModel) : this()
        {
            DataContext = viewModel;
            if (viewModel.CloseAction == null)
                viewModel.CloseAction = new Action(() => this.Close());
        }
    }
}
