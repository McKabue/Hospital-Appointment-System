using Hos.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Hos.HELPERS
{
    public class AuthRepo : IDisposable
    {
        private HosContext _ctx;
        private PasswordHasher hasher;
        private UserManager<UserProfile> _userManager;

        public AuthRepo()
        {
            hasher = new PasswordHasher();
            _ctx = new HosContext();
            _userManager = new UserManager<UserProfile>(new UserStore<UserProfile>(_ctx));
            _userManager.UserValidator = new UserValidator<UserProfile>(_userManager) { AllowOnlyAlphanumericUserNames = false };
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            var result = _userManager.Create(new UserProfile() { SurName = userModel.SurName, FirstName = userModel.FirstName, LastName = userModel.LastName, National_ID_Number = userModel.National_ID_Number, UserName = userModel.UserName, RoleName = userModel.RoleName, PasswordHash = hasher.HashPassword(userModel.Password) });
            return result;
        }

        public async Task<UserProfile> FindUser(string userName, string password)
        {
            UserProfile user = await _userManager.FindAsync(userName, password);

            return user;
        }

        /*public async Task<UserProfile> FindByName(string userName)
        {
            UserProfile user = await _userManager.FindByNameAsync(userName);
            return user;

        }*/

        public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        {
            IdentityUser user = await _userManager.FindAsync(loginInfo);

            return user;
        }

        public async Task<IdentityResult> CreateAsync(UserProfile user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}