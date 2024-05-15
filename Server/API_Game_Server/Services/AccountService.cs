using API_Game_Server.DataContext;
using API_Game_Server.Models;

namespace API_Game_Server.Services;

public class AccountService
{
    AppDbContext _context;
    FacebookService _fb;
    JwtTokenService _token;

    public AccountService(AppDbContext context, FacebookService fb, JwtTokenService token)
    {
        _context = context;
        _fb = fb;
        _token = token;
    }

    public async Task<string> LoginFacebookAccount(string token)
    {
        FacebookTokenData tokenData = await _fb.GetUserTokenData(token);
        if(tokenData == null || tokenData.is_valid == false)
        {
            return null;
        }

        Account accountDb = _context.Accounts.FirstOrDefault(
            a => a.LoginProviderUserId == tokenData.user_id
            && a.LoginProviderType == ProviderType.Facebook);

        if(accountDb == null)
        {
            accountDb = new Account()
            {
                LoginProviderUserId = tokenData.user_id,
                LoginProviderType = ProviderType.Facebook
            };

            _context.Accounts.Add(accountDb);
            await _context.SaveChangesAsync();
        }

        string jwtToken = _token.CreateJwtAccessToken(accountDb.Id);

        return jwtToken;
    }
}
