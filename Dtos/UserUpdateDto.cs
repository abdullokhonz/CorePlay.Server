using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CorePlay.Dtos;

public class UserUpdateDto
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
}