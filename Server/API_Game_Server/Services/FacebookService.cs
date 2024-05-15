using Newtonsoft.Json;
using API_Game_Server.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace API_Game_Server.Services;

public class FacebookService
{
    HttpClient _httpClient;
    IConfiguration _config;
    readonly string _accessToken; // TODO Secret

    public FacebookService(IConfiguration config)
    {
        _config = config;

        _httpClient = new HttpClient() { BaseAddress = new Uri("https://graph.facebook.com/") };
        _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        _accessToken = _config["Facebook:AccessToken"];
    }

    public async Task<FacebookTokenData> GetUserTokenData(string inputToken)
    {
        HttpResponseMessage response =  await _httpClient.GetAsync($"debug_token?input_token={inputToken}&access_token={_accessToken}");

        if(!response.IsSuccessStatusCode)
        {
            return null; 
        }

        string resultStr = await response.Content.ReadAsStringAsync();

        FacebookResponseJsonData result = JsonConvert.DeserializeObject<FacebookResponseJsonData>(resultStr);
        
        return result.data;
    }
}
