using JetBrains.Annotations;

namespace States
{
    public interface IGameState
    {
        void Enter(GameStateManager gsm);
        void Update(GameStateManager gsm);
        void Exit(GameStateManager gsm);
    }
}