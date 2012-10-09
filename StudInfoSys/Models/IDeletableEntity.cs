namespace StudInfoSys.Models
{

    /// <summary>
    /// Implement this interface if you want an entity to just be marked as deleted in the database instead of permanently deleting it
    /// </summary>
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
    }
}