using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public const string PARAM_WIN_CORRECT_COUNT = "PARAM_WIN_CORRECT_COUNT";
    public const string PARAM_WIN_WRONG_COUNT = "PARAM_WIN_WRONG_COUNT";

    [SerializeField]
    private int maxTime;

    [SerializeField]
    private int goalHitCount;

    [SerializeField]
    private Text lblTimeLeft;

    private int timeLeft;
    private bool isGameOver;
    private int nextUpdate;

    static LevelManager()
    {
        Instance = null;
    }

    public static LevelManager Instance { get; private set; }

    //Awake is always called before any Start functions
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        this.timeLeft = this.maxTime;
        EventBroadcaster.Instance.AddObserver(EventNames.ON_WIN, this.ShowWinScreen);
    }

    void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EventNames.ON_WIN, this.ShowWinScreen);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGameOver)
            return;

        // If the next update is reached
        if (!(Time.time >= nextUpdate))
            return;

        nextUpdate = Mathf.FloorToInt(Time.time) + 1;
        UpdateTime();
    }

    // Update is called once per second
    private void UpdateTime()
    {
        if (isGameOver)
            return;

        timeLeft--;
        lblTimeLeft.text = "TIME LEFT: " + timeLeft;

        if (timeLeft != 0)
            return;

        ShowGameOver();
    }

    private void ShowWinScreen(Parameters parameters)
    {
        isGameOver = true;
        // Show result screen
        var dlgResult = (ResultDialog)ViewHandler.Instance.Show(ViewNames.DialogNames.RESULT_DIALOG_NAME);

        var correctHits = parameters.GetIntExtra(PARAM_WIN_CORRECT_COUNT, 0);
        var wrongHits = parameters.GetIntExtra(PARAM_WIN_WRONG_COUNT, 0);

        dlgResult.SetResult(correctHits, wrongHits);
    }

    private void ShowGameOver()
    {
        isGameOver = true;
        ViewHandler.Instance.Show(ViewNames.DialogNames.GAMEOVER_DIALOG_NAME);
    }

    public bool IsGameOver()
    {
        return this.isGameOver;
    }

    public int GetGoalHitCount()
    {
        return this.goalHitCount;
    }

}