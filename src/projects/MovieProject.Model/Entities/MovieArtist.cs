using Core.DataAccess.Entities;

namespace MovieProject.Model.Entities;

public sealed class MovieArtist: Entity<long>
{

    public MovieArtist()
    {
        Artist = new Artist();
        Movie = new Movie();
    }
    public long ArtistId { get; set; }

    public Artist Artist { get; set; }

    public Guid MovieId { get; set; }
    public Movie Movie { get; set; }
}





