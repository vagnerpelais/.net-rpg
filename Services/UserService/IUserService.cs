namespace net_rpg.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<GetUserDto>> GetUserById(int id);
        Task<ServiceResponse<List<GetUserDto>>> AddUser(RequestUserDto newCharacter);
        Task<ServiceResponse<GetUserDto>> UpdateUser(RequestUserDto newCharacter, int id);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);
        Task<ServiceResponse<string>> VerifyUserLogin(RequestUserDto request);
    }
}