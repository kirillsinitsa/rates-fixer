using RatesFixer.DAL.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class TariffPayVM : ModelBase, IDataErrorInfo
    {
        #region Properties
        public int TariffPayId { get; set; }

        private int jobClass;
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

        private TariffPayType tariffPayType;
        [Required(ErrorMessage = "Обязательное поле")]
        [EnumDataType(typeof(TariffPayType))]
        public TariffPayType TariffPayType
        {
            get => tariffPayType;
            set
            {
                tariffPayType = value;
                OnPropertyChanged();
            }
        }

        private int tariffPayCode;
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(11, 16)]
        public int TariffPayCode
        {
            get => tariffPayCode;
            set
            {
                tariffPayCode = value;
                OnPropertyChanged();
            }
        }
        [Range(1.00f, 2.00f)]
        public float TariffMultiplier { get; set; }
        public double TariffPayValue { get; set; }
        public string TariffPayGroup => GetTariffPayGroup();
        public string PayForm => GetPayForm();
        public string TariffPayTypeName => GetTariffPayTypeName();
        #endregion

        #region Ctors
        public TariffPayVM() { }
        public TariffPayVM(TariffPayVM other)
        {
            TariffPayId = other.TariffPayId;
            TariffPayGroupId = other.TariffPayGroupId;
            TariffPayCode = other.TariffPayCode;
            TariffMultiplier = other.TariffMultiplier;
            TariffPayValue = other.TariffPayValue;
            TariffPayType = other.TariffPayType;
            JobClass = other.JobClass;
        }
        #endregion

        #region Methods
        private string GetTariffPayGroup()
        {
            return string.Concat("0", TariffPayGroupId.ToString());
        }
        private string GetPayForm()
        {
            return TariffPayCode % 2 == 0 ? "Сдельно" : "Повременно";
        }
        private string GetTariffPayTypeName()
        {
            string typeName = string.Empty;
            switch (TariffPayType)
            {
                case TariffPayType.Month:
                    typeName = "Месячная";
                    break;
                case TariffPayType.Hour:
                    typeName = "Часовая";
                    break;
                case TariffPayType.Minute:
                    typeName = "Минутная";
                    break;
            }
            return typeName;
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
                    case nameof(TariffPayId):
                        errors = GetErrorsFromAnnotations(nameof(TariffPayId), TariffPayId);
                        break;
                    case nameof(JobClass):
                        errors = GetErrorsFromAnnotations(nameof(JobClass), JobClass);
                        break;
                    case nameof(TariffPayGroupId):
                        errors = GetErrorsFromAnnotations(nameof(TariffPayGroupId), TariffPayGroupId);
                        break;
                    case nameof(TariffPayType):
                        errors = GetErrorsFromAnnotations(nameof(TariffPayType), TariffPayType);
                        break;
                    case nameof(TariffPayCode):
                        errors = GetErrorsFromAnnotations(nameof(TariffPayCode), TariffPayCode);
                        break;
                    case nameof(TariffMultiplier):
                        errors = GetErrorsFromAnnotations(nameof(TariffMultiplier), TariffMultiplier);
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
