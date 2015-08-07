using System;
using System.Collections.Generic;

namespace FunctionalDemo
{
    public static class Functional
    {
        public static void Main()
        {
            var runCustomerReportBatch = Compose();

            runCustomerReportBatch();
        }

        public static Action Compose()
        {
            return () => RunCustomerReportBatch(
                GetCustomersForCustomerReport,
                CreateCustomerReport,
                SendEmail);
        }

        public static void RunCustomerReportBatch(
            Func<IEnumerable<Customer>> getCustomersForCustomerReport,
            Func<Customer, Report> createCustomerReport,
            Action<string, string> sendEmail)
        {
            var customers = getCustomersForCustomerReport();

            foreach (var customer in customers)
            {
                var report = createCustomerReport(customer);
                sendEmail(report.ToAddress, report.Body);
            }
        }

        public static IEnumerable<Customer> GetCustomersForCustomerReport()
        {
            // pretend to do data access
            yield return new Customer("mike@mikelair.com");
            yield return new Customer("leo@leofort.com");
            yield return new Customer("yuna@yunacastle.com");
        }


        public static Report CreateCustomerReport(Customer customer)
        {
            return new Report(customer.Email, $"This is the report for {customer.Email}!");
        }

        public static void SendEmail(string toAddress, string body)
        {
            // pretend to send an email here
            Console.Out.WriteLine("Sent Email to: {0}, Body: '{1}'", toAddress, body);
        }
    }
}