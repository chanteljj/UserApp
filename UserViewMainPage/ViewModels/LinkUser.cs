namespace UserViewMainPage.ViewModels
{
    public class LinkUser
    {
        public Guid Id { get; set; }
        public UserDetails UserDetails { get; set; }
        public UserGroup UserGroup { get; set; }
        public UserPermission UserPermission { get; set; }
    }
}
