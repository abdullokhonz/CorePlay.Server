using System.ComponentModel.DataAnnotations;
using CorePlay.Enums;

namespace CorePlay.Dtos;

public class GameQuaryDto
{
    public string Name { get; set; }
    

    public int GameId { get; set; }
    

    public string Description { get; set; }
    

    public Genre Genre { get; set; }
    
    [Range(0.1, 50)]
    public decimal MinPrice { get; set; }
    
    [Range(50, 250)]
    public decimal MaxPrice { get; set; }

    public string IconPath { get; set; }
    

    public int DeveloperId  { get; set; } 
    
    [Required]
    public int PageNumber { get; set; } 
}