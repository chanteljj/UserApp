using UserApp.Database.Model;

namespace UserApp.Database.Repository
{
    public interface IRepositoryData
    {
        List<UserDetails> GetAllUser();
        List<UserGroup> GetAllGroups();
        List<UserPermission> GetAllUserPermission();
        void SaveUser(UserDetails user, LinkUser linkUser);
    }
}
