using Microsoft.EntityFrameworkCore;
using UserApp.Controllers.Class;
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

        public void EditUser(UserDetails order, LinkUser linkUser)
        {
            try
            {
                _context.Update<UserDetails>(order);
                _context.SaveChanges();
                linkUser.UserDetails = _context.UserDetails.Find(order.Id);
                linkUser.UserGroup = _context.UserGroup.Find(linkUser.UserGroup.Id);
                linkUser.UserPermission = _context.UserPermission.Find(linkUser.UserPermission.Id);
                _context.Add<LinkUser>(linkUser);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserHome GetUserById(Guid id)
        {
            try
            {
                UserDetails user = _context.UserDetails.Find(id);
                LinkUser linkUser = _context.LinkUser.Include(x => x.UserDetails)
                    .Where(x => x.UserDetails.Id == user.Id).FirstOrDefault();
                UserHome home = new UserHome
                {
                    UserDetails = user,
                    LinkUser = linkUser
                };
                return home;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}
