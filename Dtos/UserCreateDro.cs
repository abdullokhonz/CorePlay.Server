using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CorePlay.Enums;

namespace CorePlay.Dtos;

public class UserCreateDro
{
    [Required]
    public string UserName { get; set; }

    [Required] 
    [EmailAddress] 
    public string Email { get; set; }
    
    [Required]
    [PasswordPropertyText]
    public string PasswordHash { get; set; }
    
}