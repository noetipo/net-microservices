using MediatR;
using Microservices.Demo.Report.API.CQRS.Queries.Infrastructure.Dtos.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Report.API.CQRS.Queries.Policy.GetPolicies
{
    public class GetPoliciesQuery : IRequest<IEnumerable<PoliciesDto>>
    {
        public string AgentLogin;
    }
}
