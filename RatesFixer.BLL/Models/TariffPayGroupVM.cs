using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class TariffPayGroupVM : ModelBase, IDataErrorInfo
    {
        #region Properties
        private int tariffPayGroupId;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 3)]
        public int TariffPayGroupId
        {
            get => tariffPayGroupId;
            set
            {
                tariffPayGroupId = value;
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
        public TariffPayGroupVM() { }
        public TariffPayGroupVM(TariffPayGroupVM other)
        {
            TariffPayGroupId = other.TariffPayGroupId;
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
                    case nameof(TariffPayGroupId):
                        errors = GetErrorsFromAnnotations(nameof(TariffPayGroupId), TariffPayGroupId);
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
