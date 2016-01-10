using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http;
using Time_Clock_Web.Logic;
using Time_Clock_Web.Models;

namespace Time_Clock_Web.Controllers
{
    public class ApiPayrollController : ApiController
    {
        TimeClockUnitOfWork db = TimeClockUnitOfWork.Create();


        [Route("api/payroll/{dateString}/{weekString}")]
        [HttpGet]
        public IEnumerable<PayStub> Index(string dateString, string weekString)
        {
            //var rangeQuery = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            var startDate = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.CurrentCulture);

            var numberOfWeeks = Convert.ToInt32(weekString);
            var pc = new PayrollCalculator(db);

            var payroll = pc.GetPayroll(startDate, numberOfWeeks);

            return payroll;
        }
    }
}
