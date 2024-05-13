using EBikeAppWebAPI.data.Abstract.Auth.Endpoint;
using EBikeAppWebAPI.data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.data.Concrete.Auth.Endpoint
{
    public class EndpointRepository : Repository<entity.Auth.Endpoint>, IEndpointRepository
    {
        public EndpointRepository(EBikeDbContext context) : base(context)
        {
        }
    }
}
