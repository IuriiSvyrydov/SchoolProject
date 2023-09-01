using SchoolProject.Core.Features.Students.Commands.Models;

namespace SchoolProject.Core.Features.Students.Commands.Handlers;

public class EditStudentCommandHandler : ResponseHandler, IRequestHandler<EditStudentCommand, Response<string>>
{
    private readonly IStudentService _studentService;
    private readonly IStringLocalizer<SharedResources> _localization;
    private IMapper _mapper;

    public EditStudentCommandHandler(IStudentService studentService, IMapper mapper,
        IStringLocalizer<SharedResources> localization) : base(localization)
    {
        _studentService = studentService;
        _mapper = mapper;
        _localization = localization;
    }

    public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
    {
        // find id
        var student = await _studentService.GetStudentByIdAsync(request.Id);
        if (student is null)
            return NotFound<string>(_localization[SharedResourcesKey.NotFound]);
        var studentMapper = _mapper.Map(request, student);
        var result = await _studentService.EditAsync(studentMapper);
        if (result is "success")
            return Created<string>(_localization[SharedResourcesKey.Updated]);
        else
            return BadRequest<string>();
    }
}