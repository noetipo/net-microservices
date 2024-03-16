namespace Microservices.Demo.Report.API.CQRS.Queries.Infrastructure.Dtos.Policy
{
    public class PolicyDto
    {
        public string? Number { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? PolicyHolder { get; set; }
        public decimal TotalPremium { get; set; }
        public string? ProductCode { get; set; }
        public string? AccountNumber { get; set; }
    }
}
