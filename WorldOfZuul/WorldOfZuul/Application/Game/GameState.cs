namespace WorldOfZuul;

public enum GameState
{   
    //Initial Game state
    None,
    Running,
    PlayerWon,
    PlayerOutOfTurns,
    PlayerCausedGlobalCrisis,
    //This is used, when the player has dropped below a certain CPI and has 2 turns to fix it
    PlayerHasLastChance,
    PlayerQuitGame
}