using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UserService.Data;
using UserService.Service;

namespace UserService.Model
{
    public class SupportClass
    {
        /*private readonly SetUpRepository _repository;
        public SupportClass() { }
        public SupportClass(SetUpRepository repository)
        {
            _context = context;
        }
        public async Task<TokenModel> GenerateToken(User u,string secretKey)
        {

            var jwtToken = new JwtSecurityTokenHandler();
            var secretKeyByte = Encoding.UTF8.GetBytes(secretKey);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("Id",u.Id),
                        new Claim(JwtRegisteredClaimNames.Email,u.Email),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

                    }),
                Expires = DateTime.UtcNow.AddSeconds(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyByte), SecurityAlgorithms.HmacSha512Signature)

            };

            var token = jwtToken.CreateToken(tokenDescription);
            var accessToken = jwtToken.WriteToken(token);
            var refreshToken = GenerateRefreshToken();
            var refToken = new RefreshToken {
                id = Guid.NewGuid().ToString(),
                jwt = token.Id,
                UserId = u.Id,
                Token = refreshToken,
                isUse =false,
                isRevoked = false,
                CreatedAt = DateTime.UtcNow,
                ExpireAt = DateTime.UtcNow.AddHours(1),
            };
            await _context.RefreshTokens.AddAsync(refToken);
            await _context.SaveChangesAsync();
            return new TokenModel
            {
                AccessToken = accessToken,

            };

        }

        public async Task<int>   GenerateRefreshToken(TokenModel model,string secretKey)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            var secretKeyByte = Encoding.UTF8.GetBytes(secretKey);
            var param= new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte),

                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false,   
            };
            try
            {
                // check 1: AccessToken valid format
                var tokenVerification = jwtToken.ValidateToken(model.AccessToken, param,out var validatedToken);
                // check 2: alg
                if(validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature,
                        StringComparison.InvariantCultureIgnoreCase);
                    if(!result)
                    {
                        return -1;
                    }
                }
                // check expire
                var utcExpire = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                var expDate = ConvertToDate(utcExpire);
                if(expDate>DateTime.UtcNow)
                {
                    return -1;
                }
                // check refresh exist
                var storedToken = _context.RefreshTokens.FirstOrDefault(x => x.Token == model.RefreshToken);
                if (storedToken == null)
                {
                    return -1;
                }
                // check refresh token is use/revoked
                if (storedToken.isUse)
                {
                    return -1;
                }
                if (storedToken.isRevoked)
                {
                    return -1;
                }
                // check accessToken id 
                var jti=tokenVerification.Claims.FirstOrDefault(x=>x.Type == JwtRegisteredClaimNames.Jti).Value;
                if(storedToken.jwt!=jti)
                {
                    return -1;
                }
                // update token
                storedToken.isUse= true;
                storedToken.isRevoked= true;
                _context.Update(storedToken);
                await _context.SaveChangesAsync();
                // create Token
                var usId = storedToken.UserId;
                var us = await _context.Users.SingleOrDefaultAsync(x=>x.Id == usId);
                var token = await GenerateToken(us, secretKey);

                return 0;
            }
            catch(Exception e)
            {
                return -1;
            }
        }

        private DateTime ConvertToDate(long utcExpire)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpire).ToUniversalTime();
            return dateTimeInterval;
        }

        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(random);
            }
            return Convert.ToBase64String(random);
        }*/
    }
}
