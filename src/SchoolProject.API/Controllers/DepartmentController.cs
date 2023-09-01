

using SchoolProject.Core.Features.Departments.Queries.Models;

namespace SchoolProject.API.Controllers;


[ApiController]
public class DepartmentController : AppControllerBase
{
    [HttpGet(Router.DepartmentRouting.GetById)]
    public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
    {
        return NewResult(await Mediator.Send(query));
    }
}
