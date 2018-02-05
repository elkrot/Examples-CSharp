/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license.txt
 */

using Thinktecture.IdentityModel.Owin;

namespace Owin
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UseRequireTls(this IAppBuilder app, bool requireClientCertificate = false)
        {
            app.Use(typeof(RequireTlsMiddleware), new RequireTlsOptions { RequireClientCertificate = requireClientCertificate });
            return app;
        }
    }
}
