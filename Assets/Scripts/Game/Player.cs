using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {


    [SerializeField]
    private Text lblCorrect;

    [SerializeField]
    private Text lblWrong;

    [SerializeField]
    private KeyCode keyRed;

    [SerializeField]
    private KeyCode keyGreen;

    [SerializeField]
    private KeyCode keyBlue;

    private int correctHits;
    private int wrongHits;

    // Use this for initialization
    private void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_CORRECT, this.OnCorrect);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_WRONG, this.OnWrong);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EventNames.ON_CORRECT, this.OnCorrect);
        EventBroadcaster.Instance.RemoveActionAtObserver(EventNames.ON_WRONG, this.OnWrong);
    }

    // Update is called once per frame
    private void Update()
    {
        if (LevelManager.Instance.IsGameOver())
            return;

        // TODO: Remove after implementation
        if (Input.GetKeyUp(keyRed))
            EventBroadcaster.Instance.PostEvent(EventNames.ON_CORRECT);

        if (Input.GetKeyUp(keyGreen))
            EventBroadcaster.Instance.PostEvent(EventNames.ON_WRONG);
    }


    private void OnCorrect()
    {
        correctHits++;
        lblCorrect.text = "CORRECT: " + correctHits;
        if (correctHits < LevelManager.Instance.GetGoalHitCount())
            return;

        // Pass player's score
        var parameters = new Parameters();
        parameters.PutExtra(LevelManager.PARAM_WIN_CORRECT_COUNT, correctHits);
        parameters.PutExtra(LevelManager.PARAM_WIN_WRONG_COUNT, wrongHits);

        EventBroadcaster.Instance.PostEvent(EventNames.PLAYER_WIN, parameters);
    }

    private void OnWrong()
    {
        wrongHits++;
        lblWrong.text = "WRONG: " + wrongHits;
    }
}
