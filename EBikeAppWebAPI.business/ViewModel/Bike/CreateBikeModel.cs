using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.ViewModel.Bike
{
    public class CreateBikeModel
    {
        public IFormFile Image { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
        public float PricePerMin { get; set; }
    }
}
