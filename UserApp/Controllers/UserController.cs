using Microsoft.AspNetCore.Mvc;
using UserApp.Controllers.Class;
using UserApp.Database.Model;
using UserApp.Database.Repository;

namespace UserApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IRepositoryData _reposityData;
        private readonly ILogger<UserController> _logger;
        public UserController(IRepositoryData reposityData, ILogger<UserController> logger)
        {
            _reposityData = reposityData;
            _logger = logger;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userList = _reposityData.GetAllUser().Select(x => x.Active == true);
                if (userList != null)
                {
                    //return View("Index");
                    return Ok(_reposityData.GetAllUser());
                }
                return BadRequest();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("getGroup")]
        public async Task<IActionResult> GetGroup()
        {
            try
            {
                var userList = _reposityData.GetAllGroups();
                if (userList != null)
                {
                    return Ok(_reposityData.GetAllGroups());
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("getPermission")]
        public async Task<IActionResult> GetPermission()
        {
            try
            {
                var userList = _reposityData.GetAllUserPermission();
                if (userList != null)
                {
                    //return View("Index");
                    return Ok(_reposityData.GetAllUserPermission());
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserHome userHome)
        {
            try
            {
                //UserHome
                if (userHome != null)
                {
                    _reposityData.SaveUser(userHome.UserDetails, userHome.LinkUser);
                    return Ok("Success");
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                throw ex;
            }
  
        }

    }
}
