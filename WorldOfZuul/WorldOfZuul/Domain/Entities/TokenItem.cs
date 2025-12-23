using WorldOfZuul.Domain.Interfaces;

namespace WorldOfZuul;

/// <summary>
/// The TokenItem class is useful in quizzes, as it assist the player, by eliminating wrong answers. 
/// </summary>
public class TokenItem: Item, IUsable
{
    public TokenItem(string itemName, string itemDesctription) : base(itemName, itemDesctription) {}
    
    /// <summary>
    /// Calls the token item
    /// </summary>
    public void Use()
    {
        
    }
}