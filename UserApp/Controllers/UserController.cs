using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
                var userList = _reposityData.GetAllUser();
                if (userList != null)
                {
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
                    return Ok(_reposityData.GetAllUserPermission());
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = _reposityData.GetUserById(id);
                if (user != null)
                {
                    return Ok(user);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("createUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserHome userHome)
        {
            try
            {
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

        [HttpPut]
        [Route("editUser")]
        public async Task<IActionResult> EditUser([FromBody] UserHome userHome)
        {
            try
            {
                if (userHome != null)
                {
                    _reposityData.EditUser(userHome.UserDetails, userHome.LinkUser);
                    return Ok("Success");
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpDelete]
        [Route("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var user = _reposityData.GetUserById(id);
                if (user != null)
                {
                    _reposityData.DeleteUser(user.UserDetails);
                    return Ok("User deleted successfully");
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
