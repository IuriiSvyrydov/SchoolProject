using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.Students.Queries.Handlers;

public class GetStudentPaginationListHandler : ResponseHandler, IRequestHandler<GetStudentPaginationListQuery, PaginationResult<GetStudentPaginationListResponse>>
{
    private readonly IStudentService _studentService;
    private readonly IStringLocalizer<SharedResources> _localiztion;
    private IMapper _mapper;
    public GetStudentPaginationListHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources> localization) : base(localization)
    {
        _studentService = studentService;
        _mapper = mapper;
        _localiztion = localization;
    }

    public async Task<PaginationResult<GetStudentPaginationListResponse>> Handle(GetStudentPaginationListQuery request, CancellationToken cancellationToken)
    {
        //Expression<Func<Student, GetStudentPaginationListResponse>> expression = e => new GetStudentPaginationListResponse(
        //    e.StudID, e.Localized(e.NameUs,e.NameUa), e.Address, e.Department.NameUa);
        // var querable = _studentService.GetStudentsQuerable();
        var filterQuery = _studentService.FilterStudentPaginatedQuerable(request.OrderBy, request.Search);
        var paginationList = await _mapper.ProjectTo<GetStudentPaginationListResponse>(filterQuery).ToPaginatedListAsync(request.PageNumber, request.PageSize);
        paginationList.Meta = new { Count = paginationList.Data.Count() };
        return paginationList;
    }
}
