using Microsoft.Live;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Security.Claims;
using Thinktecture.AuthorizationServer.Interfaces;
using Thinktecture.AuthorizationServer.Models;
using Thinktecture.AuthorizationServer.OAuth2;

namespace Thinktecture.Samples
{
    public class AssertionGrantValidator : IAssertionGrantValidation
    {
        public const string MsaIdentityToken = "urn:msaidentitytoken";

        public ClaimsPrincipal ValidateAssertion(ValidatedRequest validatedRequest)
        {
            if (validatedRequest.AssertionType == MsaIdentityToken)
            {
                var appId = "";
                var appSecret = "";
                var audience = "";

                var authClient = new LiveAuthClient(
                       appId,
                       appSecret,
                       audience);

                var msaId = authClient.GetUserId(validatedRequest.Assertion);
                var id = new ClaimsIdentity("MSA");
                id.AddClaim(new Claim(ClaimTypes.NameIdentifier, msaId));

                return FederatedAuthentication.FederationConfiguration
                                              .IdentityConfiguration
                                              .ClaimsAuthenticationManager
                                              .Authenticate(
                    "AssertionValidation", 
                    new ClaimsPrincipal(id));
            }

            return null;
        }
    }
}