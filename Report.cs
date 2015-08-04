namespace FunctionalDemo
{
    public class Report
    {
        public string ToAddress { get; private set; }
        public string Body { get; private set; }

        public Report(string toAddress, string body)
        {
            ToAddress = toAddress;
            Body = body;
        }
    }
}