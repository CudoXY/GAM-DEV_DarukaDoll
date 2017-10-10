using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDialog : MonoBehaviour {

    public void btnPlayAgain_Click()
    {
        LoadManager.Instance.LoadScene(SceneNames.PLAYING_SCENE);
    }
}
