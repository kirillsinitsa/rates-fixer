using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class PartOperationVM : ModelBase, IDataErrorInfo
    {
        #region Properties
        public int PartOperationId { get; set; }

        private string number;
        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов.")]
        public string Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }

        private double rateTime;
        [Required(ErrorMessage = "Обязательное поле")]
        public double RateTime
        {
            get => rateTime;
            set
            {
                rateTime = value;
                OnPropertyChanged();
            }
        }

        private double percentage;
        [Required(ErrorMessage = "Обязательное поле")]
        public double Percentage
        {
            get => percentage;
            set
            {
                percentage = value;
                OnPropertyChanged();
            }
        }

        private int wageRateId;
        public int WageRateId
        {
            get => wageRateId;
            set
            {
                wageRateId = value;
                OnPropertyChanged();
            }
        }
        public double WageRateValue { get; set; }

        public int operationId;
        [Required(ErrorMessage = "Обязательное поле")]
        public int OperationId
        {
            get => operationId;
            set
            {
                operationId = value;
                OnPropertyChanged();
            }
        }
        public OperationVM Operation { get; set; }

        private int divisionId;
        [Required(ErrorMessage = "Обязательное поле")]
        public int DivisionId
        {
            get => divisionId;
            set
            {
                divisionId = value;
                OnPropertyChanged();
            }
        }

        public DivisionVM Division { get; set; }

        private int partId;
        [Required(ErrorMessage = "Обязательное поле")]
        public int PartId
        {
            get => partId;
            set
            {
                partId = value;
                OnPropertyChanged();
            }
        }
        public PartVM Part { get; set; }
        public string NumberWithName => GetNumberWithName();
        #endregion

        #region Methods
        private string GetNumberWithName()
        {
            return string.Concat(Number, " ", Operation.Name);
        }
        #endregion

        #region Ctors
        public PartOperationVM() { }

        public PartOperationVM(PartOperationVM other)
        {
            if (other == null) return;
            PartOperationId = other.PartOperationId;
            Number = other.Number;
            RateTime = other.RateTime;
            Percentage = other.Percentage;
            WageRateId = other.WageRateId;
            WageRateValue = other.WageRateValue;
            OperationId = other.OperationId;
            DivisionId = other.DivisionId;
            PartId = other.PartId;
            Operation = other.Operation;
            Division = other.Division;
            Part = other.Part;
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
                    case nameof(PartOperationId):
                        errors = GetErrorsFromAnnotations(nameof(PartOperationId), PartOperationId);
                        break;
                    case nameof(Number):
                        errors = GetErrorsFromAnnotations(nameof(Number), Number);
                        break;
                    case nameof(RateTime):
                        errors = GetErrorsFromAnnotations(nameof(RateTime), RateTime);
                        break;
                    case nameof(Percentage):
                        errors = GetErrorsFromAnnotations(nameof(Percentage), Percentage);
                        break;
                    case nameof(OperationId):
                        errors = GetErrorsFromAnnotations(nameof(OperationId), OperationId);
                        break;
                    case nameof(DivisionId):
                        errors = GetErrorsFromAnnotations(nameof(DivisionId), DivisionId);
                        break;
                    case nameof(PartId):
                        errors = GetErrorsFromAnnotations(nameof(PartId), PartId);
                        break;
                    case nameof(WageRateId):
                        errors = GetErrorsFromAnnotations(nameof(WageRateId), WageRateId);
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
