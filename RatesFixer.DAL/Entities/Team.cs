using System.ComponentModel.DataAnnotations;

namespace RatesFixer.DAL.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Номер бригады.")]
        [Range(1, 100)]
        public int Number { get; set; }
        [Required(ErrorMessage = "Обязательное поле Id участка.")]
        [Range(1, 100)]
        public int DivisionId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Таб. номер бригадира.")]
        [Range(1, 100000)]
        public int ForemanId { get; set; }
        [MaxLength(100)]
        public string Note { get; set; }

    }
}
