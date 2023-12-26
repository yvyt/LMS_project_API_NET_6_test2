using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserService.Data;
using UserService.Model;

namespace UserService.Service
{
    public class SetUpRepository : ISetUp
    {
        private readonly UserDbContext _context;
        public SetUpRepository(UserDbContext context)
        {
            _context = context;
        }
        public RefreshToken Add(RefreshToken token)
        {
            _context.RefreshTokens.Add(token);
            _context.SaveChanges();
            return token;
        }

        public User getById(string id)
        {
            var user = _context.Users.SingleOrDefault(user => user.Id == id);
            if(user == null)
            {
                return null;
            }
            return user;
        }

        public RefreshToken getByToken(string token)
        {
            var re = _context.RefreshTokens.FirstOrDefault(x => x.Token == token);
            if (re == null)
            {
                return null;
            }
            return re;
        }

        public void update(RefreshToken token)
        {
            var tokenEdit = _context.RefreshTokens.SingleOrDefault(u => u.id == token.id);
            tokenEdit.isUse = true;
            tokenEdit.isRevoked = true;
            _context.RefreshTokens.Update(token);
            _context.SaveChanges();
        }
    }
}
