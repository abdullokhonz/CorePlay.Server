using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CorePlay.Enums;

namespace CorePlay.Entity;

public class GameEntity
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
    
    [ForeignKey(nameof(DeveloperId))]
    public UserEntity Dev { get; set; } = null!;
}



