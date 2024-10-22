using NLog;
using System.Text.Json;
using System.Reflection;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

// deserialize mario json from file into List<Mario>
string marioFileName = "mario.json";
string dkFileName = "mario.json";
string sfFileName = "mario.json";
List<MarioCharacter> marios = [];
List<DonkeyKongCharacter> donkeys = [];
List<StreetFighterCharacter> streets = [];
// check if file exists`
if (File.Exists(marioFileName))
{
    marios = JsonSerializer.Deserialize<List<MarioCharacter>>(File.ReadAllText(marioFileName))!;
    logger.Info($"File deserialized {marioFileName}");
}
if (File.Exists(dkFileName))
{
    donkeys = JsonSerializer.Deserialize<List<DonkeyKongCharacter>>(File.ReadAllText(dkFileName))!;
    logger.Info($"File deserialized {dkFileName}");
}
if (File.Exists(sfFileName))
{
    streets = JsonSerializer.Deserialize<List<StreetFighterCharacter>>(File.ReadAllText(sfFileName))!;
    logger.Info($"File deserialized {sfFileName}");
}

do
{
    // display choices to user
    Console.WriteLine("1) Display Mario Characters");
    Console.WriteLine("2) Add Mario Character");
    Console.WriteLine("3) Remove Mario Character");
    Console.WriteLine("Enter to quit");

    // input selection
    string? choice = Console.ReadLine();
    logger.Info("User choice: {Choice}", choice);

    if (choice == "1")
    {
        // Display Mario Characters
        foreach (var c in marios)
        {
            Console.WriteLine(c.Display());
        }
    }
    else if (choice == "2")
    {
        // Add Mario Character
        // Generate unique ID
        MarioCharacter mario = new()
        {
            ID = marios.Count == 0 ? 1 : marios.Max(c => c.ID) + 1
        };
        // Input character
        InputCharacter(mario);
        // Add Character
        marios.Add(mario);
        File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
        logger.Info($"Character added: {mario.Name}");
    }
    else if (choice == "3")
    {
        // Remove Mario Character
        Console.WriteLine("Enter the ID of the character to remove:");
        if (UInt32.TryParse(Console.ReadLine(), out UInt32 ID))
        {
            MarioCharacter? character = marios.FirstOrDefault(c => c.ID == ID);
            if (character == null)
            {
                logger.Error($"Character ID {ID} not found");
            }
            else
            {
                logger.Info($"Character ID {ID} found");
                marios.Remove(character);
                // serialize list<marioCharacter> into json file
                File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
                logger.Info($"Character ID {ID} removed");
            }
        }
        else
        {
            logger.Error("InvalID ID");
        }
    }
    else if (string.IsNullOrEmpty(choice))
    {
        break;
    }
    else
    {
        logger.Info("InvalID choice");
    }
} while (true);

logger.Info("Program ended");


static void InputCharacter(Character character)
{
    Type type = character.GetType();
    PropertyInfo[] properties = type.GetProperties();
    var props = properties.Where(p => p.Name != "ID");
    foreach (PropertyInfo prop in props)
    {
        if (prop.PropertyType == typeof(string))
        {
            Console.WriteLine($"Enter {prop.Name}:");
            prop.SetValue(character, Console.ReadLine());
        }
        else if (prop.PropertyType == typeof(List<string>))
        {
            List<string> list = [];
            do
            {
                Console.WriteLine($"Enter {prop.Name} or (enter) to quit:");
                string response = Console.ReadLine()!;
                if (string.IsNullOrEmpty(response))
                {
                    break;
                }
                list.Add(response);
            } while (true);
            prop.SetValue(character, list);
        }
    }
}