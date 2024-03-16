using MediatR;
using Microservices.Demo.Report.API.CQRS.Queries.Infrastructure.Dtos.Policy;
using Microservices.Demo.Report.API.CQRS.Queries.Policy.GetPolicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Report.API.Application
{
    public class PolicyApplicationService
    {
        private readonly IMediator _mediator;
        public PolicyApplicationService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IEnumerable<PoliciesDto>> GetPoliciesReport(string AgentLogin)
        {
            var result = await _mediator.Send(new GetPoliciesQuery { AgentLogin = AgentLogin });
            return result;
        }
    }
}
