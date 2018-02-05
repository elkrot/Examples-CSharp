using System.Web.Http;
using Thinktecture.IdentityModel;
using Thinktecture.IdentityModel.WebApi;
using System.Net.Http;

namespace AuthorizationDemo.Controllers
{
    //[CustomAuthorization]
    [ScopeAuthorize("add")]
    public class CustomAttributeController : ApiController
    {
        //[ResourceActionAuthorize("read", "data")]
        public IHttpActionResult Get()
        {
            if (!Request.CheckAccess("read", "data", "id"))
            {
                return Unauthorized();
            }

            return Ok();
        }
    }
}