using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microservices.Demo.Report.API.CQRS.Queries.Infrastructure.Dtos.Policy;
using Polly;
using Microservices.Demo.Report.API.CQRS.Queries.Infrastructure.Dtos.Product;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace Microservices.Demo.Report.API.CQRS.Queries.Policy.GetPolicies
{
    public class GetPoliciesHandler : IRequestHandler<GetPoliciesQuery, IEnumerable<PoliciesDto>>
    {
        private readonly HttpClient _httpClient;
        public GetPoliciesHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PoliciesDto>> Handle(GetPoliciesQuery request, CancellationToken cancellationToken)
        {
            var policies = await this.GetPolicies();
            var productCodeList = GetProductCodeList(policies);
            var products = await this.GetProducts(productCodeList);

            return JoinPoliciesWithProducts(policies, products, request.AgentLogin);
        }

        private async Task<List<PolicyDto>> GetPolicies()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://microservices.demo.policy.api/api/policies");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlcmljayIsIm5hbWUiOiJlcmljayIsInJvbGUiOlsiU0FMRVNNQU4iLCJVU0VSIl0sImF2YXRhciI6ImFzc2V0cy9hdmF0YXJzL2VyaWNrLnBuZyIsInVzZXJUeXBlIjoiU0FMRVNNQU4iLCJuYmYiOjE3MTAyNzQ1NDIsImV4cCI6MTcxMDg3OTM0MiwiaWF0IjoxNzEwMjc0NTQyfQ.FAGOnpEembIFZHgTEVgRUpbVF7JK7N3oMi8D7ZJBcmM");
            request.Content = new StringContent("", Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<PolicyDto>>(responseContent);
            }

            return new List<PolicyDto>();
        }

        private async Task<List<ProductDto>> GetProducts(List<string> productCodeList)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "http://microservices.demo.product.api/api/products/list");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJlcmljayIsIm5hbWUiOiJlcmljayIsInJvbGUiOlsiU0FMRVNNQU4iLCJVU0VSIl0sImF2YXRhciI6ImFzc2V0cy9hdmF0YXJzL2VyaWNrLnBuZyIsInVzZXJUeXBlIjoiU0FMRVNNQU4iLCJuYmYiOjE3MTAyNzQ1NDIsImV4cCI6MTcxMDg3OTM0MiwiaWF0IjoxNzEwMjc0NTQyfQ.FAGOnpEembIFZHgTEVgRUpbVF7JK7N3oMi8D7ZJBcmM");
            request.Content = new StringContent("", Encoding.UTF8, "application/json");

            var productCodesJson = JsonSerializer.Serialize(productCodeList);
            request.Content = new StringContent(productCodesJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<ProductDto>>(responseContent);
            }

            return new List<ProductDto>();
        }

        private List<string> GetProductCodeList(List<PolicyDto> policies)
        {
            List<string> productCodeList = new List<string>();
            foreach (var policy in policies)
            {
                if (!productCodeList.Contains(policy.ProductCode))
                {
                    productCodeList.Add(policy.ProductCode);
                }
            }
            return productCodeList;
        }

        private IEnumerable<PoliciesDto> JoinPoliciesWithProducts(List<PolicyDto> policies, List<ProductDto> products, string agentLogin)
        {
            List<PoliciesDto> listPolicies = new List<PoliciesDto>();
            foreach (var policy in policies)
            {
                var product = products.First(p => p.Code == policy.ProductCode);
                var itemPolicy = new PoliciesDto {
                    Number = policy.Number ?? "",
                    DateFrom = policy.DateFrom,
                    DateTo = policy.DateTo,
                    PolicyHolder = policy.PolicyHolder ?? "",
                    TotalPremium = policy.TotalPremium,
                    AccountNumber = null,
                    ProductCode = policy.ProductCode ?? "",
                    ProductName = product.Name,
                    ProductImage = product.Image,
                    ProductDescription = product.Description,
                    ProductMaxNumberOfInsured = product.MaxNumberOfInsured,
                    AgentLogin = agentLogin
                };
                listPolicies.Add(itemPolicy);
            }
            return listPolicies;
        }
    }
}
