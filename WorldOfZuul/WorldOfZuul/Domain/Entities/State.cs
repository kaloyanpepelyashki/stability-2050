namespace WorldOfZuul;

//This class was added, in case we decide to implement more functionality for state, it will be easier to expand the code, without changing the logic too much
public class State
{
    public string StateName { get; private set; }
    public string StateDescription { get; private set; }

    public State(string stateName, string stateDescription)
    {
        StateName = stateName;
        StateDescription = stateDescription;
    }
}