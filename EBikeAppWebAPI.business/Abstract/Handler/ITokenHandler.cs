using EBikeAppWebAPI.business.DTOs;
using EBikeAppWebAPI.entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.Abstract.Handler
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(int second, AppUser user);
        string CreateRefreshToken();
    }
}
