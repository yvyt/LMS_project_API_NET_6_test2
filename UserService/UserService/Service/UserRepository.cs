using System.ComponentModel.DataAnnotations.Schema;
using UserService.Data;
using UserService.Model;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context) {
            _context=context;
        }
        public UserVM Add(UserVM user)
        {
            var userNew = new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Gender = user.Gender,
                Phone = user.Phone,
                DateOfBirth = user.DateOfBirth,
                Avatar = user.Avatar,
                IsFirstLogin = user.IsFirstLogin,
                Address = user.Address,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };
            _context.Users.Add(userNew);
            _context.SaveChanges();
            return new UserVM
            {
                Id = userNew.Id,
                Email = userNew.Email,
                FirstName = userNew.FirstName,
                LastName = userNew.LastName,
                Password = userNew.Password,
                Gender = userNew.Gender,
                Phone = userNew.Phone,
                DateOfBirth = userNew.DateOfBirth,
                Avatar = userNew.Avatar,
                IsFirstLogin = userNew.IsFirstLogin,
                Address = userNew.Address,
                IsActive = userNew.IsActive,
                CreatedAt = userNew.CreatedAt,
                UpdatedAt = userNew.UpdatedAt,
            };
        }

        public void Delete(string id)
        {
            var user = _context.Users.SingleOrDefault(user => user.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public List<UserVM> getAll()
        {
            var users = _context.Users.Select(user => new UserVM
            {
                Id=user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Gender = user.Gender,
                Phone = user.Phone,
                DateOfBirth = user.DateOfBirth,
                Avatar = user.Avatar,
                IsFirstLogin = user.IsFirstLogin,
                Address = user.Address,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            });
            return users.ToList();
        }

        public UserVM getById(string id)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(user => user.Id == id);
                if(user == null)
                {
                    return null;
                }
                UserVM us = new UserVM
                {
                    Id= user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    Gender = user.Gender,
                    Phone = user.Phone,
                    DateOfBirth = user.DateOfBirth,
                    Avatar = user.Avatar,
                    IsFirstLogin = user.IsFirstLogin,
                    Address = user.Address,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                };
                return us;
            }
            catch
            {
                return null;
            }
        }

        public void Update(UserVM user)
        {
            var userEdit = _context.Users.SingleOrDefault(u => u.Id == user.Id);
            userEdit.Avatar=user.Avatar;
            _context.SaveChanges();
        }

        public UserVM Validate(UserLogin model)
        {
            var user= _context.Users.SingleOrDefault(p=>p.Email==model.Email && p.Password==model.Password);
            if (user != null)
            {
                UserVM u= new UserVM
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Password = user.Password,
                    Gender = user.Gender,
                    Phone = user.Phone,
                    DateOfBirth = user.DateOfBirth,
                    Avatar = user.Avatar,
                    IsFirstLogin = user.IsFirstLogin,
                    Address = user.Address,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                };
                return u;
            }
            return null;
        }
    }
}
