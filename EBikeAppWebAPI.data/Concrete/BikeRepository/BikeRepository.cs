using EBikeAppWebAPI.data.Abstract.Auth.Endpoint;
using EBikeAppWebAPI.data.Abstract.Bike;
using EBikeAppWebAPI.data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.data.Concrete.BikeRepository
{
    public class BikeRepository : Repository<entity.Bike.Bike>, IBikeRepository
    {
        public BikeRepository(EBikeDbContext context) : base(context)
        {
        }
    }
}
