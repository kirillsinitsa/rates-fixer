using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RatesFixer.DAL.Entities
{
    [ComplexType]
    public class Payment
    {       
        [Required(ErrorMessage = "Обязательное поле Сумма.")]
        public double Sum { get; set; }
        [Required(ErrorMessage = "Обязательное поле Нормочасы.")]
        public double StandardHours { get; set; }
    }
}
