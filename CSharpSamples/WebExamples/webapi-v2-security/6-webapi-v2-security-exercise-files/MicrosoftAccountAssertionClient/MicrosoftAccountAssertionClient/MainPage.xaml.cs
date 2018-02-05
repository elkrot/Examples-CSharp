using Microsoft.Live;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Thinktecture.IdentityModel.Client;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MicrosoftAccountAssertionClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        LiveLoginResult _loginResult;
        TokenResponse _asTokenResponse;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            var scopes = new string[] { "wl.signin" };
            var authClient = new LiveAuthClient("http://www.thinktecture.com");

            // try silent logon first
            _loginResult = await authClient.InitializeAsync(scopes);

            if (_loginResult.Status != LiveConnectSessionStatus.Connected)
            {
                // need to (re) prompt
                _loginResult = await authClient.LoginAsync(scopes);

                if (_loginResult.Status != LiveConnectSessionStatus.Connected)
                {
                    await new MessageDialog("Access denied!").ShowAsync();
                    return;
                }
            }

            AccountImage.Source = new BitmapImage(new Uri("https://apis.live.net/v5.0/me/picture?access_token=" + _loginResult.Session.AccessToken));

            Output1Label.Text = "Microsoft Account access token";
            Output1.Text = _loginResult.Session.AccessToken;

            Output2Label.Text = "Microsoft Account authentication token";
            Output2.Text = _loginResult.Session.AuthenticationToken;
        }

        private async void DoAssertionFlowButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new OAuth2Client(
                new Uri("https://as.local/users/oauth/token"),
                "assertionclient",
                "secret");

            _asTokenResponse = await client.RequestAssertionAsync(
                "urn:msaidentitytoken",
                _loginResult.Session.AuthenticationToken,
                "read");

            Output1Label.Text = "AuthorizationServer token response";
            Output1.Text = _asTokenResponse.Json.ToString();

            Output2Label.Text = "";
            Output2.Text = "";
        }

        private async void CallResourceServer_Click(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient { 
                BaseAddress = new Uri("https://web.local/rs2/") };

            client.SetBearerToken(_asTokenResponse.AccessToken);
            var response = await client.GetStringAsync("api/identity");

            Output2Label.Text = "Resource server response";
            Output2.Text = JArray.Parse(response).ToString();
        }
    }
}
