using LoyaltyProgram.Domain.Entities;
using LoyaltyProgram.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersLoyaltyProgramController : ControllerBase
    {

        private readonly ILoyaltyProgramRepository userStore;
        private readonly IEventRepository eventStore;

        public UsersLoyaltyProgramController(ILoyaltyProgramRepository userStore, IEventRepository eventStore)
        {
            this.userStore = userStore;
            this.eventStore = eventStore;
        }

        [HttpGet("{userId:int}")]
        public ActionResult<LoyaltyProgramUser> GetUser(int userId)
        {
           
            return Ok(userId);
        }

        [HttpPost]
        public ActionResult<LoyaltyProgramUser> CreateUser([FromBody] LoyaltyProgramUser user)
        {
            return Ok(user);
        }


    }
}
