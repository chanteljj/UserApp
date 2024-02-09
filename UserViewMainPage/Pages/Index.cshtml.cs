using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using UserViewMainPage.ViewModels;

namespace UserViewMainPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public IList<UserDetails> Users { get; set; }

        public string BaseUrl { get; private set; }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("API");

            BaseUrl = _configuration.GetValue<string>("APIURL:BaseUrl");
            var response = await client.GetAsync(BaseUrl + "/api/User/getall");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Users = JsonConvert.DeserializeObject<IList<UserDetails>>(content);
            }
            else
            {
                // Handle error
            }
        }
    }
}