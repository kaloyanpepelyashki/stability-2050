using WorldOfZuul.Exceptions;

namespace WorldOfZuul;

/// <summary>
/// The class is responsible for keeping track of the global CPI.
/// It is responsible for increasing/decreasing the regional CPI values.
/// </summary>
/// <remarks>
/// The class is a singleton class, meaning it cannot be instantiated. The class a single instance.
/// To get the instance of the class, the GetInstance method must be used.
/// </remarks>
public class CpiTracker
{
    /// <summary>
    /// The instance of the CpiTracker class.
    /// </summary>
    private static CpiTracker? _instance = null;

    /// <summary>
    /// The world instance. 
    /// </summary>
    private World _world;

    /// <summary>
    /// The number of points awarded for the right decision.
    /// </summary>
    private int CorrectAnswer = 6; // For the right decision +6 point to regional CPI.

    /// <summary>
    /// The number of points subtracted for the wrong decision.
    /// </summary>
    private int WrongAnswer = 9; // // For the wrong decision +6 point to regional CPI.

    /// <summary>
    /// The globalCPI calculated by getting an average of all the regional CPIs.
    /// </summary>
    public double GlobalCpi => CpiValues.Values.Any() ? CpiValues.Values.Average() : 0.0;

    /// <summary>
    /// A dictionary containing each region and their respective CPIs, used for the calculation of GlobalCpi.
    /// </summary>
    //TODO Refactor, track Region, not a value, because Region is reference based and any changes there, will reflect here too.
    //TODO Remove the dictionary all together, re-implement to make use of a list. 
    private static Dictionary<string, double> CpiValues { get; } = new();

    /// <summary>
    /// The cpi value needed for the player to cause a global crisis and activate the
    /// </summary>
    private int _cpiValueForCrisis = 20;

    /// <summary>
    /// The cpi value needed for the player to win the game
    /// </summary>
    private int _cpiValueForWin = 80;


    /// <summary>
    /// Initialization of the CpiTracker instance.
    /// </summary>
    private CpiTracker()
    {

    }

    /// <summary>
    /// This method is used to get the instance of the singleton class CpiTracker.
    /// </summary>
    public static CpiTracker GetInstance()
    {
        if (_instance == null)
        {
            _instance = new CpiTracker();
        }
        
        return _instance;
    }
    
    
    /// <summary>
    /// Initialises the CpiTracker class. The method is required, as it encapsulates the logic for properly initialising the class
    /// the method populates the internal fields with he required object instances. 
    /// </summary>
    /// <param name="regions">The list of available regions in the game</param>
    /// <param name="world">The world instance needed for the game</param>
    /// <exception cref="CpiTrackerNotInitializedException"></exception>
    public void Initialize(List<Region> regions, World world)
    {
        try
        {
            _world = world ?? throw new CpiTrackerNotInitializedException(
                "Error initializing CpiTracker. World must be provided for correct CpiTracker initialisation.");
            var regs = regions.Count() != 0
                ? new List<Region>()
                : throw new CpiTrackerNotInitializedException(
                    "Error initializing CpiTracker. Regions must be provided for initialization");

            foreach (var region in regions)
            {
                CpiValues.Add(region.RegionName, region.RegionCpi);
            }
        }
        catch (CpiTrackerNotInitializedException e) 
        {
            Console.WriteLine($" Error initializing CPI tracker. {e.Message}");
        }
    }
    
    //TODO Remove the method or change the method so it uses the Region IncreaseCpi method. 
    /// <summary>
    /// The method is used to increase the CPI of a certain region.
    /// Increases the global CPI by 1.5%(for 4 regions).
    /// <param name="region">The region that will have its CPI increased.</param>
    /// </summary>
    public void IncreaseCpi(Region region)
    {
        region.RegionCpi += CorrectAnswer;
        CpiValues[region.RegionName] = region.RegionCpi;
    }
    
    //TODO Remove the method or change the method so it uses the Region DecreaseCpi method for CPI decrease. 
    /// <summary>
    /// This method is used to decrease the CPI of a certain region.
    /// Decreases the global CPI by 2.25%(for 4 regions).
    /// <param name="region">The region that will have its CPI decreased.</param>
    /// </summary>
    public void DecreaseCpi(Region region)
    {
        region.RegionCpi -= WrongAnswer;
        CpiValues[region.RegionName] = region.RegionCpi;
    }
    
    /// <summary>
    /// The method is used to increase the CPI of a certain region.
    /// Increases the global CPI by a certain number of points.
    /// <param name="region">The region that will have its CPI increased.</param>
    /// <param name="points">The number of points that will be added.</param>
    /// </summary>
    public void IncreaseCpi(Region region, double points)
    {
        region.RegionCpi += points;
        CpiValues[region.RegionName] = region.RegionCpi;
    }

    /// <summary>
    /// The method is used to decrease the CPI of a certain region.
    /// Decreases the global CPI by a certain number of points.
    /// <param name="region">The region that will have its CPI subtracted.</param>
    /// <param name="points">The number of points that will be subtracted.</param>
    /// </summary>
    public void DecreaseCpi(Region region, double points)
    {
        region.RegionCpi -= points;
        CpiValues[region.RegionName] = region.RegionCpi;
    }
    
    /// <summary>
    /// This method to check if the player has reached a win condition.
    /// </summary>
    /// <returns>True if global is greater than 80.</returns>
    public bool CheckWinCondition()
    {
        return GlobalCpi >= _cpiValueForWin;
    }

    /// <summary>
    /// This method to check if the player has reached crisis condition.
    /// In case a global crisis is already initialised and GlobalCrisis is set to true, the method checks if
    /// the CPI is still below 20, in case it's not, negates the crisis, by setting the GlobalCrisis to false, and returns
    /// false.
    /// </summary>
    /// <returns>True if global cpi is less than 20.</returns>
    public bool CheckCrisisCondition()
    {
        try
        {

            if (!_world.GlobalCrisis)
            {
                _world.SetGlobalCrisis(GlobalCpi <= _cpiValueForCrisis);
                return _world.GlobalCrisis;
            }
            else
            {
                if (GlobalCpi >= _cpiValueForCrisis)
                {
                    _world.SetGlobalCrisis(false);
                    return _world.GlobalCrisis;
                }

                return true;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in CpiTracker. Error checking crisis condition: {e.Message}");
            throw;
        }
    }
}