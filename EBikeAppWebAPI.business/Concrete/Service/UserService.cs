using EBikeAppWebAPI.business.Abstract.Handler;
using EBikeAppWebAPI.business.Abstract.Service;
using EBikeAppWebAPI.business.DTOs;
using EBikeAppWebAPI.business.Exceptions;
using EBikeAppWebAPI.business.ServiceResponses.User;
using EBikeAppWebAPI.business.ViewModel.User;
using EBikeAppWebAPI.data.Abstract.Auth.Endpoint;
using EBikeAppWebAPI.data.Context;
using EBikeAppWebAPI.entity.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace EBikeAppWebAPI.business.Concrete.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EBikeDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenHandler _tokenHandler;
        readonly SignInManager<AppUser> _singInManager;
        private readonly EBikeDbContext _ebikeDbContext;
        public UserService(EBikeDbContext context,
                           UserManager<AppUser> userManager,
                           SignInManager<AppUser> signInManager,
                           IEmailService emailService,
                           RoleManager<AppRole> roleManager,
                           ITokenHandler tokenHandler,
                           SignInManager<AppUser> singInManager,
                           EBikeDbContext ebikeDbContext,
                           IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _roleManager = roleManager;
            _tokenHandler = tokenHandler;
            _singInManager = singInManager;
            _ebikeDbContext = ebikeDbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CreateUser(UserCreateModel model)
        {
            AppUser check = await _userManager.FindByEmailAsync(model.Email);
            if (check != null) { return false; }
            if (model.Password != model.PasswordAgn)
            {
                return false;
            }
            AppUser user = new AppUser()
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                Id = Guid.NewGuid().ToString(),
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                PhoneNumber = model.PhoneNumber,
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string url = $"Auth/ConfirmEmail?userId={user.Id}&token={HttpUtility.UrlEncode(token)}";
                Console.Out.WriteLine(token);
                await _emailService.SendEmailAsync(user.Email, "ebike.com.tr Hoş Geldiniz, Lütfen E-posta Adresinizi Doğrulayın", url);
                AppRole role = await _context.Roles.FirstOrDefaultAsync(r => r.NormalizedName == "USER");
                if (role != null)
                {
                    await AddToRoleAsync(user.Id, role.Id);
                }
            }
            return result.Succeeded;
        }
        public async Task<bool> AddToRoleAsync(string userId, string roleId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            AppRole role = await _roleManager.FindByIdAsync(roleId);
            await _userManager.AddToRoleAsync(user, role.Name);
            return true;
        }

        public async Task<bool> ConfirmEmail(string userId, string token)
        {
            IdentityResult result = new();
            AppUser user;
            if (userId != null && token != null)
            {
                user = await _userManager.FindByIdAsync(userId);
                result = await _userManager.ConfirmEmailAsync(user, token);
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                }
            }
            return result.Succeeded;
        }

        public async Task<bool> UpdateEmailAsync(UpdateEmailModel model)
        {
            if (model.NewEmail != model.NewEmailAgain) { return false; }
            AppUser user1 = await _userManager.FindByNameAsync(model.UserName);
            AppUser user2 = await _userManager.FindByEmailAsync(model.Email);
            if (user1 != user2) { return false; }
            bool pswCheck = await _userManager.CheckPasswordAsync(user1, model.Password);
            if (!pswCheck) { return false; }
            string token = await _userManager.GenerateChangeEmailTokenAsync(user1, model.NewEmail);
            string tokenUrl = $"User/ConfirmUpdateEmail?userId={user1.Id}&token={HttpUtility.UrlEncode(token)}&newEmail={model.NewEmail}";
            await _emailService.SendEmailAsync(model.NewEmail, "E-Posta Adresinizi Değiştirin", tokenUrl);

            return true;
        }

        public async Task<bool> ConfirmUpdateEmail(string userId, string token, string newEmail)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            IdentityResult result = await _userManager.ChangeEmailAsync(user, newEmail, token);
            if (!result.Succeeded)
            {
                return false;
            }
            user.UserName = newEmail;
            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, true);
            return true;
        }

        public async Task<bool> SendUpdatePasswordRequestAsync(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string url = $"user/updatePassword/?token={HttpUtility.UrlEncode(token)}";
            await _emailService.SendEmailAsync(user.Email, "Şifre Yenileme", url);
            return true;
        }

        public async Task<bool> UpdatePassword(UpdatePasswordModel model)
        {
            if (model.NewPassword != model.NewPasswordAgain) { return true; }
            AppUser user = await _userManager.FindByNameAsync(model.UserName);
            bool pswCheck = await _userManager.CheckPasswordAsync(user, model.CurrentPassword);
            if (!pswCheck) { return true; }
            IdentityResult result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!result.Succeeded) { return true; }
            await _userManager.UpdateSecurityStampAsync(user);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(user, true);
            return true;
        }

        public async Task<bool> UpdateUserNames(string userName, string FirstName, string LastName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            user.Name = FirstName;
            user.Surname = LastName;
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<bool> SendResetPasswordRequest(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string url = $"Auth/ResetPassword?userId={user.Id}&token={HttpUtility.UrlEncode(token)}";
            await _emailService.SendEmailAsync(user.Email, "Şifretiniz Sıfırlayın - Arite", url);
            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordModel model)
        {
            AppUser user;
            IdentityResult result;
            if (model.Password != model.PasswordAgain)
            {
                return false;
            }
            user = await _userManager.FindByIdAsync(model.userId);
            if (user == null)
            {
                return false;
            }
            result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            return result.Succeeded;
        }

        public async Task<LoginUserResponse> LoginAsync(string email, string password)
        {
            int accessTokenLifeTime = 2700;
            AppUser user = await _userManager.FindByNameAsync(email);
            if (user == null)
                user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new NotFoundUserException("Es gibt auf diese Weise keinen registrierten Benutzer");
            if (!user.EmailConfirmed)
                throw new AuthenticationErrorException();

            SignInResult result = await _singInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await UpdateRefreshToken(user, token.RefreshToken, token.Expiration, 900);
                return new LoginUserResponse()
                {
                    AccessToken = token.AccessToken,
                    Expiration = token.Expiration,
                    RefreshToken = token.RefreshToken
                };
            }
            throw new AuthenticationErrorException();
        }

        public async Task<bool> UpdateRefreshToken(AppUser user, string refreshToken, DateTime acccessTokenDate, int addOnAccessTokenSecond)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = acccessTokenDate.AddSeconds(addOnAccessTokenSecond);
                await _userManager.UpdateAsync(user);
                return true;
            }
            throw new DirectoryNotFoundException();
        }
        public async Task<bool> IsAdminTokenAsync(string? refreshToken)
        {
            if (refreshToken != null)
            {
                AppUser user = _ebikeDbContext.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
                if (user != null)
                {
                    bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                    bool isSuperAdmin = await _userManager.IsInRoleAsync(user, "Super Admin");
                    return isAdmin || isSuperAdmin;
                }
            }
            return false;
        }
        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = _userManager.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
            if (user != null && user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(2700, user);
                await UpdateRefreshToken(user, token.RefreshToken, token.Expiration, 900);
                return token;
            }
            else
                throw new NotFoundUserException();
        }

        public async Task<GetLoogedIdUserProfileResponse> GetLoogedIdUserProfile()
        {
            string activeUserName = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (activeUserName != null)
            {
                AppUser user = await _userManager.FindByNameAsync(activeUserName);
                return new()
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email
                };
            }
            else
            {
                throw new NotFoundUserException();
            }
        }

        public async Task<List<GetLoggedInUserPastRidesResponse>> GetLoggedInUserPastRides()
        {
            string activeUserName = _httpContextAccessor.HttpContext.User.Identity.Name;
            if (activeUserName != null)
            {
                AppUser user = await _ebikeDbContext.Users.Include(u => u.Rides).FirstOrDefaultAsync(u => u.UserName == activeUserName);
                return user.Rides.Select(r => new GetLoggedInUserPastRidesResponse()
                {
                     TotalPrice = r.Price,
                     EndDate = r.EndDate,
                     StartDate = r.StartDate,
                     EndLocation = new ViewModel.Location.Location()
                     {
                         Lat = r.EndLat,
                         Long = r.EndLong
                     },
                     StartLocation = new ViewModel.Location.Location()
                     {
                         Lat = r.StartLat,
                         Long = r.StartLong
                     }
                }).ToList();
            }
            else
            {
                throw new NotFoundUserException();
            }
        }
    }
}
