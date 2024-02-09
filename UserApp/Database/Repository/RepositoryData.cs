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
                return _context.UserDetails.Where(x => x.Active == true).ToList();
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
                order.Id = userId;
                order.Active = true;
                _context.Add<UserDetails>(order);
                _context.SaveChanges();

                if (userId != null)
                {
                    linkUser.Id = Guid.NewGuid();
                    linkUser.UserDetails = _context.UserDetails.Find(userId);
                    linkUser.UserGroup = _context.UserGroup.Find(linkUser.UserGroup.Id);
                    linkUser.UserPermission = _context.UserPermission.Find(linkUser.UserPermission.Id);

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
                LinkUser updatedLink = _context.LinkUser.Include(x => x.UserDetails)
                    .Where(x => x.UserDetails.Id == order.Id).FirstOrDefault();
                updatedLink.UserDetails = _context.UserDetails.Find(order.Id);
                updatedLink.UserGroup = _context.UserGroup.Find(linkUser.UserGroup.Id);
                updatedLink.UserPermission = _context.UserPermission.Find(linkUser.UserPermission.Id);
                _context.Update(updatedLink);
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
                    .Include(x => x.UserGroup)
                    .Include(x => x.UserPermission)
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

        public void DeleteUser(UserDetails userDetails)
        {
            try
            {
                userDetails.Active = false;
                _context.Update<UserDetails>(userDetails);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
