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
        public UserDetailViewModel User { get; set; } 
        public UserGroupViewModel Group { get; set; }
        public UserPermissionViewModel Permission { get; set; }

        public IEnumerable<SelectListItem> UserGroupOptions { get; set; }
        public IEnumerable<SelectListItem> UserPermissionOptions { get; set; }


        public IActionResult OnPost()
        {
            try
            {
                var client = _clientFactory.CreateClient("API");
                string url = _configuration.GetValue<string>("APIURL:BaseUrl") + "/api/User/createUser";

                if (!ModelState.IsValid)
                {
                    return Page();
                }

                UserDetailViewModel user = new()
                {
                    Id = User.Id,
                    Username = User.Username,
                    Surname = User.Surname,
                    ContactNumber = User.ContactNumber,
                    Address = User.Address,
                    Active = true
                };

                Guid test = Group.Id;

                UserHomeViewModel homeViewModel = new()
                {
                    UserDetails = user,
                    LinkUser = new()
                    {
                        UserGroup = Group,
                        UserPermission = Permission,
                    }
                };

                var json = JsonConvert.SerializeObject(homeViewModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, content);

                if (response.IsCompleted)
                {
                    return RedirectToPage("/Index"); // Redirect to the users index page after adding the user
                }
                return RedirectToPage("/Index"); // Redirect to the users index page after adding the user
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string BaseUrl { get; private set; }


        public IList<UserGroupViewModel> UserGroupOption { get; set; }
        public IList<UserPermissionViewModel> UserPermissionOption { get; set; }



        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("API");

            BaseUrl = _configuration.GetValue<string>("APIURL:BaseUrl");
            var response = await client.GetAsync(BaseUrl + "/api/User/getGroup");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserGroupOption = JsonConvert.DeserializeObject<IList<UserGroupViewModel>>(content);
                UserGroupOptions = UserGroupOption
            .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name });
            }

            var responses = await client.GetAsync(BaseUrl + "/api/User/getPermission");
            if (responses.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserPermissionOption = JsonConvert.DeserializeObject<IList<UserPermissionViewModel>>(content);
                UserPermissionOptions = UserPermissionOption
            .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name });
            }
        }

    }
}
