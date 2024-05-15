using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_Game_Server.DataContext;

public enum ProviderType
{
    Facebook = 1,
    Google = 2,
}

[Table("Account")]
public class Account
{
    public int Id { get; set; }

    [Required]
    public string LoginProviderUserId { get; set; }
    [Required]
    public ProviderType LoginProviderType { get; set; }
}
