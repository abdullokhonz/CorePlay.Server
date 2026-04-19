using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CorePlay.Enums;

namespace CorePlay.Entity;

public class UserEntity
{
    [Required]
    [RegularExpression("^[a-zA-Z0-9]*$")]
    public string UserName { get; set; }

    [Required] 
    [EmailAddress] 
    public string Email { get; set; }
    
    [Required]
    [PasswordPropertyText]
    public string PasswordHash { get; set; }
    
    [Key]
    public int UserId { get; set; }
    
    
}