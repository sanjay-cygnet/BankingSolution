namespace Customer.Domain.Entities;

using BuildingBlocks.Shared.DomainObjects;
using CAARepositoryLibrary.Entities;

public class BankBranch : Entity, IBaseEntity
{
    #region Members
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public DateTime OpenedOn { get; set; }

    private readonly List<Account> _accounts;
    public IReadOnlyCollection<Account> Accounts => _accounts.AsReadOnly();
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
    public string CreatedById { get; set; } = string.Empty;
    public string? LastModifiedById { get; set; }////Allows nullable in database
    public DateTime? LastModifiedDate { get; set; }
    public short Currency { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Ctor
    public BankBranch()
    {
        _accounts = new List<Account>();
    }
    #endregion
}