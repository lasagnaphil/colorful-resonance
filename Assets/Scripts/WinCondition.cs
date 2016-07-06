﻿public class WinCondition
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

public class EscapeWinCondition : WinCondition
{
    public Vector2i escapePosition;

    public EscapeWinCondition(Vector2i escapePosition)
    {
        this.escapePosition = escapePosition;
    }
}