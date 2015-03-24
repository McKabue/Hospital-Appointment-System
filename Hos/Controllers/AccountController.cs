using Hos.HELPERS;
using Hos.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Hos.Controllers
{
   // [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : SignalrBaseWebApiController<hosHub>
    {
        private HosContext context = new HosContext();
        private AuthRepo _repo = null;

        public AccountController()
        {
            _repo = new AuthRepo();
        }

        // POST api/Account/Register
        [Authorize]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
                var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
                if (roleName == "ADMIN")
                {

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var findUser = await _repo.findUserAsync(userModel);
                    if (findUser == null)
                    {
                        var result = await _repo.RegisterUser(userModel);
                        Hub.Clients.All.newUser(result);
                        return Ok();
                    }

                    else
                    {
                        return BadRequest("User With the UserName or Admission Number " + userModel.UserName + " already Exists");
                    }
                    

                    
                    
                }
                else
                {
                    return new ResponseMessageResult(Request.CreateErrorResponse((HttpStatusCode)777, new HttpError("You are not an ADMIN")));
                } 
            }

            else
            {
                return new ResponseMessageResult(Request.CreateErrorResponse((HttpStatusCode)888, new HttpError("You are not Allowed")));
            }
        }


        [Authorize]
        [Route("Update")]
        public async Task<IHttpActionResult> PutUpdate(UserModel userModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
                var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
                if (roleName == "ADMIN")
                {
                    var userr = await _repo.Update(userModel);

                    Hub.Clients.All.usersChanged(userModel.Id, userr);
                    return Ok();
                }
                else
                {
                    return new ResponseMessageResult(Request.CreateErrorResponse((HttpStatusCode)777, new HttpError("You are not an ADMIN")));
                }
            }

            else
            {
                return new ResponseMessageResult(Request.CreateErrorResponse((HttpStatusCode)888, new HttpError("You are not Allowed")));
            }
        }



        [HttpDelete]
        [Authorize]
        [Route("Delete")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
                var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
                if (roleName == "ADMIN")
                {
                    var userr = await _repo.Delete(id);

                    Hub.Clients.All.userDeleted(id);
                    return Ok();
                }
                else
                {
                    return new ResponseMessageResult(Request.CreateErrorResponse((HttpStatusCode)777, new HttpError("You are not an ADMIN")));
                }
            }

            else
            {
                return new ResponseMessageResult(Request.CreateErrorResponse((HttpStatusCode)888, new HttpError("You are not Allowed")));
            }
        }


       // [AllowAnonymous]
        [Route("AllUsers")]
        public async Task<IHttpActionResult> Get()
        {
            /*if (User.Identity.IsAuthenticated)
            {
                var cp = (ClaimsPrincipal)User; //var cp = User as ClaimsPrincipal;
                var roleName = ((Claim)cp.Claims.SingleOrDefault(x => x.Type == "RoleName")).Value.ToString();
                if (roleName == "ADMIN")
                {*/
                    var Users = (from user in context.Users.ToList()
                                        select new
                                        {
                                            Id = user.Id,
                                            FirstName = user.FirstName,
                                            LastName = user.LastName,
                                            Surname = user.SurName,
                                            UserName = user.UserName,
                                            Role = user.RoleName,
                                        }).AsEnumerable();

                            return Ok(Users);
                /* }
                else
                {
                    return new ResponseMessageResult(Request.CreateErrorResponse((HttpStatusCode)777, new HttpError("You are not an ADMIN")));
                }
            }

            else
            {
                var Users = (from user in context.Users.ToList()
                             select new
                             {
                                 Id = user.Id,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Surname = user.SurName,
                                 UserName = user.UserName,
                                 Role = user.RoleName,
                             }).AsEnumerable();

                return Ok(Users);
            }*/
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        public IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}