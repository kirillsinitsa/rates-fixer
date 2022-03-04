using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RatesFixer.DAL.Entities
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Range(1, 100000)]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = "Обязательное поле Фамилия")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Обязательное поле Имя.")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string Patronymic { get; set; }
        [Required(ErrorMessage = "Обязательное поле Разряд")]
        [Range(1, 6)]
        public int JobClass { get; set; }
        [Required(ErrorMessage = "Обязательное поле Код профессии.")]
        [Range(1, 100000)]
        public int OccupationCode { get; set; }
        [Range(1, 100)]
        public int WorkStationId { get; set; }
    }
}
