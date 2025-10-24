namespace WorldOfZuul;

/// <summary>
/// This class represents the inventory of the player. In charge of keeping track of the items in player inventory.
/// </summary>
/// <remarks>
/// The class is a singleton class, meaning it cannot be instantiated. The class a single instance.
/// To get the instance of the class, the GetTurnCounter method must be used
/// </remarks>
public class Inventory
{
    private static Inventory _instance = null;
    
    //Should be changed later, both type and the way it is initialised
    public List<int> InventoryItems { get; private set; }  = new List<int>();
    
    private Inventory(){}

    public static Inventory GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Inventory();
        }
        
        return _instance;
    }
}