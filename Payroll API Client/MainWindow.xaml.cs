using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;

namespace Payroll_Api_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DateInput.SelectedDate = DateTime.Now.Date;
            WeeksInput.Text = "1";
        }

        private void GetPayrollButton_Click(object sender, RoutedEventArgs e)
        {
            PayrollOutput.ItemsSource = null;
            var weeks = 0;
            weeks = Convert.ToInt32(WeeksInput.Text);
            if (DateInput.SelectedDate != null)
            {
                var startDate = DateInput.SelectedDate.Value;
                if (startDate != null && weeks != 0)
                {
                    var dateString = startDate.ToString("yyyyMMdd");
                    try
                    {
                        RunAsync(dateString, weeks).Wait();
                    } catch (Exception ex) when (ex is InvalidOperationException || ex is AggregateException)
                    {
                        MessageBox.Show("There is no payroll to show.");
                    }
                }
            }
        }


        private async Task RunAsync(string startDate, int weeks)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://jadtimeclock.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    HttpResponseMessage response = client.GetAsync("api/payroll/" + startDate + "/" + weeks).Result;
                    response.EnsureSuccessStatusCode();

                    //*********************************************
                    // This is where the magic happens!

                    var payroll = (await response.Content.ReadAsAsync<IEnumerable<PayStub>>()).ToList();
                    //*********************************************

                    ShowPayroll(payroll);
                } catch (HttpRequestException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void ShowPayroll(List<PayStub> payroll)
        {
            // Display to datagrid view
            DataTable dt = new DataTable();
            foreach (var property in payroll.First().GetType().GetProperties())
            {
                dt.Columns.Add(new DataColumn(property.Name, property.PropertyType));
            }

            foreach (var payStub in payroll)
            {
                var newRow = dt.NewRow();

                foreach (var property in payroll.First().GetType().GetProperties())
                {
                    newRow[property.Name] = payStub.GetType().GetProperty(property.Name).GetValue(payStub, null);
                }

                dt.Rows.Add(newRow);
            }

            PayrollOutput.ItemsSource = dt.DefaultView;
        }
    }
}
