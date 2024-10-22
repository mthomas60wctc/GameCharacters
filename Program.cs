using NLog;
using System.Text.Json;
using System.Reflection;
using NLog.LayoutRenderers;
string path = Directory.GetCurrentDirectory() + "//nlog.config";

// create instance of Logger
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

// deserialize mario json from file into List<Mario>
string marioFileName = "mario.json";
string dkFileName = "dk.json";
string sfFileName = "sf.json";
List<MarioCharacter> marios = [];
List<DonkeyKongCharacter> donkeys = [];
List<StreetFighterCharacter> streets = [];
var selectedList = marios;
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
    string? choice = null;
    string currentUniverse = "";
    do
    {
        // display choices to user
        Console.WriteLine("1) Mario");
        Console.WriteLine("2) Donkey Kong");
        Console.WriteLine("3) Street Fighter");

        choice = Console.ReadLine();

    } while (choice is null || !(choice == "1" || choice == "2" || choice == "3"));

    switch (choice)
    {
        case "1":
            currentUniverse = "Mario";
            break;
        case "2":
            currentUniverse = "Donkey Kong";
            break;
        case "3":
            currentUniverse = "Street Fighter";
            break;
    }

    logger.Info("User choice 1: {Choice}", choice);

    // display choices to user
    Console.WriteLine("1) Display " + currentUniverse + "  Characters");
    Console.WriteLine("2) Add " + currentUniverse + " Character");
    Console.WriteLine("3) Remove " + currentUniverse + " Character");
    Console.WriteLine("Enter to quit");

    // input selection
    choice = Console.ReadLine();
    logger.Info("User choice 2: {Choice}", choice);

    if (choice == "1")
    {
        if (currentUniverse == "Mario")
        {
            // Display Mario Characters
            foreach (MarioCharacter c in marios)
            {
                Console.WriteLine(c.Display());
            }
        }
        else if (currentUniverse == "Street Fighter")
        {
            // Display Mario Characters
            foreach (DonkeyKongCharacter c in donkeys)
            {
                Console.WriteLine(c.Display());
            }
        }
        else if (currentUniverse == "Donkey Kong")
        {
            // Display Mario Characters
            foreach (StreetFighterCharacter c in streets)
            {
                Console.WriteLine(c.Display());
            }
        }
    }
    else if (choice == "2")
    {
        if (currentUniverse == "Mario")
        {
            // Add Mario Character
            // Generate unique ID
            MarioCharacter character = new()
            {
                ID = marios.Count == 0 ? 1 : marios.Max(c => c.ID) + 1
            };
            // Input character
            InputCharacter(character);
            // Add Character
            marios.Add(character);
            File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
        logger.Info($"{currentUniverse} Character added: {character.Name}");
        }
        else if (currentUniverse == "Street Fighter")
        {
            // Add Street Fighter Character
            // Generate unique ID
            StreetFighterCharacter character = new()
            {
                ID = streets.Count == 0 ? 1 : streets.Max(c => c.ID) + 1
            };
            // Input character
            InputCharacter(character);
            // Add Character
            streets.Add(character);
            File.WriteAllText(sfFileName, JsonSerializer.Serialize(streets));
        logger.Info($"{currentUniverse} Character added: {character.Name}");
        }
        else if (currentUniverse == "Donkey Kong")
        {
            // Add Donkey Kong Character
            // Generate unique ID
            DonkeyKongCharacter character = new()
            {
                ID = donkeys.Count == 0 ? 1 : donkeys.Max(c => c.ID) + 1
            };
            // Input character
            InputCharacter(character);
            // Add Character
            donkeys.Add(character);
            File.WriteAllText(dkFileName, JsonSerializer.Serialize(donkeys));
        logger.Info($"{currentUniverse} Character added: {character.Name}");
        }
    }
    else if (choice == "3")
    {
        if (currentUniverse == "Mario")
        {
            // Remove Mario Character
            Console.WriteLine("Enter the ID of the character to remove:");
            if (UInt32.TryParse(Console.ReadLine(), out UInt32 ID))
            {
                MarioCharacter? character = marios.FirstOrDefault(c => c.ID == ID);
                if (character == null)
                {
                    logger.Error($"{currentUniverse} Character ID {ID} not found");
                }
                else
                {
                    logger.Info($"{currentUniverse} Character ID {ID} found");
                    marios.Remove(character);
                    // serialize list<marioCharacter> into json file
                    File.WriteAllText(marioFileName, JsonSerializer.Serialize(marios));
                    logger.Info($"{currentUniverse} Character ID {ID} removed");
                }
            }
            else
            {
                logger.Error("InvalID ID");
            }

        }
        else if (currentUniverse == "Street Fighter")
        {
            // Remove Street Fighter Character
            Console.WriteLine("Enter the ID of the character to remove:");
            if (UInt32.TryParse(Console.ReadLine(), out UInt32 ID))
            {
                StreetFighterCharacter? character = streets.FirstOrDefault(c => c.ID == ID);
                if (character == null)
                {
                    logger.Error($"{currentUniverse} Character ID {ID} not found");
                }
                else
                {
                    logger.Info($"{currentUniverse} Character ID {ID} found");
                    streets.Remove(character);
                    // serialize list<marioCharacter> into json file
                    File.WriteAllText(sfFileName, JsonSerializer.Serialize(streets));
                    logger.Info($"{currentUniverse} Character ID {ID} removed");
                }
            }
            else
            {
                logger.Error("InvalID ID");
            }

        }
        else if (currentUniverse == "Donkey Kong")
        {
            // Remove Donkey Kong Character
            Console.WriteLine("Enter the ID of the character to remove:");
            if (UInt32.TryParse(Console.ReadLine(), out UInt32 ID))
            {
                DonkeyKongCharacter? character = donkeys.FirstOrDefault(c => c.ID == ID);
                if (character == null)
                {
                    logger.Error($"{currentUniverse} Character ID {ID} not found");
                }
                else
                {
                    logger.Info($"{currentUniverse} Character ID {ID} found");
                    donkeys.Remove(character);
                    // serialize list<marioCharacter> into json file
                    File.WriteAllText(dkFileName, JsonSerializer.Serialize(donkeys));
                    logger.Info($"{currentUniverse} Character ID {ID} removed");
                }
            }
            else
            {
                logger.Error("InvalID ID");
            }

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