using System;
using UserApp.Controllers.Class;
using UserApp.Database.Model;

namespace UserApp.Database.Repository
{
    public interface IRepositoryData
    {
        List<UserDetails> GetAllUser();
        List<UserGroup> GetAllGroups();
        List<UserPermission> GetAllUserPermission();
        UserHome GetUserById(Guid id);
        void DeleteUser(UserDetails userDetails);
        void SaveUser(UserDetails user, LinkUser linkUser);
        void EditUser(UserDetails user, LinkUser linkUser);
    }
}
