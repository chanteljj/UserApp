namespace UserViewMainPage.ViewModels
{
    public class LinkUserViewModel
    {
        public Guid Id { get; set; }
        public UserDetailViewModel UserDetails { get; set; }
        public UserGroupViewModel UserGroup { get; set; }
        public UserPermissionViewModel UserPermission { get; set; }
    }
}
