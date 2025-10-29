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
    private static CpiTracker? _instance = null;
    private int CorrectAnswer = 6; // For the right decision +6 point to regional CPI
    private int WrongAnswer = 9; // // For the wrong decision +6 point to regional CPI
    public double GlobalCpi => CpiValues.Values.Any() ? CpiValues.Values.Average() : 0.0 ;
    private static Dictionary<Region, double> CpiValues { get; } = new();
    private CpiTracker(List<Region> regions)
    {
        foreach (var region in regions)
        {
            Console.WriteLine(region.RegionName);
            Console.ReadKey();
            CpiValues.Add(region, region.RegionCpi);
        }
    }
    
    /// <summary>
    /// This method is used to get the instance of the singleton class CpiTracker.
    /// <param name="regions">Receives the initial CPI values of the regions.</param>
    /// </summary>
    /// <returns>CpiTracker</returns>
    public static CpiTracker GetInstance(List<Region>? regions = null)
    {
        if (_instance != null) return _instance;
        if (regions == null || regions.Count == 0) throw new CpiTrackerNotInitializedException();
        _instance = new CpiTracker(regions);
        return _instance;
    }
    
    /// <summary>
    /// Return the singleton instance of the CpiTracker if initialized.
    /// Throws an exception if not yet initialized.
    /// </summary>
    public static CpiTracker Instance => _instance ?? throw new CpiTrackerNotInitializedException();

    /// <summary>
    /// This method is used to increase the CPI of a certain region.
    /// Increases the global CPI by 1.5%.
    /// <param name="region">The region that will have its CPI increased.</param>
    /// </summary>
    public void IncreaseCpi(Region region)
    {
        region.RegionCpi += CorrectAnswer;
        CpiValues[region] = region.RegionCpi;
    }

    /// <summary>
    /// This method is used to decrease the CPI of a certain region.
    /// Decreases the global CPI by 2.25%.
    /// <param name="region">The region that will have its CPI decreased.</param>
    /// </summary>
    public void DecreaseCpi(Region region)
    {
        region.RegionCpi -= WrongAnswer;
        CpiValues[region] = region.RegionCpi;
    }

    /// <summary>
    /// This method to check if the player has reached a win condition.
    /// </summary>
    /// <returns>True if global is greater than 80.</returns>
    public bool CheckWinCondition()
    {
        return true && GlobalCpi >= 80;
    }

    /// <summary>
    /// This method to check if the player has reached crisis condition.
    /// </summary>
    /// <returns>True if global cpi is less than 20.</returns>
    public bool CheckCrisisCondition()
    {
        return true && GlobalCpi <= 20;
    }
}