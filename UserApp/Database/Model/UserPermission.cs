using System.ComponentModel.DataAnnotations;

namespace UserApp.Database.Model
{
    public class UserPermission
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
