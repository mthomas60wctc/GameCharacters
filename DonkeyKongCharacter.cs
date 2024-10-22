public class DonkeyKongCharacter : Character{
    public string Species { get; set; } = "";

    public override string Display() => base.Display() + "Species: " + this.Species;

}