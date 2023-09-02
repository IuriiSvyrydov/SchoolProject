

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
    [HttpGet(Router.DepartmentRouting.GetDepartmentStudentCount)]
    public async Task<IActionResult> GetDepartmentStudentCount()
    {
        return NewResult(await Mediator.Send(new GetDepartmentStudentCountQuery()));
    }

    [HttpGet(Router.DepartmentRouting.GetDepartmentStudentCountById)]
    public async Task<IActionResult> GetDepartmentStudentCountById([FromRoute] int id)
    {
        return NewResult(await Mediator.Send(new GetDepartmentStudentCountByIdQuery()
        {
            DID = id
        }));
        ;
    }
}

