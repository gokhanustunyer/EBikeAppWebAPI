using EBikeAppWebAPI.entity.Base;
using EBikeAppWebAPI.entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.entity.CreditCard
{
    public class CreditCard: BaseEntity
    {
        public string NameOnCard { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CVC { get; set; }
        public AppUser User { get; set; }
    }
}
