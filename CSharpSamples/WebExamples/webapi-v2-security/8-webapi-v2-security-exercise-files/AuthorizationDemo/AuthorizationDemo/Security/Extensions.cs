using Thinktecture.IdentityModel;

namespace System.Net.Http
{
    public static class Extensions
    {
        public static bool CheckAccess(this HttpRequestMessage request, string action, params string[] resources)
        {
            return ClaimsAuthorization.CheckAccess(request.GetRequestContext().Principal, action, resources);
        }
    }
}