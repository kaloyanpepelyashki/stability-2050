namespace WorldOfZuul;
/// <summary>
/// This class represents the player the class holds information related to the player of the game.
/// </summary>
/// <remarks>
/// The class is a singleton class, meaning it cannot be instantiated. The class a single instance.
/// To get the instance of the class, the GetTurnCounter method must be used
/// </remarks>
public class Player
{
    private static Player _instance;
    public string Name { get; set; }
    public Inventory Inventory { get; set; }

    private Player()
    {
        Inventory = Inventory.GetInstance();
    }

    public static Player GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Player();
        }
        
        return _instance;
    }

    public void SetPlayerName(string name)
    {
        Name = name;
    }

}