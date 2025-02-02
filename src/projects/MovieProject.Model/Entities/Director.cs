using Core.DataAccess.Entities;

namespace MovieProject.Model.Entities;

public sealed class Director : Entity<long>
{
    public Director()
    {
        Name = string.Empty;
        Surname = string.Empty;
        ImageUrl = string.Empty;
        Movies = new HashSet<Movie>();
    }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string ImageUrl { get; set; }

    public DateTime BirthDay { get; set; }


    public ICollection<Movie> Movies { get; set; }
}





