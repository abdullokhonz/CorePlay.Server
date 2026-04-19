using System.ComponentModel.DataAnnotations;
using CorePlay.Enums;

namespace CorePlay.Dtos;

public class UserQuaryDtos
{
    public string? UserName { get; set; }
    
    public string? Email { get; set; }
    
    public int? UserId { get; set; }

    [Required]
    public int PageNumber { get; set; } 
}
