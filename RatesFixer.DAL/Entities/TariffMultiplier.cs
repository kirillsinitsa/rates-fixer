using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RatesFixer.DAL.Entities
{
    public class TariffMultiplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(1, 6)]
        public int JobClass { get; set; }
        [Required(ErrorMessage = "Обязательное поле Коэффициент.")]
        [Range(1.00f, 2.00f)]
        public float Multiplier { get; set; }
    }
}
