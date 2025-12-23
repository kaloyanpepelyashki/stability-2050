namespace WorldOfZuul;

/// <summary>
/// In charge of creating the items for the different regions. The factory method provides a method, for creating items, on a random baisis. 
/// </summary>
public static class ItemFactoryService
{   
    
    /// <summary>
    /// The method decided what type of item to create, the decision is randomised
    /// </summary>
    /// <returns></returns>
    public static Item CreateRandomItem(List<EffectItem> effectItems, List<TokenItem> tokenItems)
    {
    
        Random random = new Random();
        
        
        int randomNumber = random.Next(1, 2);
        
        
        
        switch (randomNumber)
        {
            case 1:
                int randomItemSelector = random.Next(0, effectItems.Count);
                return effectItems[randomItemSelector];
            case 2:
                int randomTokenSelector = random.Next(0, tokenItems.Count);
                return tokenItems[randomTokenSelector];
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}