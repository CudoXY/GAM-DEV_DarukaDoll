using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchHandler : MonoBehaviour
{

    private Player attachedPlayer;

    public const string PARAM_KEY_PRESSED = "PARAM_KEY_PRESSED";
    public const string PARAM_TARGETCOLOR = "PARAM_TARGETCOLOR";

	// Use this for initialization
	void Start () {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_BLOCKCLICK, this.OnBlockClicked);
	    attachedPlayer = GetComponent<Player>();
	}

    void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EventNames.ON_BLOCKCLICK, this.OnBlockClicked);
    }

    void OnBlockClicked(Parameters parameter)
    {
        if (LevelManager.Instance.IsGameOver())
            return;

        var key = (KeyCode) parameter.GetObjectExtra(PARAM_KEY_PRESSED);

        var targetColor = (BlockColor) parameter.GetIntExtra(PARAM_TARGETCOLOR, 0);
        var isCorrect = false;

        if ((key == attachedPlayer.keyRed && targetColor == BlockColor.RED) ||
            (key == attachedPlayer.keyGreen && targetColor == BlockColor.GREEN) ||
            (key == attachedPlayer.keyBlue && targetColor == BlockColor.BLUE))
        {
            EventBroadcaster.Instance.PostEvent(EventNames.ON_CORRECT);
            EventBroadcaster.Instance.PostEvent(EventNames.REMOVE_BLOCK);
            return;
        }
        
        EventBroadcaster.Instance.PostEvent(EventNames.ON_WRONG);
    }
}
