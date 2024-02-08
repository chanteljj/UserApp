using System.ComponentModel.DataAnnotations;

namespace UserApp.Database.Model
{
    public class LinkUser
    {
        [Key]
        public Guid Id { get; set; }
        public UserDetails UserDetails { get; set; }
        public UserGroup UserGroup { get; set; }
        public UserPermission UserPermission { get; set; }
    }
}
