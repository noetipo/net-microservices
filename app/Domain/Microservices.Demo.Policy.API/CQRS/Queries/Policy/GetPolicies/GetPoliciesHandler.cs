using MediatR;
using Microservices.Demo.Policy.API.Infrastructure.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microservices.Demo.Policy.API.Infrastructure.Data.Entities;
using Microservices.Demo.Policy.API.Domain;
using Microservices.Demo.Policy.API.CQRS.Queries.Infrastructure.Dtos.Policy;
using Polly;

namespace Microservices.Demo.Policy.API.CQRS.Queries.Policy.GetPolicies
{
    public class GetPoliciesHandler : IRequestHandler<GetPoliciesQuery, IEnumerable<PoliciesDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PolicyDomainService _policyDomainService;

        public GetPoliciesHandler(IUnitOfWork unitOfWork, PolicyDomainService policyDomainService)
        {
            _unitOfWork = unitOfWork;
            _policyDomainService = policyDomainService;
        }

        public async Task<IEnumerable<PoliciesDto>> Handle(GetPoliciesQuery request, CancellationToken cancellationToken)
        {
            var policies = await _unitOfWork.Policies.GetAll();

            return ConstructResult(policies);
        }

        private IEnumerable<PoliciesDto> ConstructResult(IEnumerable<API.Infrastructure.Data.Entities.Policy> policies)
        {
            List<PoliciesDto> listPolicies = new List<PoliciesDto>();
            foreach (var policy in policies)
            {
                // IEnumerable<PolicyVersion> versions = policy.PolicyVersions;
                // var ev = versions.First(v => v.VersionNumber == 1);

                // var effectiveVersion = _policyDomainService.FirstVersion(policy.PolicyVersions);
                var itemPolicy = new PoliciesDto {
                    Number = policy.Number,
                    ProductCode = policy.ProductCode,
                    DateFrom = DateTime.Today, //effectiveVersion.CoverPeriodPolicyValidityPeriod.PolicyFrom,
                    DateTo = DateTime.Today.AddDays(5), // effectiveVersion.CoverPeriodPolicyValidityPeriod.PolicyTo,
                    PolicyHolder = "Nombres", // $"{effectiveVersion.PolicyHolder.FirstName} {effectiveVersion.PolicyHolder.LastName}",
                    TotalPremium = 100, //effectiveVersion.TotalPremiumAmount,
                    AccountNumber = null
                };
                listPolicies.Add(itemPolicy);
            }
            return listPolicies;
        }
    }
}
