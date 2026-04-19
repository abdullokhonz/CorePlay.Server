using System.ComponentModel.DataAnnotations;
using CorePlay.Enums;

namespace CorePlay.Dtos;

public class UserDtos
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public int UserId { get; set; }
    
}