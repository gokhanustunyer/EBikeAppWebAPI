using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.ViewModel.Ride
{
    public class StartRideModel
    {
        public string UserName { get; set; }
        public string BikeToken { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
    }
}
