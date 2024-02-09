using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;
using UserViewMainPage.ViewModels;

namespace UserViewMainPage.Pages
{
    public class EditUserModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public EditUserModel(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [BindProperty]
        public UserDetails User { get; set; }

        [BindProperty]
        public LinkUser LinkUser { get; set; }

        [BindProperty]
        public UserGroup Group { get; set; }
        [BindProperty]
        public UserPermission Permission { get; set; }

        public IEnumerable<SelectListItem> UserGroupOptions { get; set; }
        public IEnumerable<SelectListItem> UserPermissionOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = _clientFactory.CreateClient("API");
            var baseUrl = _configuration.GetValue<string>("APIURL:BaseUrl");

            var response = await client.GetAsync($"{baseUrl}/api/User/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            var userHome = JsonConvert.DeserializeObject<UserHome>(content);

            User = userHome.UserDetails;
            LinkUser = userHome.LinkUser;
            Group = userHome.LinkUser.UserGroup;
            Permission = userHome.LinkUser.UserPermission;

            response = await client.GetAsync($"{baseUrl}/api/User/getGroup");
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
                var userGroups = JsonConvert.DeserializeObject<IList<UserGroup>>(content);
                UserGroupOptions = userGroups.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name });
            }

            response = await client.GetAsync($"{baseUrl}/api/User/getPermission");
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
                var userPermissions = JsonConvert.DeserializeObject<IList<UserPermission>>(content);
                UserPermissionOptions = userPermissions.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _clientFactory.CreateClient("API");
            var baseUrl = _configuration.GetValue<string>("APIURL:BaseUrl");

            var response = await client.GetAsync(baseUrl + "/api/User/getGroup");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserGroupOption = JsonConvert.DeserializeObject<IList<UserGroup>>(content);
                UserGroupOptions = UserGroupOption
            .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name });
            }

            var responses = await client.GetAsync(baseUrl + "/api/User/getPermission");
            if (responses.IsSuccessStatusCode)
            {
                var content = await responses.Content.ReadAsStringAsync();
                UserPermissionOption = JsonConvert.DeserializeObject<IList<UserPermission>>(content);
                UserPermissionOptions = UserPermissionOption
            .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });
            }

            UserGroup selectedGroup = UserGroupOption.FirstOrDefault(g => g.Id == Group.Id);
            UserPermission selectedPermission = UserPermissionOption.FirstOrDefault(p => p.Id == Permission.Id);
            
            LinkUser.UserPermission = selectedPermission;
            LinkUser.UserGroup = selectedGroup;
            LinkUser.UserDetails = User;
            var userHome = new UserHome
            {
                UserDetails = User,
                LinkUser = LinkUser
            };

            var jsonContent = JsonConvert.SerializeObject(userHome);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send PUT request to update user details
            var updateResponse = await client.PutAsync($"{baseUrl}/api/User/editUser", stringContent);

            if (updateResponse.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                return BadRequest();
            }
        }

        public IList<UserGroup> UserGroupOption { get; set; }
        public IList<UserPermission> UserPermissionOption { get; set; }
    }
}