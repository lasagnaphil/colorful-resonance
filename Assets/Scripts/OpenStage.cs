using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class OpenStage : MonoBehaviour {

    public void LoadStage(string stageName) {
        SceneManager.LoadScene(stageName);
    }
}
