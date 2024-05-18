using API_Game_Server.DataContext;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Game_Server.Models;

[Table("UserInfo")]
public class UserInfo
{
    [Key]
    public int UserId { get; set; }

    public int AccountId { get; set; }
    [Required]
    [StringLength(20)]
    public string NickName { get; set; }
    public int Level { get; set; }
    public int ActivePoint { get; set; }
    public int Exp {  get; set; }
    public int Power { get; set; }
    public int Gem { get; set; }
    public int Gold { get; set; }
    public DateTime CreatedAt { get; set; }

    [ForeignKey("AccountId")]
    public Account Account { get; set; }
}
