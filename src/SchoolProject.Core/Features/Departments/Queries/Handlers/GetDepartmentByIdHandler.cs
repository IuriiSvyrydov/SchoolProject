

using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Features.Departments.Queries.Results;
using SchoolProject.Core.Wrappers;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Departments.Queries.Handlers;

public class GetDepartmentByIdHandler : ResponseHandler, IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
{
    private readonly IDepartmentService _departmentsService;
    private readonly IStudentService _studentService;
    private readonly IStringLocalizer<SharedResources> _localization;
    private IMapper _mapper;
    public GetDepartmentByIdHandler(IDepartmentService departmentService, IStudentService studentService,
        IMapper mapper, IStringLocalizer<SharedResources> localization) : base(localization)
    {
        _departmentsService = departmentService;
        _studentService = studentService;
        _mapper = mapper;
        _localization = localization;
    }

    public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var dept = await _departmentsService.GetDepartmentByIdAsync(request.Id);
        if (dept == null) return NotFound<GetDepartmentByIdResponse>(_localization[SharedResourcesKey.NotFound]);
        var mapper = _mapper.Map<GetDepartmentByIdResponse>(dept);
        Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Localized(e.NameUa, e.NameUs));
        var studentQueryble = _studentService.GetStudentsByDepartmentIdQuerable(request.Id);
        var paginationList = await studentQueryble.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
        mapper.StudentList = paginationList;
        return Success(mapper);

    }
}
