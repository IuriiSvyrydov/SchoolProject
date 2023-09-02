using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Results;

namespace SchoolProject.Core.Features.Departments.Queries.Handlers;

public class GetDepartmentStudentCountHandler : ResponseHandler, IRequestHandler<GetDepartmentStudentCountQuery, Response<List<GetDepartmentStudentCountResponse>>>
{
    private readonly IDepartmentService _departmentService;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IMapper _mapper;
    public GetDepartmentStudentCountHandler(IStringLocalizer<SharedResources> localizer, IDepartmentService departmentService, IMapper mapper) : base(localizer)
    {
        _localizer = localizer;
        _departmentService = departmentService;
        _mapper = mapper;
    }
    public async Task<Response<List<GetDepartmentStudentCountResponse>>> Handle(GetDepartmentStudentCountQuery request, CancellationToken cancellationToken)
    {
        var viewDepartmentResult = await _departmentService.GetDepartmentDataAsync();
        var result = _mapper.Map<List<GetDepartmentStudentCountResponse>>(viewDepartmentResult);
        return Success(result);
    }
}