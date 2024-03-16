namespace Microservices.Demo.Report.API.CQRS.Queries.Infrastructure.Dtos.Product
{
    public class ProductDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int MaxNumberOfInsured { get; set; }
    }
}
