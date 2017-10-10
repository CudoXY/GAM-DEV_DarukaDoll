using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputHandler : MonoBehaviour {

	public const string KEY_PRESSED = "KEY_PRESSED";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame	
	void Update () {
		CheckKeyboardInput ();

	}


	void CheckKeyboardInput(){
		

		if (!Input.anyKeyDown)
			return;


		foreach(BlockColor color in BlockColor.ALL){
			if (Input.inputString.ToUpper() == color.Key.ToString()) {
				Parameters parameters = new Parameters ();
				parameters.PutExtra (KEY_PRESSED, color.Key.ToString ());
				EventBroadcaster.Instance.PostEvent (BlockEventNames.ON_BLOCK_CLICKED, parameters);
			}
		}

	}


}
