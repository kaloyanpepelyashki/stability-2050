using WorldOfZuul.Domain.Interfaces;

namespace WorldOfZuul;

/// <summary>
/// The EffectItem class is useful to the player, as when used, the player can get an effect such as bonus cpi points, when answering a question or extra turns
/// </summary>
public class EffectItem: Item, IUsable
{
    
    public int Id { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public string ItemEffect { get; set; }
    public double Value { get; set; }
    
    
    public EffectItem(int id, string itemName, string itemDescription, string effect, double value) : base(itemName, itemDescription)
    {
        Id = id; 
        ItemName = itemName;
        ItemDescription = itemDescription;
        ItemEffect = effect;
        Value = value;
        
    }
    
    
    /// <summary>
    /// Calls the effect of the EffectItem
    /// </summary>
    public void Use()
    {
        
    }
}