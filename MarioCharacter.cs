public class MarioCharacter : Character{
    public List<string> Alias { get; set; } = [];

    public override string Display() => base.Display() + $"Alias: {string.Join(", ", Alias)}\n";
    
}