public abstract class Character{
    public UInt64 ID { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public virtual string Display(){
        return $"Id: {ID}\nName: {Name}\nDescription: {Description}";
    }
}