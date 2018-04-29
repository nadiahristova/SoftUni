using Client.DataTransferObjects;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string routePrefix = @"http://localhost:62858";
        private static string tokenForCurrUser;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string commandText = this.request.Text;
            var data = Regex.Split(commandText.Trim(), @"\s+");
            var response = string.Empty;

            try
            {
                response = await ReturnRequestResponseAsync(data);
            }
            catch(HttpRequestException)
            {
                response += "Please turn Battleships Web API on and try again. (:\n";
            }
            catch(FormatException fex)
            {
                response += fex.Message + " Coordinates must be integers. Please try again.\n";
            }

            this.visualizeRequest.Text += response;
        }

        private async Task<string> ReturnRequestResponseAsync(string[] data)
        {
            int startIndex = 0;
            string response = null;

            if (data[1] == @"$")
                startIndex = 2;
            else if (data[0] == @"$")
                startIndex = 1;

            switch (data[startIndex].ToLower())
            {
                case "register":
                    {
                        if (data[startIndex + 2] == data[startIndex + 3])
                        {
                            string status = await RegisterUserPostAsync(data[startIndex + 1], data[startIndex + 2]);
                            response = "Attempt to register User " + data[startIndex + 1] + " ...\n" + status + "\n\n";
                        }                            
                        else response = "Password and Confirm Password Fields do not match. \nPlease try again.\n";
                    } break;
                case "login":
                    {
                        response += LoginUser(data[startIndex + 1], data[startIndex + 2]);
                    } break;
                case "create-game":
                    {
                        response += await CreateGameAsync();
                    } break;
                case "join-game":
                    {
                        response += await JoinGameAsync(data[startIndex + 1]);
                    } break;
                case "play":
                    {
                        response += await PlayAsync(data[startIndex + 1], int.Parse(data[startIndex+2]), int.Parse(data[startIndex+3]));
                    } break;
                default: response = "Unknown command. \nPlease try again. \n"; break;
            }

            
            return response;
        }

        private async Task<string> PlayAsync(string gameId, int positionX, int positionY)
        {
            string joinInEndpoint = routePrefix + @"/games/play";
            string result = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenForCurrUser);

                var bodyData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("GameId", gameId),
                    new KeyValuePair<string, string>("PositionX", positionX.ToString()),
                    new KeyValuePair<string, string>("PositionY", positionY.ToString())
                });

                var response = await httpClient.PostAsync(joinInEndpoint, bodyData);
                var content = response.Content;

                if (!response.IsSuccessStatusCode)
                    result = response.StatusCode + " : " + await response.Content.ReadAsStringAsync() + "\n";
                else
                    result = "You have successfully joined the game " + await response.Content.ReadAsStringAsync() + ".\n";
            }

            return result;
        }

        private async Task<string> JoinGameAsync(string gameGuid)
        {
            string joinInEndpoint = routePrefix + @"/games/join";
            string result = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenForCurrUser);

                var builder = new UriBuilder(joinInEndpoint);
                var query = HttpUtility.ParseQueryString(string.Empty);
                query["GameId"] = gameGuid;
                builder.Query = query.ToString();
                
                var response = await httpClient.PostAsync(builder.ToString(), null);
                var content = response.Content;

                if (!response.IsSuccessStatusCode)
                    if (response.StatusCode.ToString() == "InternalServerError")
                        result = response.StatusCode + "\nIn order this command to work u need to modify GamesController JoinGame action to accept paramethers From Uri. \nExample: public IHttpActionResult JoinGame([FromUri]JoinGameBindingModel model)\n";                
                    else                    
                    result = response.StatusCode + " New Game " + await response.Content.ReadAsStringAsync() + " created successfully.\n";
            }

            return result;
        }

        private async Task<string> CreateGameAsync()
        {
            string createGameEndpoint = routePrefix + @"/games/create";
            string result = null;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + tokenForCurrUser);
                var response = await httpClient.PostAsync(createGameEndpoint, null);
                var content = response.Content;

                if (!response.IsSuccessStatusCode)
                    result = response.StatusCode + " : " + await response.Content.ReadAsStringAsync() + "\n";
                else
                    result = response.StatusCode + " New Game " + await response.Content.ReadAsStringAsync() + " created successfully.\n";
            }

            return result;
        }

        private string LoginUser(string userName, string password)
        {
            string registerEndpointForToken = routePrefix + @"/Token";

            string result = null;

            using (var httpClient = new HttpClient())
            {
                var bodyData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("Password", password),
                    new KeyValuePair<string, string>("username", userName)
                });

                var response = httpClient.PostAsync(registerEndpointForToken, bodyData).Result;
                var content = response.Content;

                if (!response.IsSuccessStatusCode)
                    result = response.StatusCode + " : " + response.Content.ReadAsStringAsync().Result + "\n";
                else
                    result = "Login was successful. Do your thing.\n";

                var toki = response.Content.ReadAsAsync<TokenFetcher>().Result;

                tokenForCurrUser = toki.access_token;
            }

            return result;
        }

        private async Task<string> RegisterUserPostAsync(string userName, string password)
        {
            string registerEndpoint = routePrefix + @"/api/Account/Register";
            string result = null;

            using (var httpClient = new HttpClient())
            {
                 var bodyData = new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("Email", userName),
                    new KeyValuePair<string, string>("Password", password),
                    new KeyValuePair<string, string>("ConfirmPassword", password)
                });

                var response = await httpClient.PostAsync(registerEndpoint, bodyData);
                var content = response.Content;
                
                if (!response.IsSuccessStatusCode)
                    result = response.StatusCode + " : " + await content.ReadAsStringAsync() + "\n";
                else
                    result = "User " + userName + "was successfully added to the database.\n";                
            }

            return result;
        }
    }
}
