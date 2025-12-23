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
    public static Item CreateRandomItem()
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 2);

        return randomNumber switch
        {
            0 => new EffectItem("", ""),
            1 => new TokenItem("", "")
        }; 
    }
}