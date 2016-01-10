using System;
using System.ComponentModel.DataAnnotations;

namespace Time_Clock_Web.Models
{
    public class IncompleteShift
    {
        [Key]
        public string Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Time In")]
        public DateTime TimeStampIn { get; set; }

        [Display(Name = "Holiday?")]
        public bool IsHolidayPay { get; set; }


        // Foreign keys
        public string EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }


        public IncompleteShift(string employeeId, DateTime timeStampIn, bool isHolidayPay = false)
        {
            Id = Guid.NewGuid().ToString();
            EmployeeId = employeeId;
            TimeStampIn = timeStampIn;
            IsHolidayPay = isHolidayPay;

        }

        public IncompleteShift() { }
    }
}
