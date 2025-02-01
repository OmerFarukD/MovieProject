namespace MovieProject.Model.Entities;

public sealed class Director
{
    public long Id { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string ImageUrl { get; set; }

    public DateTime BirthDay { get; set; }


    public ICollection<Movie> Movies { get; set; }
}





