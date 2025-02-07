using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieProject.Model.Entities;

namespace MovieProject.DataAccess.Configurations
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {




            builder.Navigation(x => x.Director).AutoInclude();
            builder.Navigation(x => x.Category).AutoInclude();
        }
    }
}
