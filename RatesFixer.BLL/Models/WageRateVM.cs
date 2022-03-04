using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class WageRateVM : ModelBase, IDataErrorInfo
    {
        #region Properties
        public int WageRateId { get; set; }

        private float intencityOfWork;
        [Required(ErrorMessage = "Обязательное поле")]
        public float IntencityOfWork
        {
            get => intencityOfWork;
            set
            {
                intencityOfWork = value;
                OnPropertyChanged();
            }
        }

        private int partOperationId;
        [Required(ErrorMessage = "Обязательное поле")]
        public int PartOperationId
        {
            get => partOperationId;
            set
            {
                partOperationId = value;
                OnPropertyChanged();
            }
        }
        public PartOperationVM PartOperation { get; set; }
        public double WageRateValue { get; set; }
        #endregion

        #region Ctors
        public WageRateVM() { }
        public WageRateVM(WageRateVM other)
        {
            WageRateId = other.WageRateId;
            WageRateValue = other.WageRateValue;
            IntencityOfWork = other.IntencityOfWork;
            PartOperationId = other.PartOperationId;
            PartOperation = other.PartOperation;
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
                    case nameof(WageRateId):
                        errors = GetErrorsFromAnnotations(nameof(WageRateId), WageRateId);
                        break;
                    case nameof(PartOperationId):
                        errors = GetErrorsFromAnnotations(nameof(PartOperationId), PartOperationId);
                        break;
                    case nameof(IntencityOfWork):
                        errors = GetErrorsFromAnnotations(nameof(IntencityOfWork), IntencityOfWork);
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
