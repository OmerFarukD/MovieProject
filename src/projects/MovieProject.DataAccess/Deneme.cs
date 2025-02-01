using MovieProject.Model.Entities;

namespace MovieProject.DataAccess;

public class Deneme
{

    public void Dene()
    {

        // Hiç bir şekilde bir kısıtlama yapmadığı için ilgili alanlar kaçırılabilir hata vermez.
        Movie movie = new Movie()
        {
            Id = Guid.NewGuid(),
            CategoryId = 1,
            CreatedTime = DateTime.Now,
            
        };

        // Kullanıcıdan zorunlu olarak almak istediğim verileri güncelleme işlemi yaparken kullanmam gerekir
        Movie movie1 = new Movie(Guid.NewGuid(),"Deneme","",10,DateTime.Now,"img.jpg",2,false,1);
        
        // Kullanıcıdan zorunlu olarak almak istediğim verileri ekleme işlemi yaparken kullanmam gerekir
        Movie movie2 = new Movie("Deneme", "", 10, DateTime.Now, "img.jpg",2, false, 1);

    }
}
