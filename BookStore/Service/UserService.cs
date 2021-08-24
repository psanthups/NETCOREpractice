using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class UserService : IUserService                                                                                                              /*we created this class to write common code to handle the userId in this app. we can use this code in services & controllers(anywhere in app)*/
    {                                                                                                                                                     /*so we can use httpcontext in this cls by injecting service into this constuctor & also in startup cls then we can use this anywhere as i said above */
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetUserId()                                                                                                                          /*we wrote this method to get the logged user id into the controller(in index actionmethod)*/
        {
            return _httpContext.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool IsAuthenticated()                                                                                                                        /*we wrote this method to check the user is logged in or not*/
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
