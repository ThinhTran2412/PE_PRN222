

namespace RealEstateManagement__TranThaiThinh.Repositories.Models;

public partial class Broker
{
    public int BrokerId { get; set; }

    public string FullName { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}