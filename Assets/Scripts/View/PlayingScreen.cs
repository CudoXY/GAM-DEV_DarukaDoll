using UnityEngine;
using UnityEngine.UI;

public class PlayingScreen : View
{
    [SerializeField]
    private int timeLeft;

    [SerializeField]
    private int correctHits;

    [SerializeField]
    private int wrongHits;

    [SerializeField]
    private Text lblCorrect;

    [SerializeField]
    private Text lblWrong;

    [SerializeField]
    private Text lblTimeLeft;

	private int goalHitCount;

    // Next update in second
    private int nextUpdate = 1;

    private bool isGameOver;

    // Use this for initialization
    private void Start()
    {
		//TODO: Dyan: I'm trying to make this dependent with BlockSpawner
		//goalHitCount = 10;
		goalHitCount = LevelManager.Instance.GetGoalHitCount(); 

        lblTimeLeft.text = "TIME LEFT: " + timeLeft;
        EventBroadcaster.Instance.AddObserver(EventNames.ON_CORRECT, OnCorrect);
        EventBroadcaster.Instance.AddObserver(EventNames.ON_WRONG, OnWrong);


    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_CORRECT);
        EventBroadcaster.Instance.RemoveObserver(EventNames.ON_WRONG);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGameOver)
            return;

        // If the next update is reached
        if (Time.time >= nextUpdate)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            UpdateEverySecond();
        }
    }

    private void OnCorrect()
    {
        correctHits++;
        lblCorrect.text = "CORRECT: " + correctHits;

        if (correctHits != goalHitCount)
            return;

        isGameOver = true;

        // Show result screen
        var dlgResult = (ResultDialog) ViewHandler.Instance.Show(ViewNames.DialogNames.RESULT_DIALOG_NAME);
        dlgResult.SetResult(correctHits, wrongHits);
    }

    private void OnWrong()
    {
        wrongHits++;
        lblWrong.text = "WRONG: " + wrongHits;
    }

    // Update is called once per second
    private void UpdateEverySecond()
    {
        timeLeft--;
        lblTimeLeft.text = "TIME LEFT: " + timeLeft;

        if (timeLeft != 0)
            return;

        ShowGameOver();
    }

    private void ShowGameOver()
    {
        ViewHandler.Instance.Show(ViewNames.DialogNames.GAMEOVER_DIALOG_NAME);
        isGameOver = true;
    }
		
}