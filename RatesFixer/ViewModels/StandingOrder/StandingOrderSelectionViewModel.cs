using Prism.Events;
using RatesFixer.BLL.Interfaces;
using RatesFixer.BLL.Models;
using RatesFixer.BLL.Services;
using RatesFixer.DAL.Enums;
using RatesFixer.Infrastructure;
using RatesFixer.ViewModels.BaseViewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace RatesFixer.ViewModels.StandingOrder
{
    #region Fields and Properties
    public class StandingOrderSelectionViewModel : BaseViewModel
    {
        private readonly IOrderService orderService;
        private readonly IEventAggregator eventAggregator;
        public ICollection<OrderVM> Orders { get; internal set; }

        private int startYear = DateTime.Now.Year;
        public int StartYear
        { 
            get { return startYear; } 
            set 
            {
                startYear = value;
                OnPropertyChanged();
                if (EndYear == 0 || EndYear < value)
                {
                    EndYear = value;
                    OnPropertyChanged("EndYear");
                }
            }
        }

        public int endYear = DateTime.Now.Year;
        public int EndYear
        {
            get { return endYear; }
            set
            {
                if (value >= StartYear) endYear = value;
                OnPropertyChanged();
            }
        }

        private Month startMonth = (Month)DateTime.Now.Month;
        public Month StartMonth
        {
            get { return startMonth; }
            set
            {
                startMonth = value;
                OnPropertyChanged();
                if (EndMonth == 0 || EndMonth < value)
                {
                    EndMonth = value;
                    OnPropertyChanged("EndMonth");
                }
            }
        }

        public Month endMonth = (Month)DateTime.Now.Month;
        public Month EndMonth
        {
            get { return endMonth; }
            set
            {
                if (value >= StartMonth) endMonth = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Ctor
        public StandingOrderSelectionViewModel(IEventAggregator eventAggregator, string connectionString)
        {
            orderService = new OrderService(connectionString);
            this.eventAggregator = eventAggregator;
        }
        #endregion

        #region Command
        private ICommand selectOrdersCommand;

        public ICommand SelectOrdersCommand => selectOrdersCommand ?? (selectOrdersCommand = new RelayCommand(SelectOrders));
        private void SelectOrders(object parameter)
        {
            Orders = orderService.Select(StartYear, EndYear, StartMonth, EndMonth);

            if (Orders.Count == 0)
            {
                MessageBox.Show("Рапорты о выработке за выбранный период отсутствуют!");
                return;
            }
            if (parameter is Window dialogWindow)
            {                
                dialogWindow.DialogResult = true;
                dialogWindow.Close();
            }           
        }
        #endregion
    }
}
