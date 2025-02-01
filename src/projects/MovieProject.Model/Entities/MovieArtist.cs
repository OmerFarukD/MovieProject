namespace MovieProject.Model.Entities;

public sealed class MovieArtist
{
    public long Id { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? UpdatedTime { get; set; }


    public long ArtistId { get; set; }

    public Artist Artist { get; set; }

    public Guid MovieId { get; set; }
    public Movie Movie { get; set; }
}





