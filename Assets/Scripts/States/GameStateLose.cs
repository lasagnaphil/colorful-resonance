using UnityEngine;

namespace States
{
    public class GameStateLose : IGameState
    {
        public void Enter(GameStateManager gsm)
        {
            Debug.Log("Player Lose : Press R to restart");
        }

        public void Update(GameStateManager gsm)
        {
			if (PlayerPrefs.GetString ("Restart") == "yes")
            {
                gsm.player.GetComponent<Animator>().SetTrigger("Restart");
                gsm.player.GetComponent<Animator>().SetBool("IsDead", false);
                gsm.ChangeState<GameStateLoad>();
				PlayerPrefs.SetString ("Restart", "no");
            }
        }

        public void Exit(GameStateManager gsm)
        {
        }
    }
}