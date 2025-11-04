namespace WorldOfZuul.Domain.Services.Interfaces;

public interface ITurnCounter
{
    void IncrementTurn();
    void CheckOutOfTurns();
}