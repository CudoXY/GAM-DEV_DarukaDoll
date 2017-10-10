using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingScreen : View {

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

	// Next update in second
	private int nextUpdate=1;

    private bool isGameOver;

	// Use this for initialization
	void Start () {
		lblTimeLeft.text = "TIME LEFT: " + timeLeft;	
		EventBroadcaster.Instance.AddObserver (EventNames.ON_CORRECT, OnCorrect);
		EventBroadcaster.Instance.AddObserver (EventNames.ON_WRONG, OnWrong);
	}

	void OnDestroy()
	{
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_CORRECT);
		EventBroadcaster.Instance.RemoveObserver (EventNames.ON_WRONG);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (isGameOver)
	        return;

        // TODO: Remove after implementation
        if (Input.GetKeyUp(KeyCode.Z))
            EventBroadcaster.Instance.PostEvent(EventNames.ON_CORRECT);

        if (Input.GetKeyUp(KeyCode.X))
            EventBroadcaster.Instance.PostEvent(EventNames.ON_WRONG);


		// If the next update is reached
		if (Time.time>=nextUpdate){
			nextUpdate=Mathf.FloorToInt(Time.time)+1;
			UpdateEverySecond();
		}
	}

	void OnCorrect()
	{
		correctHits++;
		lblCorrect.text = "CORRECT: " + correctHits;	
	}

	void OnWrong()
	{
		wrongHits++;
		lblWrong.text = "WRONG: " + wrongHits;	
	}
		
	// Update is called once per second
	void UpdateEverySecond(){
		timeLeft--;
		lblTimeLeft.text = "TIME LEFT: " + timeLeft;

	    if (timeLeft != 0)
            return;

        ShowGameOver();
	}

    void ShowGameOver()
    {
	    ViewHandler.Instance.Show(ViewNames.DialogNames.GAMEOVER_DIALOG_NAME);
	    isGameOver = true;
    }
}
