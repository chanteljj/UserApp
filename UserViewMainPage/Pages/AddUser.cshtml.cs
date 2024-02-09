using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using UserViewMainPage.ViewModels;

namespace UserViewMainPage.Pages
{
    public class AddUserModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;


        public AddUserModel(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        [BindProperty]
        public UserDetails User { get; set; } // BindProperty for the form data

        public IEnumerable<SelectListItem> UserGroupOptions { get; set; }
        public IEnumerable<SelectListItem> UserPermissionOptions { get; set; }

        [BindProperty]
        public UserGroup Group { get; set; }
        [BindProperty]
        public UserPermission Permission { get; set; }

        public async Task<IActionResult> OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _clientFactory.CreateClient("API");
            BaseUrl = _configuration.GetValue<string>("APIURL:BaseUrl");

            var response = await client.GetAsync(BaseUrl + "/api/User/getGroup");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserGroupOption = JsonConvert.DeserializeObject<IList<UserGroup>>(content);
                UserGroupOptions = UserGroupOption
            .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name });
            }

            var responses = await client.GetAsync(BaseUrl + "/api/User/getPermission");
            if (responses.IsSuccessStatusCode)
            {
                var content = await responses.Content.ReadAsStringAsync();
                UserPermissionOption = JsonConvert.DeserializeObject<IList<UserPermission>>(content);
                UserPermissionOptions = UserPermissionOption
            .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });
            }

            UserGroup selectedGroup = UserGroupOption.FirstOrDefault(g => g.Id == Group.Id);
            UserPermission selectedPermission = UserPermissionOption.FirstOrDefault(p => p.Id == Permission.Id);
            UserDetails newUser = new UserDetails {
                Username = User.Username,
                Surname = User.Surname,
                ContactNumber = User.ContactNumber,
                Address = User.Address,
                Active = User.Active
            };

            UserHome userHome = new UserHome
            {
                UserDetails = newUser,
                LinkUser = new LinkUser
                {
                    UserGroup = selectedGroup,
                    UserPermission = selectedPermission,
                    UserDetails = newUser
                }
            };

            var jsonContent = JsonConvert.SerializeObject(userHome);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send POST request to the API endpoint
            var saveResponse = await client.PostAsync(BaseUrl + "/api/User/createUser", stringContent);

            if (saveResponse.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index"); 
            }
            else
            {
                return BadRequest();
            }
        }

        public string BaseUrl { get; private set; }


        public IList<UserGroup> UserGroupOption { get; set; }
        public IList<UserPermission> UserPermissionOption { get; set; }



        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("API");

            BaseUrl = _configuration.GetValue<string>("APIURL:BaseUrl");
            var response = await client.GetAsync(BaseUrl + "/api/User/getGroup");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserGroupOption = JsonConvert.DeserializeObject<IList<UserGroup>>(content);
                UserGroupOptions = UserGroupOption
            .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name });
            }

            var responses = await client.GetAsync(BaseUrl + "/api/User/getPermission");
            if (responses.IsSuccessStatusCode)
            {
                var content = await responses.Content.ReadAsStringAsync();
                UserPermissionOption = JsonConvert.DeserializeObject<IList<UserPermission>>(content);
                UserPermissionOptions = UserPermissionOption
            .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });
            }
        }

    }
}
