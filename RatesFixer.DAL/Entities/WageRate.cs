using System.ComponentModel.DataAnnotations;

namespace RatesFixer.DAL.Entities
{
    public class WageRate
    {
        public int WageRateId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Напряженность труда.")]
        public float IntencityOfWork { get; set; }
        [Required(ErrorMessage = "Обязательное поле Id операции по тех. процессу.")]
        public int PartOperationId { get; set; }
        public double WageRateValue { get; set; }
    }
}
