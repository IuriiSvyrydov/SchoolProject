
using SchoolProject.Core.Features.Students.Commands.Models;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class AddStudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>
    {
        private readonly IStudentService _studentService;
        private IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localization;
        public AddStudentCommandHandler(IStringLocalizer<SharedResources> localization, IStudentService studentService, IMapper mapper) : base(localization)
        {
            _studentService = studentService;
            _mapper = mapper;
            _localization = localization;

        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapping = _mapper.Map<Student>(request);
            var result = await _studentService.AddAsync(studentMapping);
            if (result == _localization[SharedResourcesKey.Created]) return Created<string>(_localization[SharedResourcesKey.Added]);
            else return BadRequest<string>();
        }
    }
}