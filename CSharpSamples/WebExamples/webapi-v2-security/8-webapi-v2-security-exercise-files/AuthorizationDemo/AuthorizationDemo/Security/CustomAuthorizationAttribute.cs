using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace AuthorizationDemo
{
    public class CustomAuthorizationAttribute : AuthorizeAttribute
    {
        bool outcome = false;

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            // retrieve principal and check authZ
            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            return outcome;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var response =
                 actionContext.Request.CreateErrorResponse(
                   HttpStatusCode.Unauthorized, "unauthorized");

            response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue("Bearer", "permission required."));

            actionContext.Response = response;
        }
    }
}