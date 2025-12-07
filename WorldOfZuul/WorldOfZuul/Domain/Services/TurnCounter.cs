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
    private World? _world = null;

    /// <summary>
    /// Tracks at which turn the player got their last two turns chance to fix the game stats
    /// </summary>
    public int? LastChanceTurnsStartedOn = null;
    public bool LastChanceInitiated = false;
    
    /// <summary>
    /// Checks if the player has already been given a last chance to solve a global crisis.
    /// A player is given a last chance once, if they drop below 20 cpi again, game ends. 
    /// </summary>
    public bool HadLastChance = false; 
    
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
    /// Assigns (sets-up) the world
    /// </summary>
    /// <param name="world">The world class instance that is to be used throughout the gamef</param>
    public void AssignWorld(World world)
    {
        _world = world;
    }
    
    /// <summary>
    /// This method is used to increment the current turn number. 
    /// </summary>
    public void IncrementTurn()
    {
        try
        {
            if (_world == null)
            {
                throw new Exception("Error! World has to be assigned to TurnCounter.");
            }

            if (!OutOfTurns)
            {
                currentTurn++;
                _world.IncrementYear();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error incrementing turn: {e.Message}");
        }
    }

    /// <summary>
    /// Checks if the player is out of turns.
    /// Used for both checking if the player is out of turns generally and checking if the player is out of their last two turns
    /// </summary>
    /// <returns></returns>
    public bool CheckOutOfTurns()
    {
        if (LastChanceInitiated)
        {
            if (currentTurn >= LastChanceTurnsStartedOn + 2)
            {
                OutOfTurns = true;
                return true;
            }

            return false;
        }

        if (currentTurn == maxTurn)
        {
                OutOfTurns = true;
                return true;
        }

        return false;
    }

    /// <summary>
    /// Initiates the last two turns a player has to solve a global crisis
    /// The method checks if the player already had last two chance given before, if yes, the player cannot get them again
    /// The method returns true, if the last two chances were given and false if they were not given.
    /// </summary>
    /// <returns>Тrue if the last chance was granted. False if last chance was not granted</returns>
    public bool InitLastTwoTurns()
    {
        try
        {
            if (!HadLastChance)
            {
                if (LastChanceTurnsStartedOn == null)
                {
                    LastChanceTurnsStartedOn = currentTurn;
                    LastChanceInitiated = true;
                    HadLastChance = true;

                    return true;
                }

                throw new Exception("Error! LastChanceTurnStartedOn is not null");
            }

            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error initing last two turns: {e.Message}");
            throw;
        }
    }

    public void ResetLastTwoTurns()
    {
        try
        {
            if (LastChanceInitiated)
            {
                LastChanceTurnsStartedOn = null;
                LastChanceInitiated = false;
                return;
            }

            return;
            
        } catch (Exception e)
        {
            Console.WriteLine($"Error reseting last two turns: {e.Message}");
        }
    }
}