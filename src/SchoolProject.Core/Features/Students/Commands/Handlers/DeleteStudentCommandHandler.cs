
using SchoolProject.Core.Features.Students.Commands.Models;

namespace SchoolProject.Core.Features.Students.Commands.Handlers;

public class DeleteStudentCommandHandler : ResponseHandler, IRequestHandler<DeleteStudentCommand, Response<string>>
{
    private readonly IStudentService _studentService;
    private IMapper _mapper;
    private IStringLocalizer<SharedResources> _localization;

    public DeleteStudentCommandHandler(IStringLocalizer<SharedResources> localization, IStudentService studentService, IMapper mapper) : base(localization)
    {
        _studentService = studentService;
        _mapper = mapper;
        _localization = localization;
    }

    public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetStudentByIdAsync(request.Id);
        if (student == null)
        {
            return NotFound<string>(_localization[SharedResourcesKey.NotFound]);
        }
        var result = await _studentService.DeleteAsync(student);
        if (result is "Success")
            return Deleted<string>(_localization[SharedResourcesKey.Deleted]);
        else
            return BadRequest<string>();
    }
}
