using Api.BizService;
using Api.Models;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Api.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        // POST: api/User
        public Userdetails Post([FromBody] Authenticate authenticate)
        {
            UserService userService = new UserService();
            var isexists = userService.Autheticate(authenticate);
            return isexists;
        }
    }
}
