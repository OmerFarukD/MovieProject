namespace MovieProject.Service.Constants.Movies;

public static class RedisMovieKey
{

    public const string MovieListKey = "movie_list";


    public static string GetByIdKey(Guid id) => $"movie_{id}";
}
