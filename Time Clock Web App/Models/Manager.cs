namespace Time_Clock_Web.Models
{
    /// <summary>
    ///     Manager is a special type of employee that can perfrom administrative tasks
    ///     Domain Object
    /// </summary>

    public class Manager : Employee
    {
        public Manager(string employeeId, string firstName, string lastName, string cardNumber, decimal wage, string email, string phone)
            : base(employeeId, firstName, lastName, cardNumber, wage, email, phone)
        {
        }

        public Manager() { }

    }
}