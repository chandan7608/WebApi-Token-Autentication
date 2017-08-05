using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace WebApiTokenAuthentication.Controllers
{
    /* Steps for getting acess token from server using postman
      Select POST method, Enter this URL http://localhost:/apikey and then click on body and select select x-www-form-urlencoded and then enter 3 parameter,
      1. username (value : user) 
      2. password (value: user) and 
      3. grant_type (value: password) and then click on  send button. After click on send button we will get 200 OK and access token 

    */

    public class ProductController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/forall")]
        //Getting data from the server for all anonymous user.All type of request, whether it is authenticated or not can access this action.
        public IHttpActionResult Get()
        {
            return Ok("Now server time is: " + DateTime.Now.ToString());
        }


        //Getting data from the server for all authenticated user, whether it is Admin user or normal user.
        //Getting access token for accessing method
        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public IHttpActionResult GetForAuthenticate()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello " + identity.Name);
        }

        //Getting data from the server only for Admin role type user.
        //Getting access token for accessing method
        [Authorize(Roles = "hr")]
        [HttpGet]
        [Route("api/data/authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value);
            return Ok("Hello " + identity.Name + " Role: " + string.Join(",", roles.ToList()));
        }
    }
}
