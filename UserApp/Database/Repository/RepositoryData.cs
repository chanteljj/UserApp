using UserApp.Database.Model;

namespace UserApp.Database.Repository
{
    public class RepositoryData : IRepositoryData
    {
        private readonly Context _context;
        public RepositoryData(Context context)
        {
            _context = context;
        }
        public List<UserDetails> GetAllUser()
        {
            try
            {
                return _context.UserDetails.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public List<UserGroup> GetAllGroups()
        {
            try
            {
                return _context.UserGroup.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //UserPermissionViewModel
        public List<UserPermission> GetAllUserPermission()
        {
            try
            {
                return _context.UserPermission.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void SaveUser(UserDetails order, LinkUser linkUser)
        {
            try
            {
                var userId = Guid.NewGuid();
                //Save 
                order.Id = userId;
                _context.Add<UserDetails>(order);
                _context.SaveChanges();

                //Guid idUser = order.Id;

                if (userId != null)
                {
                    //Save Linked
                    linkUser.Id = Guid.NewGuid();
                    linkUser.UserDetails = _context.UserDetails.Find(userId);
                    linkUser.UserGroup = _context.UserGroup.Find(linkUser.UserGroup.Id);
                    linkUser.UserPermission = _context.UserPermission.Find(linkUser.UserPermission.Id);

                    //if (string.IsNullOrWhiteSpace(linkUser.UserPermission.Id.ToString()))
                    if (linkUser.UserDetails.Id != null)
                    {
                        _context.Add<LinkUser>(linkUser);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

  
    }
}
