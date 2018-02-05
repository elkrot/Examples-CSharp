using System;
using System.Net.Http;
using Thinktecture.IdentityModel.Client;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var response = GetToken();
            CallService(response.AccessToken);
        }

        private static TokenResponse GetToken()
        {
            var client = new OAuth2Client(
                new Uri("http://localhost:2727/token"));

            return client.RequestResourceOwnerPasswordAsync("bob", "bob").Result;
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