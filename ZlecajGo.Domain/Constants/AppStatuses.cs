namespace ZlecajGo.Domain.Constants;

public class AppStatuses
{
    public int Id { get; }
    public string Name { get; }
    
    private AppStatuses(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public static readonly AppStatuses Pending = new(1, "Oczekujące");
    public static readonly AppStatuses Occupied = new(2, "Zajęte");
    public static readonly AppStatuses Ended = new(3, "Zakończone");
    public static readonly AppStatuses Planned = new(4, "Zaplanowane");
    public static readonly AppStatuses Ongoing = new(5, "Trwające");
    public static readonly AppStatuses Cancelled = new(6, "Anulowane");
    public static readonly AppStatuses Completed = new(7, "Ukończone");

    public override string ToString() => Name;
}