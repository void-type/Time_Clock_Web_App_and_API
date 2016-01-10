using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Time_Clock_Web.Models
{
    /// <summary>
    ///     employees work at the store, they work shifts and get paid.
    ///     Domain Object
    /// </summary>
    public class Employee
    {

        [Key]
        [Display(Name = "Employee Id")]
        public string EmployeeId { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Id Card Number")]
        public string CardNumber { get; set; }

        public decimal Wage { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        public string AspIdentity { get; set; }

        [Display(Name = "Disabled?")]
        public bool Disabled { get; set; }

        [NotMapped]
        [Display(Name = "Name")]
        public string FullName => LastName + ", " + FirstName;

        // Foreign Key
        public virtual List<Shift> Shifts { get; set; }

        public Employee(string employeeId, string firstName, string lastName, string cardNumber, decimal wage, string email, string phone)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
            CardNumber = cardNumber;
            Wage = wage;
            EmailAddress = email;
            PhoneNumber = phone;
        }

        public Employee()
        {

        }
    }
}