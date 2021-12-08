using System.ComponentModel.DataAnnotations;

#nullable enable
namespace homework7._1.Models
{
    public class UserProfile
    {
        [Display(Name = "Фамилия")]
        public string FirstName { get; set; }
        [Display(Name = "Имя")]
        public string LastName { get; set; }
        [Display(Name = "Отчество")]
        public string? Patronymic { get; set; }
        public Sex Sex { get; set; }
        public int Age { get; set; }
    }
    
    public enum Sex
    {
        Male,
        Female
    };
}