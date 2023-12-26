using UserService.Data;
using UserService.Model;

namespace UserService.Service
{
    public interface ISetUp
    {
        RefreshToken Add(RefreshToken token);
        void update(RefreshToken token);
        RefreshToken getByToken(string token);
        User getById(string id);

    }
}
