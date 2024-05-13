using EBikeAppWebAPI.business.ViewModel.User;
using EBikeAppWebAPI.entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.Abstract.Service
{
    public interface IUserService
    {
        Task<bool> CreateUser(UserCreateModel model);
        Task<LoginUserResponse> LoginAsync(string email, string password);
        Task<bool> AddToRoleAsync(string userId, string roleId);
        Task<bool> ConfirmEmail(string userId, string token);
        Task<bool> UpdateEmailAsync(UpdateEmailModel model);
        Task<bool> ConfirmUpdateEmail(string userId, string token, string newEmail);
        Task<bool> SendUpdatePasswordRequestAsync(string userName);
        Task<bool> UpdatePassword(UpdatePasswordModel model);
        Task<bool> UpdateUserNames(string userName, string FirstName, string LastName);
        Task<bool> SendResetPasswordRequest(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordModel model);
        Task<bool> UpdateRefreshToken(AppUser user, string refreshToken, DateTime acccessTokenDate, int addOnAccessTokenSecond);
    }
}
