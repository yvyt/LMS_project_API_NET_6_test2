using UserService.Model;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        List<UserVM> getAll();
        UserVM getById(string id);
        UserVM Add(UserVM user);
        void Update(UserVM user); 
        void Delete(string id);
        UserVM Validate(UserLogin model);
    }
}
