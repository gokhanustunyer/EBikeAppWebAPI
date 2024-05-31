using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.ServiceResponses.Bike
{
    public class CreateBikeResponse
    {
        public string QrCodePath { get; set; }
        public string BikeToken { get; set; }
    }
}
