using System;
using System.ComponentModel.DataAnnotations;

namespace Time_Clock_Web.Models
{
    /// <summary>
    ///     A shift represents a span of time worked by a particular employee
    ///     Domain Object
    /// </summary>
    public class Shift
    {
        // Properties
        [Key]
        public string Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Time In")]
        public DateTime TimeStampIn { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Time Out")]
        public DateTime TimeStampOut { get; set; }

        [Display(Name = "Holiday?")]
        public bool IsHolidayPay { get; set; }

        // Foreign keys
        public string EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }


        public Shift(string employeeId, DateTime timeStampIn, DateTime timeStampOut, bool isHolidayPay = false)
        {
            Id = Guid.NewGuid().ToString();
            EmployeeId = employeeId;
            TimeStampIn = timeStampIn;
            TimeStampOut = timeStampOut;
            IsHolidayPay = isHolidayPay;
        }

        public Shift()
        {

        }
    }
}