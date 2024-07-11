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
            var user = userStore.GetBy(userId);

            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public ActionResult<LoyaltyProgramUser> CreateUser([FromBody] LoyaltyProgramUser user)
        {
            if (user == null)
                return BadRequest();
            var newUser = userStore.Save(user);
            return Created(new Uri($"/users/{newUser.Id}", UriKind.Relative), newUser);
        }

        [HttpPut("{userId:int}")]
        public LoyaltyProgramUser UpdateUser(int userId, [FromBody] LoyaltyProgramUser user) => userStore.Save(user) ;

    }
}
