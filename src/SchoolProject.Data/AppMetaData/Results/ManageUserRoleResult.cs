namespace SchoolProject.Data.Requests.Results;

public class ManageUserRoleResult
{
    public int UserId { get; set; }
    public List<UserRoles> RolesList { get; set; } = new();
}

public class UserRoles
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsHasRole { get; set; }
}