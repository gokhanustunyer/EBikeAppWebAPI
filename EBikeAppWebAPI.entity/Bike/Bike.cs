using EBikeAppWebAPI.entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.entity.Bike
{
    public class Bike: BaseEntity
    {
        public string Image { get; set; }
        public string QrCode { get; set; }
        public string Token { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
    }
}
