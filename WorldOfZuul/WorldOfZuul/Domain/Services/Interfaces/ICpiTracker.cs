namespace WorldOfZuul.Interfaces
{
public interface ICpiTracker
{
    double GlobalCpi { get; }
    void IncreaseCpi(Region region);
    void DecreaseCpi(Region region);
    void IncreaseCpi(Region region, int units);
    void DecreaseCpi(Region region, int units);
    bool CheckWinCondition();
    bool CheckCrisisCondition();
}
}