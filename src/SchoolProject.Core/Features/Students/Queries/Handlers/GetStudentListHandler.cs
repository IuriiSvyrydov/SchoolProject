

namespace SchoolProject.Core.Features.Students.Queries.Handlers;

public class GetStudentListHandler : ResponseHandler, IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>
{
    private readonly IStudentService _studentService;
    private readonly IStringLocalizer<SharedResources> _localizer;
    private IMapper _mapper;
    public GetStudentListHandler(IStudentService studentService, IMapper mapper,
        IStringLocalizer<SharedResources> localizer) : base(localizer)
    {
        _studentService = studentService;
        _mapper = mapper;
        _localizer = localizer;
    }



    public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
    {

        var studentList = await _studentService.GetStudentListAsync();
        var studentListMapper = _mapper.Map<List<GetStudentListResponse>>(studentList);
        var result = Success(studentListMapper);
        result.Meta = new { Count = studentListMapper.Count() };
        return result;

    }
}
