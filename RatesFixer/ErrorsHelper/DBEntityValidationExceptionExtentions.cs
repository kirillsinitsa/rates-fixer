using System.Data.Entity.Validation;
using System.Text;

namespace RatesFixer.ErrorsHelper
{
    public static class DBEntityValidationExceptionExtentions
    {
        public static string EntityValidationErrorsToString(this DbEntityValidationException e)
        {
            var sb = new StringBuilder();
            foreach (var eve in e.EntityValidationErrors)
            {
                foreach (var ve in eve.ValidationErrors)
                {
                    sb.Append(string.Concat(ve.PropertyName, "-", ve.ErrorMessage, "\n"));
                }
            }
            return sb.ToString();
        }
    }
}
