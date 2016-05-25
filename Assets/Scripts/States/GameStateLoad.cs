namespace States
{
    public class GameStateLoad : IGameState
    {
        public void Enter(GameStateManager gsm)
        {
            gsm.LoadGame();
            gsm.ChangeState<GameStatePlay>();
        }

        public void Update(GameStateManager gsm)
        {
        }

        public void Exit(GameStateManager gsm)
        {
            
        }
    }
}