
using Denemeler;
using MovieProject.Model.Dtos.Categories;
using MovieProject.Model.Entities;

Console.WriteLine("Hello, World!");

Deneme d1 = new Deneme() {Id =1,Name="asd" };
Deneme d2 = new Deneme() {Id = 1, Name = "asd"};

DenemeRecord d3 = new DenemeRecord { Id = 1, Name = "asd" };
DenemeRecord d4 = new DenemeRecord { Id = 1};
//Console.WriteLine(d4.Name);



Deneme2 deneme2 = new Deneme2(1,"lkasjlkfjalkasjfldsf");
Deneme3 deneme3 = new Deneme3(1,"ASDFG");


string metin = "blo";

Console.WriteLine(metin.AddText("selamlar ben Buse"));

//Console.WriteLine(deneme2);

////Console.WriteLine(d1==d2);



////Console.WriteLine(d3==d4);


////Console.WriteLine(d1);
//Console.WriteLine(d4);




Category Add(Category category)
{
    // veri tabanına ekledikten sonra 
    return category;
}


void Add1(Category category)
{
    //veri tabanına ekle 
}

class Deneme
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"Id: {Id} Name: {Name}";
    }
}





// record : Data taşıyan immutable yapıdadır.
// record struct : struct ın hızlı özelliği ve recordun immutable yapısının ortak halidir
record struct DenemeRecord
{
    public int Id { get; init; }
    public string Name { get; init; }
}

record Deneme2(int Id, string Name);


record  Deneme3
{

    public Deneme3(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; init; }
    public string Name { get; init; }
}
