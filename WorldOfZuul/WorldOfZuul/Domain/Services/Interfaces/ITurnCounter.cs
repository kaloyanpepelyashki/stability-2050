namespace WorldOfZuul.Domain.Services.Interfaces;

public interface ITurnCounter
{
    void IncrementTurn();
    bool CheckOutOfTurns();
}