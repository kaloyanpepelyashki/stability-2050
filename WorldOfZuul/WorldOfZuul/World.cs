namespace WorldOfZuul;

/// <summary>
/// This class represents the parameters of the global state of the game (the world). Keeps track of the global CPU and stability rate
/// </summary>
/// <remarks>
/// The class is a singleton class, meaning it cannot be instantiated. The class a single instance.
/// To get the instance of the class, the GetInstance method must be used
/// </remarks>
public class World
{
    private static World _instance;
    /// <summary>
    /// The stability index of the world
    /// The stability index of the world changes, as the player commits to different choices throughout the game.
    /// Default value is 60
    /// </summary>
    //public int stabilityIndex { get; private set; } = 60;

    /// <summary>
    /// The CPI , taken globally - meaning the overall average CPI of the entire world (all regions taken together)
    /// The global CPI is influenced by player's choices, as the player changes the average of the differet regions, that also influences the global index
    /// </summary>
    public double globalCPI = CpiTracker.Instance.GlobalCpi;
    
    private World() {}

    public static World GetInstance()
    {
        if (_instance == null)
        {
            _instance = new World();
        }
        
        return _instance;
    }
}