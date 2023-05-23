using Virtual_Wardrobe_Management_System.Data_Layer.Entities.Authentication___Authorization;
using Virtual_Wardrobe_Management_System.Data_Layer.Repositories;

namespace Virtual_Wardrobe_Management_System.Business_Logic.RepositoryInterfaces

{
    public interface IUserRepository
    {
        Users GetByEmail(string email);
        Users AddUser(Users user);
        void SignUp(Users user);
        Users Login(LoginRequest loginRequest);
        string HashPassword(string password);
    }
}
