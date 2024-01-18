using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TechStore.Models{
    public class Order {

        [BindNever]
        public int Id { get; set; }

        [Display(Name = "Имя")]
        public string Name { get; set; }
        
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        
        [Display(Name = "Адрес")]
        public string Adress { get; set; }
        
        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        [Required(ErrorMessage = "Длина номера не менее 10 знаков")]
        public string Phone { get; set; }
        
        [Display(Name = "e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [BindNever]
        [ScaffoldColumn(false)] //строка не будет отражена при просмотре исходного кода страницы
        public DateTime OrderTime { get; set; }
    }
}