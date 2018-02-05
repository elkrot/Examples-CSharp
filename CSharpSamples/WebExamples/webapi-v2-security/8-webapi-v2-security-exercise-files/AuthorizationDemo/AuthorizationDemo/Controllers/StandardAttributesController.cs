using System.Web.Http;

namespace AuthorizationDemo.Controllers
{
    [Authorize(Roles = "SomeRole")]
    public class StandardAttributesController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [AllowAnonymous]
        public IHttpActionResult Post()
        {
            return Ok();
        }

        [Authorize(Roles = "SomeAdditionalRole")]
        public IHttpActionResult Put()
        {
            return Ok();
        }

        [OverrideAuthorization]
        [Authorize(Roles = "SomeOtherRole")]
        public IHttpActionResult Delete()
        {
            return Ok();
        }
    }
}