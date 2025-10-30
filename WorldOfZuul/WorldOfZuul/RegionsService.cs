using WorldOfZuul.DTOs;

namespace WorldOfZuul;

/// <summary>
/// In charge of handling all tasks related to regions, their initialisation, mutation etc.
/// </summary>
public class RegionsService : IRegionsService
{   
    private RegionDataParser regionDataParser;
    
    public RegionsService(RegionDataParser regionDataParser)
    {
       this.regionDataParser = regionDataParser;
    }
    
    /// <summary>
    /// In charge of initialising the regions, their states and their exits
    /// </summary>
    /// <returns>Returns a list of regions</returns>
    public Dictionary<string, Region> InitialiseRegions()
    {
        try
        {
            List<RegionDTO> regionDtOs = regionDataParser.DeserializeRegionData();

            Dictionary<string, Region> regionsDict = ConvertListToDictionary(regionDtOs.Select(dto =>
                new Region(
                    dto.RegionName,
                    dto.RegionDescription,
                    dto.RegionCPI,
                    dto.State.StateName,
                    dto.State.StateDescription
                )
            ).ToList());
            
            // Iterates over the regions and together with that iterates over the regionDTOs to assign the exits (present in the regionDTOs) to the regions
            foreach(KeyValuePair<string, Region> regionPair in regionsDict)
            {
                string currentRegionTitle = regionPair.Value.RegionName;
                
                for (int j = 0; j < regionDtOs.Count; j++)
                {
                    if (regionDtOs[j].RegionName ==  currentRegionTitle)
                    { 
                        RegionDTO currentRegionDto =  regionDtOs[j];
                        //Before assigning each of the exits from the DTO, it's checked if the exit is not an empty string, to avoid breaking it and throwing an exception that his key doesn't exist in the dictionary
                        Region? northExit = (!string.IsNullOrEmpty(currentRegionDto.Exits.North)) ? regionsDict[currentRegionDto.Exits.North] : null;
                        Region? eastExit = (!string.IsNullOrEmpty(currentRegionDto.Exits.East)) ? regionsDict[currentRegionDto.Exits.East] : null;
                        Region? southExit = (!string.IsNullOrEmpty(currentRegionDto.Exits.South)) ? regionsDict[currentRegionDto.Exits.South] : null;
                        Region? westExit = (!string.IsNullOrEmpty(currentRegionDto.Exits.West)) ? regionsDict[currentRegionDto.Exits.West] : null;
                        
                        //Assigns the (sets) the exits to the region, currently itereated over.
                        regionPair.Value.SetExits(northExit, eastExit, southExit, westExit); 
                    }
                } 
            }

            foreach (KeyValuePair<string, Region> regionPair in regionsDict)
            {   
                Console.WriteLine(regionPair.Value.RegionName);
                foreach (KeyValuePair<string, Region> exit in regionPair.Value.Exits)
                {
                    Console.WriteLine($"{exit.Key} exit : {exit.Value.RegionName}");
                }
            }
            
            return regionsDict;
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
        }

        return new Dictionary<string, Region>();
    }

    private Dictionary<string, Region> ConvertListToDictionary(List<Region> regions)
    {
        try
        {
            Dictionary<string, Region> dict = regions.ToDictionary(reg => reg.RegionName);

            return dict;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error converting list of regions to a dictionary");
            throw; 
        }
    }

    public List<Region> GetRegions()
    {
        try
        {
            List<Region> regions = regionDataParser.DeserializeRegionData().Select(dto =>
                new Region(
                    dto.RegionName,
                    dto.RegionDescription,
                    dto.RegionCPI,
                    dto.State.StateName,
                    dto.State.StateDescription
                )
            ).ToList();
            
            return regions;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return [];
    }
}