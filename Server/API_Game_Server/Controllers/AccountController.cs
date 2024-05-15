using API_Game_Server.Models;
using API_Game_Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Game_Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    AccountService _account;

    public AccountController(AccountService account)
    {
        _account = account;
    }

    [HttpPost]
    [Route("login/facebook")]
    public async Task<LoginFacebookRes> LoginAccount([FromBody] LoginFacebookReq req)
    {
        LoginFacebookRes res = new LoginFacebookRes();

        string jwtToken = await _account.LoginFacebookAccount(req.Token);
        if(string.IsNullOrEmpty(jwtToken))
        {
            res.LoginOk = false;
            return res;
        }

        res.LoginOk = true;
        res.JwtAccessToken = jwtToken;

        return res;
    }
}
