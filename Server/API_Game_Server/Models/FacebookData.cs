namespace API_Game_Server.Models;

public class FacebookResponseJsonData
{
    public FacebookTokenData data {  get; set; }
}

public class FacebookTokenData
{
    public long app_id { get; set; }
    public string application {  get; set; }
    public long expires_at { get; set; }
    public bool is_valid { get; set; }
    public string user_id { get; set; }
}