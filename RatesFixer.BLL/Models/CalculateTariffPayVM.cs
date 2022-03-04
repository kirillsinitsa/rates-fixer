using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.BLL.Models
{
    public class CalculateTariffPayVM : ModelBase, IDataErrorInfo
    {
        #region Properties
        [Required(ErrorMessage = "Обязательное поле")]
        public int AnnualOperationalHours { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public double FirstClassTimeWorkPay { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public double FirstClassPieceWorkPay { get; set; }
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
                    case nameof(AnnualOperationalHours):
                        errors = GetErrorsFromAnnotations(nameof(AnnualOperationalHours), AnnualOperationalHours);
                        break;
                    case nameof(FirstClassPieceWorkPay):
                        errors = GetErrorsFromAnnotations(nameof(FirstClassPieceWorkPay), FirstClassPieceWorkPay);
                        break;
                    case nameof(FirstClassTimeWorkPay):
                        errors = GetErrorsFromAnnotations(nameof(FirstClassTimeWorkPay), FirstClassTimeWorkPay);
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
