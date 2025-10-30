using WorldOfZuul.DTOs;

namespace WorldOfZuul;

public interface IRegionDataParser
{
    List<RegionDTO> DeserializeRegionData();
}