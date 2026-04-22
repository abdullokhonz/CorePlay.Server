using CorePlay.Dtos;
using CorePlay.Data;
using CorePlay.Entity;
using CorePlay.implimintations.interfaces;
using Microsoft.EntityFrameworkCore;

namespace CorePlay.implimintations.Services;

public class GameServer : IGame
{
    private readonly AppDbContext _db;

    public GameServer(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<GameDto>> GetAsync(GameQuaryDto dto)
    {
        var query = _db.Games.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(dto.Name))
            query = query.Where(x => EF.Functions.Like(x.Name, $"%{dto.Name}%"));

        if (dto.GameId != 0)
            query = query.Where(x => x.GameId == dto.GameId);

        if (!string.IsNullOrWhiteSpace(dto.Description))
            query = query.Where(x => EF.Functions.Like(x.Description, $"%{dto.Description}%"));

        if (dto.Genre != 0)
            query = query.Where(x => x.Genre == dto.Genre);

        if (dto.MinPrice != 0)
            query = query.Where(x => x.Price >= dto.MinPrice);

        if (dto.MaxPrice != 0)
            query = query.Where(x => x.Price <= dto.MaxPrice);

        if (!string.IsNullOrWhiteSpace(dto.IconPath))
            query = query.Where(x => EF.Functions.Like(x.IconPath, $"%{dto.IconPath}%"));

        if (dto.DeveloperId > 0)
            query = query.Where(x => x.DeveloperId == dto.DeveloperId);

        var pageNumber = dto.PageNumber < 1 ? 1 : dto.PageNumber;

        return await query
            .OrderBy(x => x.GameId)
            .Skip((pageNumber - 1) * 20)
            .Take(20)
            .Select(x => new GameDto
            {
                GameId = x.GameId,
                Name = x.Name,
                Description = x.Description,
                Genre = x.Genre,
                Price = x.Price,
                IconPath = x.IconPath,
                DeveloperId = x.DeveloperId
            })
            .ToListAsync();
    }

    public async Task<GameDto?> CreateAsync(GameCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) ||
            string.IsNullOrWhiteSpace(dto.Description) ||
            dto.Genre == 0 ||
            dto.Price == 0 ||
            string.IsNullOrWhiteSpace(dto.IconPath))
        {
            return null;
        }

        var developerExists = await _db.Users.AnyAsync(x => x.UserId == dto.DeveloperId);
        if (!developerExists)
            return null;

        var gameExists = await _db.Games.AnyAsync(x => x.Name == dto.Name);
        if (gameExists)
            return null;

        var newGame = new GameEntity
        {
            Name = dto.Name,
            Description = dto.Description,
            Genre = dto.Genre,
            Price = dto.Price,
            IconPath = dto.IconPath,
            DeveloperId = dto.DeveloperId
        };

        _db.Games.Add(newGame);
        await _db.SaveChangesAsync();

        return ToDto(newGame);
    }

    public async Task<GameDto?> UpdateAsync(int id, GameUpdateDto dto)
    {
        var game = await _db.Games.SingleOrDefaultAsync(x => x.GameId == id);

        if (game == null)
            return null;

        if (!string.IsNullOrWhiteSpace(dto.Name))
            game.Name = dto.Name;

        if (!string.IsNullOrWhiteSpace(dto.Description))
            game.Description = dto.Description;

        if (dto.Genre != 0)
            game.Genre = dto.Genre;

        if (dto.Price != 0)
            game.Price = dto.Price;

        if (!string.IsNullOrWhiteSpace(dto.IconPath))
            game.IconPath = dto.IconPath;

        await _db.SaveChangesAsync();

        return ToDto(game);
    }

    public async Task<GameDto?> DeleteAsync(int id)
    {
        var game = await _db.Games.SingleOrDefaultAsync(x => x.GameId == id);

        if (game == null)
            return null;

        _db.Games.Remove(game);
        await _db.SaveChangesAsync();

        return ToDto(game);
    }

    private static GameDto ToDto(GameEntity game)
    {
        return new GameDto
        {
            GameId = game.GameId,
            Name = game.Name,
            Description = game.Description,
            Genre = game.Genre,
            Price = game.Price,
            IconPath = game.IconPath,
            DeveloperId = game.DeveloperId
        };
    }
}