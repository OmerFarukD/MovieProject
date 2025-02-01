namespace MovieProject.Model.Entities;

public sealed class Category
{
    public int Id { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public string Name { get; set; }

    public ICollection<Movie> Movies { get; set; }
}





