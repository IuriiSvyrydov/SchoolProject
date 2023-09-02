using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Results;
using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.Core.Features.Departments.Queries.Handlers;

public class GetDepartmentStudentCountByIdHandler : ResponseHandler, IRequestHandler<GetDepartmentStudentCountByIdQuery,
    Response<GetDepartmentStudentCountByIdResponse>>
{
    private readonly IDepartmentService _departmentService;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private readonly IMapper _mapper;
    public GetDepartmentStudentCountByIdHandler(IStringLocalizer<SharedResources> localizer, IDepartmentService departmentService, IMapper mapper) : base(localizer)
    {
        _localizer = localizer;
        _departmentService = departmentService;
        _mapper = mapper;
    }

    public async Task<Response<GetDepartmentStudentCountByIdResponse>> Handle(GetDepartmentStudentCountByIdQuery request, CancellationToken cancellationToken)
    {
        var parameters = _mapper.Map<DepartmentStudentCountProcParameters>(request);
        var departmentResult = await _departmentService.GetDepartmentStudentCountProcAsync(parameters);
        var result = _mapper.Map<GetDepartmentStudentCountByIdResponse>(departmentResult.FirstOrDefault());
        return Success(result);
    }
}