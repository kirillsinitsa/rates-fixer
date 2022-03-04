using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.DAL.Entities
{
    public class Rate
    {
        public int RateId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Id операции по тех. процессу.")]
        public int PartOperationId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Id тарифной ставки.")]
        public int TariffPayId { get; set; }
    }
}
