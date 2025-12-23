using WorldOfZuul.Domain.Interfaces;

namespace WorldOfZuul;

/// <summary>
/// The TokenItem class is useful in quizzes, as it assist the player, by eliminating wrong answers. 
/// </summary>
public class TokenItem: Item, IUsable
{
    
    public int Id { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public string ItemEffect { get; set; }

    public TokenItem(int id, string itemName, string itemDesctription, string itemEffect) : base(itemName,
        itemDesctription)
    {
        Id = id;
        ItemName = itemName;
        ItemDescription = itemDesctription;
        ItemEffect = itemEffect;
    }
    
    /// <summary>
    /// Calls the token item
    /// </summary>
    public void Use()
    {
        
    }
}