using System.ComponentModel.DataAnnotations;

namespace UserApp.Database.Model
{
    public class UserDetails
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Surname { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
    }
}
