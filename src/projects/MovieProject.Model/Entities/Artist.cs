namespace MovieProject.Model.Entities;

public sealed class Artist
{
    public long Id { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }
    public string ImageUrl { get; set; }

    public DateTime BirthDate { get; set; }

    public ICollection<MovieArtist> MovieArtists { get; set; }
}





