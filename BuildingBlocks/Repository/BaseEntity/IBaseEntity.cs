namespace CAARepositoryLibrary.Entities;

/// <summary>
/// IBaseEntity could use to inherit following common property
/// </summary>
public interface IBaseEntity
{
    public string CreatedById { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? LastModifiedById { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
