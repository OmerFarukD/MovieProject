using MovieProject.Model.Dtos.Directors;
using MovieProject.Model.Entities;

namespace Service.Test.Utils;

public static class DirectorUtils
{

    public static DirectorAddRequestDto AddRequestDto() 
    {
        return new DirectorAddRequestDto
        {
            BirthDate = DateTime.UtcNow,
            Image = null,
            Name = "Buse",
            Surname = "Doğan"
        };
    }

    public static Director AddMapDirector()
    {
        return new Director
        {
            Id = 1,
            BirthDay = DateTime.UtcNow,
            CreatedTime = DateTime.UtcNow,
            ImageUrl = "img.jpg",
            Movies = new List<Movie>(),
            Name = "Buse",
            Surname = "Doğan",
            UpdatedTime = null
        };
    }


    public static List<Director> DirectorsList()
    {
        return new List<Director>
        {
            new Director
        {
            Id = 1,
            BirthDay = DateTime.UtcNow,
            CreatedTime = DateTime.UtcNow,
            ImageUrl = "img.jpg",
            Movies = new List<Movie>(),
            Name = "Buse",
            Surname = "Doğan",
            UpdatedTime = null
        },
  new Director
        {
            Id = 2,
            BirthDay = DateTime.UtcNow,
            CreatedTime = DateTime.UtcNow,
            ImageUrl = "img.jpg",
            Movies = new List<Movie>(),
            Name = "Ömer",
            Surname = "Doğan",
            UpdatedTime = null
        },

        };
    }

    public static List<DirectorResponseDto> ResponseList()
    {
        return new List<DirectorResponseDto>
        {
            new DirectorResponseDto
            {
                BirthDate = DateTime.UtcNow,
                Id = 1,
                Name = "Buse",
                Surname = "Doğan",
                Image = null
            },
            new DirectorResponseDto
            {
                BirthDate = DateTime.UtcNow,
                Id = 2,
                Name = "Ömer",
                Surname = "Doğan",
                Image = null
            },
        };
    }

}
