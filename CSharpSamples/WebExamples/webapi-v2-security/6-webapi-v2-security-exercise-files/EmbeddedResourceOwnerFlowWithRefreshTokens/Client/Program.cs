using System;
using System.Net.Http;
using System.Threading;
using Thinktecture.IdentityModel.Client;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var response = GetToken();
            CallService(response.AccessToken);

            var refreshResponse = RefreshToken(response.RefreshToken);
            CallService(refreshResponse.AccessToken);

            refreshResponse = RefreshToken(refreshResponse.RefreshToken);
            CallService(refreshResponse.AccessToken);
        }

        private static TokenResponse GetToken()
        {
            var client = new OAuth2Client(
                new Uri("http://localhost:2727/token"),
                "client1",
                "secret");

            var response = client.RequestResourceOwnerPasswordAsync("bob", "bob").Result;
            return response;
        }

        private static TokenResponse RefreshToken(string refreshToken)
        {
            var client = new OAuth2Client(
                new Uri("http://localhost:2727/token"),
                "client1",
                "secret");

            var response = client.RequestRefreshTokenAsync(refreshToken).Result;
            return response;
        }

        private static void CallService(string token)
        {
            var client = new HttpClient();
            client.SetBearerToken(token);
            var response = client.GetStringAsync(new Uri("http://localhost:2727/api/identity")).Result;

            Console.WriteLine(response);
        }
    }
}
