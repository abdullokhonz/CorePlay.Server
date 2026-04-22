using System.ComponentModel.DataAnnotations;
using CorePlay.Enums;

namespace CorePlay.Dtos;

public class GameCreateDto
{
    [Required]
    public string Name { get; set; }
    
    [Key]
    public int GameId { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public Genre Genre { get; set; }
    
    [Required]
    [Range(0.1, 250)]
    public decimal Price { get; set; }
    
    [Required]
    public string IconPath { get; set; }
    
    [Required]
    public int DeveloperId  { get; set; } 
}