namespace Customer.Domain.Entities;

using BuildingBlocks.Shared.DomainObjects;
using CAARepositoryLibrary.Entities;
using System.Collections.ObjectModel;

public class Customer : Entity, IBaseEntity
{
    #region Members
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime Dob { get; set; }
    public string CreatedById { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string? LastModifiedById { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }

    private readonly List<Account> _accounts;
    public ReadOnlyCollection<Account> Accounts => _accounts.AsReadOnly();
    #endregion


    #region Ctor
    public Customer()
    {
        _accounts = new List<Account>();
    }
    #endregion
}
