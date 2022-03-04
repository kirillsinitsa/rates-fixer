using RatesFixer.DAL.Enums;
using System.ComponentModel.DataAnnotations;

namespace RatesFixer.DAL.Entities
{
    public class TariffPay
    {
        public int TariffPayId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Разряд.")]
        [Range(1, 6)]
        public int JobClass { get; set; }
        [Required(ErrorMessage = "Обязательное поле Id группы тарифных ставок.")]
        [Range(1, 3)]
        public int TariffPayGroupId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Вид тарифной ставки.")]
        [EnumDataType(typeof(TariffPayType))]
        public TariffPayType TariffPayType { get; set; }
        [Required(ErrorMessage = "Обязательное поле Код тарифной ставки.")]
        [Range(11, 16)]
        public int TariffPayCode { get; set; }
        public double TariffPayValue { get; set; }
    }
}
