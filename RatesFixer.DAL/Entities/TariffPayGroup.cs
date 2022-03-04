using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RatesFixer.DAL.Entities
{
    public class TariffPayGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(1, 3)]
        public int TariffPayGroupId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Коэффициент.")]
        [Range(1.00f, 2.00f)]
        public float Multiplier { get; set; }
    }
}
