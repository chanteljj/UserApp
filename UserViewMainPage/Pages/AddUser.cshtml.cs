using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
        public UserDetailViewModel User { get; set; } // BindProperty for the form data

        public IEnumerable<SelectListItem> UserGroupOptions { get; set; }
        public IEnumerable<SelectListItem> UserPermissionOptions { get; set; }


        public UserGroupViewModel Group { get; set; }
        public UserPermissionViewModel Permission { get; set; }

        public IActionResult OnPost()
        {

            var test = User;
            //if (!ModelState.IsValid)
            //{
            //    return Page(); // If model state is not valid, redisplay the form
            //}

            //// Convert UserDetailViewModel to your domain model if necessary
            //var user = new User
            //{
            //    Id = User.Id,
            //    Username = User.Username,
            //    Surname = User.Surname,
            //    ContactNumber = User.ContactNumber,
            //    Address = User.Address,
            //    Active = User.Active
            //};

            //_userRepository.AddUser(user); // Add the user to the database


            return RedirectToPage("/Index"); // Redirect to the users index page after adding the user
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
            }

            var responses = await client.GetAsync(BaseUrl + "/api/User/getPermission");
            if (responses.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserPermissionOption = JsonConvert.DeserializeObject<IList<UserPermissionViewModel>>(content);
            }
        }

    }
}
