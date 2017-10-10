using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Base popup inherited by other popups
/// BY: NeilDG
/// </summary>
public class ResultDialog : View
{
    [SerializeField]
    private Button btnPlayAgain;

    [SerializeField]
    private Text lblCorrect;

    [SerializeField]
    private Text lblWrong;

    public void btnPlayAgain_Click()
    {
        LoadManager.Instance.LoadScene(SceneNames.PLAYING_SCENE);
    }

    public void SetResult(int correct, int wrong)
    {
        lblCorrect.text = correct + "";
        lblWrong.text = wrong + "";
    }
}
