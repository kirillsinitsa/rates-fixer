using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class TariffMultiplierVM : ModelBase, IDataErrorInfo
    {
        #region Properties
        private int jobClass;
        [Key]
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 6)]
        public int JobClass
        {
            get => jobClass;
            set
            {
                jobClass = value;
                OnPropertyChanged();
            }
        }

        private float multiplier;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1.00f, 2.00f)]
        public float Multiplier
        {
            get => multiplier;
            set
            {
                multiplier = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Ctors
        public TariffMultiplierVM() { }

        public TariffMultiplierVM(TariffMultiplierVM other)
        {
            JobClass = other.JobClass;
            Multiplier = other.Multiplier;
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
                    case nameof(JobClass):
                        errors = GetErrorsFromAnnotations(nameof(JobClass), JobClass);
                        break;
                    case nameof(Multiplier):
                        errors = GetErrorsFromAnnotations(nameof(Multiplier), Multiplier);
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
