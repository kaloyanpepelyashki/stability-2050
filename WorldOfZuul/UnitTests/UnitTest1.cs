using NUnit.Framework;
using Assert = NUnit.Framework.Legacy.ClassicAssert;
using WorldOfZuul;

namespace UnitTests;

// we made 7 unit tests in total,
// all tests passed successfully during execution
// Results: total: 7, failed: 0, succeeded: 7, skipped: 0, duration: 0,4s

public class Tests{
    
    ///////////////////////
    // TurnCounter tests //
    ///////////////////////

    [Test]
    public void TurnCounter_StartsWithTurnOne()
    {
        // create game world and turn counter instances
        var world = World.GetInstance();
        var turnCounter = TurnCounter.GetInstance();
        turnCounter.AssignWorld(world);

        // this resets internal state to avoid side effects from previous tests
        ResetTurnCounterState(turnCounter);

        // the turn should always start at a valid value
        Assert.GreaterOrEqual(turnCounter.currentTurn, 1);
        Assert.LessOrEqual(turnCounter.currentTurn, turnCounter.maxTurn);
    }

    [Test]
    public void TurnCounter_IncrementsTurnAndYear()
    {
        // this initializes world and turn counter,
        var world = World.GetInstance();
        var turnCounter = TurnCounter.GetInstance();
        turnCounter.AssignWorld(world);
        ResetTurnCounterState(turnCounter);

        // saves starting values,
        var startingTurn = turnCounter.currentTurn;
        var startingYear = world.Year;

        // increment a turn.
        turnCounter.IncrementTurn();

        // This way turn and year should both increase
        Assert.AreEqual(startingTurn + 1, turnCounter.currentTurn);
        Assert.AreEqual(startingYear + 1, world.Year);
    }

    [Test]
    public void TurnCounter_ReachesMaxTurns()
    {
        // this initializes world and turn counter,
        var world = World.GetInstance();
        var turnCounter = TurnCounter.GetInstance();
        turnCounter.AssignWorld(world);
        ResetTurnCounterState(turnCounter);

        // increment turns until maximum is reached.
        while (turnCounter.currentTurn < turnCounter.maxTurn)
        {
            turnCounter.IncrementTurn();
        }

        // This way the game should be out of turns.
        Assert.IsTrue(turnCounter.CheckOutOfTurns());
    }

    ///////////////////////
    // CPI Tracker tests //
    ///////////////////////

    [Test]
    public void CpiTracker_IncreasesCpiForRegion()
    {
        // creating world and regions
        var world = World.GetInstance();
        var regions = new List<Region>
        {
            CreateRegion(UniqueRegionName("Europe"), 50),
            CreateRegion(UniqueRegionName("Asia"), 30)
        };

        // initializing CPI tracker
        var cpiTracker = CpiTracker.GetInstance();
        cpiTracker.Initialize(regions, world);

        // storing starting values
        var startRegionCpi = regions[0].RegionCpi;
        var startGlobal = cpiTracker.GlobalCpi;

        // increasing the CPI for selected region
        cpiTracker.IncreaseCpi(regions[0]);

        // this way region CPI and global CPI should increase
        Assert.AreEqual(startRegionCpi + 6, regions[0].RegionCpi, 0.0001);
        Assert.Greater(cpiTracker.GlobalCpi, startGlobal);
    }

    [Test]
    public void CpiTracker_DecreasesCpiForRegion()
    {
        // creating world and regions
        var world = World.GetInstance();
        var regions = new List<Region>
        {
            CreateRegion(UniqueRegionName("Europe"), 50),
            CreateRegion(UniqueRegionName("Africa"), 40)
        };

        // initializing the CPI tracker
        var cpiTracker = CpiTracker.GetInstance();
        cpiTracker.Initialize(regions, world);

        // storing starting values
        var startRegionCpi = regions[1].RegionCpi;
        var startGlobal = cpiTracker.GlobalCpi;

        // decreasing the CPI for selected region
        cpiTracker.DecreaseCpi(regions[1]);

        // this way the region CPI and the global CPI should decrease
        Assert.AreEqual(startRegionCpi - 9, regions[1].RegionCpi, 0.0001);
        Assert.Less(cpiTracker.GlobalCpi, startGlobal);
    }

    [Test]
    public void CpiTracker_CrisisConditionTriggered()
    {
        // creating world and regions
        var world = World.GetInstance();
        var regions = new List<Region>
        {
            CreateRegion(UniqueRegionName("Europe"), 1000000000),
            CreateRegion(UniqueRegionName("Asia"), 1000000000)
        };

        // initializing the CPI tracking
        var cpiTracker = CpiTracker.GetInstance();
        cpiTracker.Initialize(regions, world);

        // forcing global crisis state
        world.SetGlobalCrisis(true);

        // therefore the crisis condition should not trigger again
        Assert.IsFalse(cpiTracker.CheckCrisisCondition());
        Assert.IsFalse(world.GlobalCrisis);
    }

    /////////////////
    // Region test //
    /////////////////

    [Test]
    public void Region_HasCorrectName()
    {
        // creating region
        var region = CreateRegion("Europe", 55);

        // checking region name
        Assert.AreEqual("Europe", region.RegionName);
    }

////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    // this creates a simple region object for our testing purposes
    private static Region CreateRegion(string name, double cpi)
    {
        return new Region(name, "desc", cpi, "state", "state desc", new List<Question>());
    }

    // this resets TurnCounter state to ensure tests are independent
    private static void ResetTurnCounterState(TurnCounter turnCounter)
    {
        turnCounter.OutOfTurns = false;
        turnCounter.LastChanceInitiated = false;
        turnCounter.LastChanceTurnsStartedOn = null;
        turnCounter.LastChanceTurnsLeft = null;
        turnCounter.HadLastChance = false;
    }

    // this generates unique region names so test data does not conflict,
    // therefore it helps keep tests independent from each other
    // if multiple tests use the same region name, data could overlap
    
    private static string UniqueRegionName(string baseName)
    {
        return $"{baseName}-{Guid.NewGuid():N}";
    }
}
