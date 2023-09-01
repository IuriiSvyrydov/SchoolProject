
namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class GetSingleStudentHandler : ResponseHandler, IRequestHandler<GetSingleStudentQuery, Response<GetSingleStudentResponse>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public GetSingleStudentHandler(IStudentService studentService,
          IMapper mapper, IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Response<GetSingleStudentResponse>> Handle(GetSingleStudentQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdAsync(request.Id);
            if (student == null)
                return NotFound<GetSingleStudentResponse>(_localizer[SharedResourcesKey.NotFound]);

            var studentMapping = _mapper.Map<GetSingleStudentResponse>(student);
            return Success(studentMapping);
        }
    }
}
