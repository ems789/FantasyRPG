namespace API_Game_Server.Models;

public class LoginFacebookReq
{
    public string Token { get; set; }
}

public class LoginFacebookRes
{
    public bool LoginOk { get; set; }
    public string JwtAccessToken { get; set; }
}
 