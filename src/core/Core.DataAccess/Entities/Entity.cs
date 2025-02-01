namespace Core.DataAccess.Entities;

public abstract class Entity<TId>
{
    public TId Id { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }


    public Entity()
    {
        Id = default;
       
    }

    public Entity(TId id)
    {
        Id = id;
    }
}