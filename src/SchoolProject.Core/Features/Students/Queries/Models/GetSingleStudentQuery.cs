

namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetSingleStudentQuery : IRequest<Response<GetSingleStudentResponse>>
    {
        public int Id { get; set; }
        public GetSingleStudentQuery(int id)
        {
            Id = id;
        }
    }
}
