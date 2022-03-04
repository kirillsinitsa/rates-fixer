using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RatesFixer.BLL.Models;

namespace RatesFixer.Authentication.Model
{
    public class UserVM : ModelBase, IDataErrorInfo
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 100000)]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string Role { get; set; }
        public UserVM(int userId, string role)
        {
            UserId = userId;
            Role = role;
        }

        #region IDataErrorInfo members
        public string this[string columnName]
        {
            get
            {
                string[] errors = null;
                bool hasError = false;
                switch (columnName)
                {
                    case nameof(UserId):
                        errors = GetErrorsFromAnnotations(nameof(UserId), UserId);
                        break;
                    case nameof(Role):
                        errors = GetErrorsFromAnnotations(nameof(Role), Role);
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
