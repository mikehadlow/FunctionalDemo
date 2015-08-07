using System;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace FunctionalDemo
{
    public class UnitTests
    {
        [Fact]
        public void ObjectOrientedTest()
        {
            // Arrange
            var customerDataMock = new Mock<ICustomerData>();   
            var reportBuilderMock = new Mock<IReportBuilder>();
            var emailerMock = new Mock<IEmailer>();

            var expectedCustomer = new Customer("fist@sea.com");
            var expectedReportBody = "the report body";

            customerDataMock.Setup(x => x.GetCustomersForCustomerReport())
                .Returns(new[] { expectedCustomer });

            reportBuilderMock.Setup(x => x.CreateCustomerReport(expectedCustomer))
                .Returns(new Report(expectedCustomer.Email, expectedReportBody));

            var sut = new ReportingService(
                customerDataMock.Object, 
                reportBuilderMock.Object, 
                emailerMock.Object);

            // Act
            sut.RunCustomerReportBatch();

            // Assert
            emailerMock.Verify(x => x.Send(expectedCustomer.Email, expectedReportBody));
        }

        [Fact]
        public void FunctionalTest()
        {
            // arrange
            var expectedCustomer = new Customer("fist@sea.com");
            var expectedReportBody = "the report body";

            Func<IEnumerable<Customer>> getCustomersForCustomerReport  = 
                () => new[] {expectedCustomer};

            Func<Customer, Report> createCustomerReport = 
                customer => new Report(expectedCustomer.Email, expectedReportBody);

            var actualToAddress = "";
            var actualBody = "";

            Action<string, string> sendEmail = (toAddress, body) =>
            {
                actualToAddress = toAddress;
                actualBody = body;
            };

            // act
            Functional.RunCustomerReportBatch(getCustomersForCustomerReport, createCustomerReport, sendEmail);

            // assert
            Assert.Equal(expectedCustomer.Email, actualToAddress);
            Assert.Equal(expectedReportBody, actualBody);
        }
    }
}