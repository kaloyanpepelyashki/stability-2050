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
    private int CurrentItemCount = 0;
    private Dictionary<int, Item> InventoryItems = new Dictionary<int, Item>();
    
    private Inventory(){}

    public static Inventory GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Inventory();
        }
        
        return _instance;
    }
    
    
    /// <summary>
    /// Responsible for adding an item to the list of Invetory Items
    /// </summary>
    /// <returns>True if the item was added and returns false if the item was not added</returns>
    public bool AddItem()
    {
        return true;
    }
    
    /// <summary>
    /// Responsible for removing an item for the list of Inventory Items
    /// </summary>
    /// <returns>Returns true if the item weas removed and returns false if the item was not removed</returns>
    public bool RemoveItem()
    {
        return true;
    }
    
}