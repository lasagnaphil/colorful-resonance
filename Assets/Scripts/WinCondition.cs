public class WinCondition
{
}

public class EliminationWinCondition : WinCondition
{
}

public class SurvivalWinCondition : WinCondition
{
    public SurvivalWinCondition(int numOfTurns)
    {
        this.numOfTurns = numOfTurns;
    }
    public int numOfTurns;
}