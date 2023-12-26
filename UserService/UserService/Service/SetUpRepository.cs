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
        public void AddPermission()
        {
            string[] permission = {"VIEW_COURSE","EDIT_COURSE", "VIEW_PRIVATE_FILE", "EDIT_PRIVATE_FILE", "CREATE_PRIVATE_FILE", "DELETE_PRIVATE_FILE", "DOWNLOAD_PRIVATE_FILE",
                "CREATE_RESOURCE","VIEW_RESOURCE","EDIT_RESOURCE","EDIT_RESOURCE","ADD_TO_COURSE","DOWNLOAD_RESOURCE","DELETE_RESOURCE","CREATE_EXAM","VIEW_EXAM","EDIT_EXAM",
                "DELETE_EXAM","DOWNLOAD_EXAM","APPROVE_EXAM"};
            List<string> permissions = permission.ToList();
            permissions.ForEach(x =>
            {
                var p =new Permission
                {
                    PermissionName = x,
                };
                _context.Permissions.Add(p);

            });
            _context.SaveChanges();
            
        }

        public void AddRoles()
        {
            
            Role r = new Role
            {
                RoleName = "Leadership"
            };
            
            Role r3 = new Role
            {
                RoleName = "Teacher"
            };
            Role r4 = new Role
            {
                RoleName = "Student"
            };
            _context.Roles.Add(r);
            _context.Roles.Add(r3);
            _context.Roles.Add(r4);
            _context.SaveChanges();

        }
    }
}
