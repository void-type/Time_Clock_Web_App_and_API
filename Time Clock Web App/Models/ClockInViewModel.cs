using System.ComponentModel.DataAnnotations;

namespace Time_Clock_Web.Models
{
    public class ClockInViewModel
    {
        [Required]
        [Display(Name = "Swipe or Id")]
        public string Credentials { get; set; }
    }
}