using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities.Views;

[Keyless]
public class ViewDepartment : GeneralLocalizableEntity
{

    public int DID { get; set; }

    public string? NameUa { get; set; }
    public string? NameUs { get; set; }
    public int StudentCount { get; set; }
}