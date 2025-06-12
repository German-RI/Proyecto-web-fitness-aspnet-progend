public class ManageRolesViewModel
{
    public string UserId { get; set; }
    public string UserEmail { get; set; }
    public List<string> AvailableRoles { get; set; }
    public List<string> AssignedRoles { get; set; }
}