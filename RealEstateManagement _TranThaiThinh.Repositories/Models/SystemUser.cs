

namespace RealEstateManagement__TranThaiThinh.Repositories.Models;

public partial class SystemUser
{
    public int UserId { get; set; }

    public string UserPassword { get; set; }

    public string Username { get; set; }

    public int UserRole { get; set; }

    public DateTime? RegistrationDate { get; set; }
}