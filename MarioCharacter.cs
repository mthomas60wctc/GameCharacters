public class MarioCharacter : Character{
    public List<string> Alias { get; set; } = [];

    public override string Display()
    {
        return base.Display() + $"Alias: {string.Join(", ", Alias)}\n";
    }
}