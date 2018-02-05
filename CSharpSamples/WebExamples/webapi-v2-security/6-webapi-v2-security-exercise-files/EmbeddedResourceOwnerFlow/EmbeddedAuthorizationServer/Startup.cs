using EmbeddedAuthorizationServer.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;

[assembly: OwinStartup(typeof(EmbeddedAuthorizationServer.Startup))]

namespace EmbeddedAuthorizationServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // token generation
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
                {
                    // for demo purposes
                    AllowInsecureHttp = true,

                    TokenEndpointPath = new PathString("/token"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromHours(8),

                    Provider = new SimpleAuthorizationServerProvider()
                });

            // token consumption
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
                
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}