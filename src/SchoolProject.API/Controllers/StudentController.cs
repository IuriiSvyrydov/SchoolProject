using Microsoft.AspNetCore.Authorization;
using SchoolProject.Core.Filters;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    //   [Authorize]
    public class StudentController : AppControllerBase
    {
        [HttpGet(Router.StudentRouting.List)]
        //  [Authorize(Roles = "User")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetStudentList()
        {
            return Ok(await Mediator.Send(new GetStudentListQuery()));
        }

        [HttpGet(Router.StudentRouting.Paginated)]
        [AllowAnonymous]
        public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginationListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetSingleStudent([FromForm] int id)
        {
            return NewResult(await Mediator.Send(new GetSingleStudentQuery(id)));
        }
        [Authorize(Policy = "CreateStudent")]

        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddStudentCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpDelete(Router.StudentRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeleteStudentCommand(id));

            return NewResult(result);
        }
    }
}