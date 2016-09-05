using UnityEngine;
using System.Collections;
using UText = UnityEngine.UI.Text;

public class MonsterLeftPanel : MonoBehaviour
{
    public UText text;

    void Update()
    {
        GameStateManager gsm = GameStateManager.Instance;
        text.text = string.Format("Monsters Left:\n{0} / {1}", gsm.monsters.Count, gsm.initialMonsterCount);
    }
}
