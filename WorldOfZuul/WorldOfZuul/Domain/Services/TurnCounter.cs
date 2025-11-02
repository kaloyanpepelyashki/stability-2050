using WorldOfZuul.Domain.Services.Interfaces;

namespace WorldOfZuul;

/// <summary>
/// The class responsible for keeping track of the turns in the game. Keeps track of the current turn the player is playing
/// Keeps track of how many turns the player has to play in total.
/// </summary>
/// <remarks>
/// The class is a singleton class, meaning it cannot be instantiated. The class a single instance.
/// To get the instance of the class, the GetTurnCounter method must be used
/// </remarks>
public class TurnCounter : ITurnCounter
{
    private static TurnCounter _instance = null;
    public int currentTurn { get; private set; } = 1;
    public int maxTurn { get; private set; } = 25;
    public bool OutOfTurns = false;

    private TurnCounter()
    {

    }
    
    /// <summary>
    /// This method is used to get the instance of the singleton class TurnCounter
    /// </summary>
    /// <returns>TurnCounter</returns>
    public static TurnCounter GetInstance()
    {
        if (_instance == null)
        {
            _instance = new TurnCounter();
            
        }
        
        return _instance;
    }
    
    /// <summary>
    /// This method is used to increment the current turn number. 
    /// </summary>
    public void IncrementTurn()
    {
        if (!OutOfTurns)
        {
            currentTurn++;
        }
    }

    public void CheckOutOfTurns()
    {
        if(currentTurn == maxTurn)
        {
            OutOfTurns = true; 
        }
    }
}