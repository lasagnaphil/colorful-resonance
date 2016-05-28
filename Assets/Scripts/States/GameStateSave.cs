using UnityEngine;
using Utils;

namespace States
{
    public class GameStateSave : IGameState
    {
        public void Enter(GameStateManager gsm)
        {
            string serializedData = JsonHelper.Serialize<MapData>(gsm.mapData);
            Debug.Log(serializedData);
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