using CorePlay.Data;
using CorePlay.implimintations.interfaces;
using CorePlay.Dtos;
using CorePlay.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CorePlay.implimintations.Services;

public class UserService : IUser
{
    private readonly AppDbContext _db;
    private readonly IPasswordHasher<UserEntity> _passwordHasher;
    
    public UserService(AppDbContext db, IPasswordHasher<UserEntity> passwordHasher)
    {
        _db = db;
        _passwordHasher = passwordHasher;
    }

    public async Task<IEnumerable<UserDtos>> GetAsync(UserQuaryDtos user)
    {
        var userquary = _db.Users.AsNoTracking();

        if (user.UserId >= 1) 
            userquary = userquary.Where(x => x.UserId >= user.UserId);

        if (!string.IsNullOrWhiteSpace(user.Email))
            userquary = userquary.Where(x => x.Email == user.Email);

        if (user.Role != 0)
            userquary = userquary.Where(x => x.Role == user.Role);

        if (!string.IsNullOrWhiteSpace(user.UserName))
            userquary = userquary.Where(x => x.UserName == user.UserName);
        
        return await userquary
            .Skip((user.PageNumber - 1) * 20)
            .Take(20)
            .Select(x=> new UserDtos
            {
                UserName = x.UserName,
                Email = x.Email,
                Role = x.Role,
            }).ToListAsync();
    }

    public async Task<UserDtos?> CreateAsync(UserCreateDro User)
    {
        
        if (await _db.Users.AnyAsync(x => x.UserName == User.UserName || x.Email == User.Email))
            return null;

        var newuser = new UserEntity
        {
            UserName = User.UserName,
            Email = User.Email,
            Role = User.Role,
            UserId = User.UserId
        };
        
        newuser.PasswordHash = _passwordHasher.HashPassword(newuser, User.PasswordHash);

        _db.Users.Add(newuser);
        await _db.SaveChangesAsync();
        
        return new UserDtos{UserId = newuser.UserId, UserName = newuser.UserName, Email = newuser.Email, Role = newuser.Role};
    }

    public async Task<UserDtos?> UpdateAsync(UserUpdateDto User)
    {
        var entity = await _db.Users.FirstOrDefaultAsync(x => x.UserId == User.);

        if (entity == null)
            return null;
    }

    public async Task<UserDtos?> DeleteAsync(int id)
    {
        var res = await _db.Users.FirstOrDefaultAsync(x => x.UserId == id);
        
        if (res == null) return null;

        _db.Users.Remove(res);
        await _db.SaveChangesAsync();
        
        return new UserDtos{UserId = id, UserName = res.UserName, Email = res.Email, Role = res.Role};
    }
}