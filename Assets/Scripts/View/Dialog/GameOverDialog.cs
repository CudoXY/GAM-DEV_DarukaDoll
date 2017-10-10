using UnityEngine;
using UnityEngine.UI;

public class GameOverDialog : View
{
    [SerializeField]
    private Button btnPlayAgain;

	void Start(){
	}


    public void btnPlayAgain_Click()
    {
        LoadManager.Instance.LoadScene(SceneNames.PLAYING_SCENE);
    }
}