namespace WorldOfZuul;

public interface IRegionsService
{
    Dictionary<string, Region> InitialiseRegions();
    List<Region> GetRegions();
}