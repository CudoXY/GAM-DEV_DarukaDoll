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
    public KeyCode keyRed;

    [SerializeField]
    public KeyCode keyGreen;

    [SerializeField]
    public KeyCode keyBlue;

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

    private void Update()
    {
        if (!Input.anyKeyDown)
            return;

        var bottomBlock = BlockSpawner.Instance.GetBottomBlock();

        if (bottomBlock == null)
            return;

        var parameters = new Parameters();
        parameters.PutExtra(MatchHandler.PARAM_TARGETCOLOR, (int) bottomBlock.Color);

        var playerControls = new [] {keyRed, keyBlue, keyGreen};

        for (var i = 0; i < playerControls.Length; i++)
        {
            if (!Input.GetKey(playerControls[i]))
                continue;

            parameters.PutObjectExtra(MatchHandler.PARAM_KEY_PRESSED, playerControls[i]);
            EventBroadcaster.Instance.PostEvent(EventNames.ON_BLOCKCLICK, parameters);
            break;
        }
    }

    private void OnCorrect()
    {
        correctHits++;
        lblCorrect.text = "CORRECT: " + correctHits;

        // Shake screen
        CameraShake.Shake(0.25f, 2f);

        if (correctHits < LevelManager.Instance.GetGoalHitCount())
            return;

        // Pass player's score
        var parameters = new Parameters();
        parameters.PutExtra(LevelManager.PARAM_WIN_CORRECT_COUNT, correctHits);
        parameters.PutExtra(LevelManager.PARAM_WIN_WRONG_COUNT, wrongHits);

        EventBroadcaster.Instance.PostEvent(EventNames.ON_WIN, parameters);
    }

    private void OnWrong()
    {
        wrongHits++;
        lblWrong.text = "WRONG: " + wrongHits;

        // Shake screen
        CameraShake.Shake(0.25f, 15f);
    }
}
