using EBikeAppWebAPI.data.Abstract.Auth.Endpoint;
using EBikeAppWebAPI.data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EBikeAppWebAPI.data.Concrete.Auth.Menu
{
    public class MenuRepository : Repository<entity.Auth.Menu>, IMenuRepository
    {
        public MenuRepository(EBikeDbContext context) : base(context)
        {
        }
    }
}
