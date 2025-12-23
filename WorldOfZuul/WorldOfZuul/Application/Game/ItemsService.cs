using WorldOfZuul.DTOs;

namespace WorldOfZuul;

public class ItemsService
{
    private ItemDataParser _dataParser = new ItemDataParser();
    public ItemsService(ItemDataParser dataParser)
    {
        _dataParser = dataParser;
    }

    public List<EffectItem> GetEffectItems()
    {
        
        List<ItemDataDTO> itemDataDtos = _dataParser.DeserialiseItemData();
        
        List<EffectItem> effectItems = itemDataDtos
            .Where(dto => dto.EffectItemDtos != null)
            .SelectMany(dto => dto.EffectItemDtos!)
            .Select(effectDto => new EffectItem(
                effectDto.Id,
                effectDto.Name,
                effectDto.Description,
                effectDto.Effect,
                effectDto.Value
            ))
            .ToList();
        
        return effectItems;
    }

    public List<TokenItem> GetTokenItems()
    {
        List<ItemDataDTO> itemDataDtos = _dataParser.DeserialiseItemData();
        
        List<TokenItem> tokenItems = itemDataDtos
            .Where(dto => dto.TokenItemDtos != null)
            .SelectMany(dto => dto.TokenItemDtos!)
            .Select(tokenDto => new TokenItem(
                tokenDto.Id,
                tokenDto.Name,
                tokenDto.Description,
                tokenDto.Effect
            ))
            .ToList();
        
        return tokenItems;
    }
}