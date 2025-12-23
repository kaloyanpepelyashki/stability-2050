using WorldOfZuul.Domain.Interfaces;

namespace WorldOfZuul;

/// <summary>
/// The EffectItem class is useful to the player, as when used, the player can get an effect such as bonus cpi points, when answering a question or extra turns
/// </summary>
public class EffectItem: Item, IUsable
{
    public EffectItem(string itemName, string itemDescription) : base(itemName, itemDescription)
    {
        
        
    }
    
    
    /// <summary>
    /// Calls the effect of the EffectItem
    /// </summary>
    public void Use()
    {
        
    }
}