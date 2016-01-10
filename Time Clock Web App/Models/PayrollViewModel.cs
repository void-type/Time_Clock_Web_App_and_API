using System;
using System.ComponentModel.DataAnnotations;

namespace Time_Clock_Web.Models
{
    public class PayrollViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Payroll Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Weeks For Period")]
        [Range(1, 4)]
        public int NumberOfWeeks { get; set; } = 1;
    }
}