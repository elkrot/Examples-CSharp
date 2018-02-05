using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationDemo
{
    public class AuthenticationSimulator
    {
        readonly Func<IDictionary<string, object>, Task> _next;

        public AuthenticationSimulator(Func<IDictionary<string, object>, Task> next)
        {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            var context = new OwinContext(env);
            context.Authentication.User = CreateUser();

            await _next(env);
        }

        private ClaimsPrincipal CreateUser()
        {
            var claims = new List<Claim>
            {
                new Claim(Constants.ClaimTypes.Subject, "12345"),
                new Claim(Constants.ClaimTypes.Role, "Geek"),
                new Claim(Constants.ClaimTypes.ClientId, "desktop"),
                new Claim(Constants.ClaimTypes.Scope, "read"),
                new Claim(Constants.ClaimTypes.Scope, "add"),

            };

            var id = new ClaimsIdentity("Application", Constants.ClaimTypes.Subject, Constants.ClaimTypes.Role);
            id.AddClaims(claims);

            return new ClaimsPrincipal(id);
        }
    }
}