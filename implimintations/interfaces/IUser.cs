using CorePlay.Dtos;

namespace CorePlay.implimintations.interfaces;

public interface IUser
{
    Task<IEnumerable<UserDtos>> GetAsync(UserQuaryDtos user);
    
    Task<UserDtos?> CreateAsync(UserCreateDro User);

    Task<UserDtos?> UpdateAsync(UserUpdateDto User);
    
    Task<UserDtos?> DeleteAsync(int id);
}