

namespace RealEstateManagement__TranThaiThinh.Repositories.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public string ContractTitle { get; set; }

    public string PropertyType { get; set; }

    public DateOnly SigningDate { get; set; }

    public DateOnly ExpirationDate { get; set; }

    public int BrokerId { get; set; }

    public decimal Value { get; set; }

    public virtual Broker Broker { get; set; }
}