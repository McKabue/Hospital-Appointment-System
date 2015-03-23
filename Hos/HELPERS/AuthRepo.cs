using Hos.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

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



        public async Task<IEnumerable> Update(UserModel userModel)
        {
            UserProfile user1 = await _userManager.FindByIdAsync(userModel.Id);
            if (user1 == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            user1.Id = userModel.Id;
            user1.FirstName = userModel.FirstName;
            user1.LastName = userModel.LastName;
            user1.SurName = userModel.SurName;
            user1.UserName = userModel.UserName;
            user1.RoleName = userModel.RoleName;

            var updatedUser = await _userManager.UpdateAsync(user1);
            _ctx.SaveChanges();

            var USER = (from user in _ctx.Users.Where(u => u.Id == userModel.Id)
                        select new
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Surname = user.SurName,
                            UserName = user.UserName,
                            Role = user.RoleName,
                        }).AsEnumerable();

            return USER;
        }


        public async Task<IEnumerable> Delete(string id)
        {
            UserProfile user1 = await _userManager.FindByIdAsync(id);
            if (user1 == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var updatedUser = await _userManager.DeleteAsync(user1);
            _ctx.SaveChanges();

            return id;
        }

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