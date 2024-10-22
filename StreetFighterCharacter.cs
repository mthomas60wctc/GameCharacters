public class StreetFighterCharacter : Character{
    public List<string> Moves { get; set; } = [];

    public override string Display() => base.Display() + $"Moves: {string.Join(", ", Moves)}\n";

}