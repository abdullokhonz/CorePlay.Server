using CorePlay.Dtos;

namespace CorePlay.implimintations.interfaces;

public interface IGame
{
    Task<IEnumerable<GameDto>> GetAsync(GameQuaryDto dto);
    
    Task<GameDto?> CreateAsync(GameCreateDto dto);
    
    Task<GameDto?> UpdateAsync(int id, GameUpdateDto dto);
    
    Task<GameDto?> DeleteAsync(int id);
}