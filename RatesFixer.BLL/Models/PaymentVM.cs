using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class PaymentVM : ModelBase, IDataErrorInfo
    {
        #region Properties
        private double sum;
        [Required(ErrorMessage = "Обязательное поле")]
        public double Sum
        {
            get => sum;
            set
            {
                sum = value;
                OnPropertyChanged();
            }
        }

        private double standardHours;
        [Required(ErrorMessage = "Обязательное поле")]
        public double StandardHours
        {
            get => standardHours;
            set
            {
                standardHours = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Ctors
        public PaymentVM() { }

        public PaymentVM(PaymentVM other)
        {
            Sum = other.Sum;
            StandardHours = other.StandardHours;
        }
        #endregion

        #region IDataErrorInfo members
        public string this[string columnName]
        {
            get
            {
                string[] errors = null;
                bool hasError = false;
                switch (columnName)
                {                    
                    case nameof(Sum):
                        errors = GetErrorsFromAnnotations(nameof(Sum), Sum);
                        break;
                    case nameof(StandardHours):
                        errors = GetErrorsFromAnnotations(nameof(StandardHours), StandardHours);
                        break;
                }
                if (errors != null && errors.Length != 0)
                {
                    ClearErrors(columnName);
                    AddErrors(columnName, errors);
                    hasError = true;
                }
                if (!hasError) ClearErrors(columnName);
                return string.Empty;
            }
        }
        public string Error { get; }
        #endregion
    }
}
