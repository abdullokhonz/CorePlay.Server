using System.ComponentModel.DataAnnotations;
using CorePlay.Enums;

namespace CorePlay.Dtos;

public class GameUpdateDto
{

    public string Name { get; set; }
    
    public string Description { get; set; }

    public Genre Genre { get; set; }

    [Range(0.1, 250)]
    public decimal Price { get; set; }
    
    public string IconPath { get; set; }
}