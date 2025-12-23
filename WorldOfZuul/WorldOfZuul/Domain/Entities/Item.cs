namespace WorldOfZuul;


public class Item
{
    
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    
    public Item(string itemName, string ItemDescription) 
    {
        ItemName = itemName;
    }
}