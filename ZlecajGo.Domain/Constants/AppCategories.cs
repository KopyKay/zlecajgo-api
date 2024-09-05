using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Domain.Constants;

public class AppCategories
{
    public int Id { get; }
    public string Name { get; }
    
    private AppCategories(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public static readonly AppCategories Electrician = new(1, "Elektryk");
    public static readonly AppCategories Handyman = new(2, "Złota rączka");
    public static readonly AppCategories FurnitureAssembly = new(3, "Montaż mebli");
    public static readonly AppCategories Cleaning = new(4, "Sprzątanie");
    public static readonly AppCategories HumanCare = new(5, "Opieka");
    public static readonly AppCategories Renovation = new(6, "Remont");
    public static readonly AppCategories Mechanic = new(7, "Mechanik");
    public static readonly AppCategories Garden = new(8, "Ogród");
    public static readonly AppCategories Plumber = new(9, "Hydraulik");
    public static readonly AppCategories Transport = new(10, "Transport");
    public static readonly AppCategories ItSpecialist = new(11, "Informatyk");
    public static readonly AppCategories Other = new(12, "Pozostałe");

    public override string ToString() => Name;
}