using MediatR;
using Microservices.Demo.Policy.API.CQRS.Queries.Infrastructure.Dtos.Policy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Demo.Policy.API.CQRS.Queries.Policy.GetPolicies
{
    public class GetPoliciesQuery : IRequest<IEnumerable<PoliciesDto>>
    {
    }
}
