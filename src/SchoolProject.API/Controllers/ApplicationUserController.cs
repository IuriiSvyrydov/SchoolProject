

using SchoolProject.Core.Features.Users.Commands.Models;
using SchoolProject.Core.Features.Users.Queries.Models;
using SchoolProject.Core.Mapping.ApplicationUser.Queries.Models;

namespace SchoolProject.API.Controllers
{

    [ApiController]
    public class ApplicationUserController : AppControllerBase
    {
        [HttpPost(Router.UserRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {

            return NewResult(await Mediator.Send(command));
        }
        [HttpGet(Router.UserRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetUserPaginationQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        [HttpGet(Router.UserRouting.GetById)]
        public async Task<IActionResult> GetSingleUser(int id)
        {
            return NewResult(await Mediator.Send(new GetUserByIdQuery(id)));
        }
        [HttpPut(Router.UserRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] UpdateUserCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpDelete(Router.UserRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteUserCommand(id));

            return NewResult(result);
        }
        [HttpPut(Router.UserRouting.ChangePassword)]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
    }
}
