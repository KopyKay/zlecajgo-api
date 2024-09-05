namespace ZlecajGo.Domain.Constants;

public class AppTypes
{
    public int Id { get; }
    public string Name { get; }
    
    private AppTypes(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static readonly AppTypes Task = new(1, "Zlecenie");
    public static readonly AppTypes Service = new(2, "Usługa");

    public override string ToString() => Name;
}