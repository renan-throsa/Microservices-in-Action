using LoyaltyProgram.Data;
using LoyaltyProgram.Domain;
using Microsoft.AspNetCore.Mvc;

namespace LoyaltyProgram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ILoyaltyProgramUserStore userStore;
        private readonly IEventStore eventStore;

        public UsersController(ILoyaltyProgramUserStore userStore, IEventStore eventStore)
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
