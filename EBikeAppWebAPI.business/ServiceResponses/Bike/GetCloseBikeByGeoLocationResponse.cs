using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.ServiceResponses.Bike
{
    public class GetCloseBikeByGeoLocationResponse
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
        public float PricePerMin { get; set; }
    }
}
