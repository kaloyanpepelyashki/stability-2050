namespace WorldOfZuul.DTOs;

public class EffectItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Effect { get; set; }
    public double Value { get; set; }
    public string Description { get; set; } = string.Empty;
}