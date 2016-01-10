using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll_Api_Client
{
    /// <summary>
    ///     PayStub holds pay information outputted by PayrollCalculator
    ///     DTO, not stored in DB
    /// </summary>

    [NotMapped]
    public class PayStub
    {
        [Display(Name = "Id")]
        public string EmployeeId { get; set; }

        [Display(Name = "Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Hours")]
        public double NormalHours { get; set; }

        [Display(Name = "Holiday Hours")]
        public double HolidayHours { get; set; }

        [DataType(DataType.Currency)]
        public decimal Wage { get; set; }

        [Display(Name = "Gross Earnings")]
        [DataType(DataType.Currency)]
        public decimal Earnings { get; set; }


        public PayStub()
        {

        }
    }
}