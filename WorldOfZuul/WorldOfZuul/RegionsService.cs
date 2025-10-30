using WorldOfZuul.DTOs;

namespace WorldOfZuul;

/// <summary>
/// In charge of handling all tasks related to regions, their initialisation, mutation etc.
/// </summary>
public class RegionsService
{   
    //TODO - has to be changed, it must be passed as a DI at runtime through the constructor
    private RegionDataParser regionDataParser = new RegionDataParser();
    
    public RegionsService()
    {
        
    }
    
    /// <summary>
    /// In charge of initialsing the regions, their states and their exits
    /// </summary>
    /// <returns>Returns a list of regions</returns>
    public Dictionary<string, Region> InitialiseRegions()
    {
        try
        {
            List<RegionDTO> regionDTOs = regionDataParser.DeserializeRegionData();

            Dictionary<string, Region> regionsDict = ConvertListToDictionary(regionDTOs.Select(dto =>
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
                
                for (int j = 0; j < regionDTOs.Count; j++)
                {
                    if (regionDTOs[j].RegionName ==  currentRegionTitle)
                    { 
                        RegionDTO currentRegionDto =  regionDTOs[j];
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
            throw e; 
        }
    }
    
}