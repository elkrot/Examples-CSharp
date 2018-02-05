using System.Security.Claims;
using System.Web.Mvc;
using Thinktecture.Samples;

namespace WebHost.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Web API security sample.";

            return View();
        }

        [Authorize]
        public ActionResult IdentityMvc()
        {
            var principal = ClaimsPrincipal.Current;
            return View(ViewClaims.GetAll(principal));
        }

        [Authorize]
        public ActionResult IdentityApi()
        {
            return View();
        }
    }
}
