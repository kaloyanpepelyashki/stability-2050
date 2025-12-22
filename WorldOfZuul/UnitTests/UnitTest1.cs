using NUnit.Framework;
using Assert = NUnit.Framework.Legacy.ClassicAssert;
using WorldOfZuul;

namespace UnitTests;


[TestFixture]
public class Tests{
    private World _world = null!;
    private TurnCounter _turnCounter = null!;

    [SetUp]
    public void SetUp()
    {
        _world = World.GetInstance();
        _turnCounter = TurnCounter.GetInstance();

        _turnCounter.AssignWorld(_world);

        ResetTurnCounterState(_turnCounter);
    }
    
    private static void ResetTurnCounterState(TurnCounter turnCounter)
    {
        turnCounter.OutOfTurns = false;
        turnCounter.LastChanceInitiated = false;
        turnCounter.LastChanceTurnsStartedOn = null;
        turnCounter.LastChanceTurnsLeft = null;
        turnCounter.HadLastChance = false;
    }
}
