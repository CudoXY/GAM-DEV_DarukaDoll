using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour {

	[SerializeField] BlockColor myColor;

	Image image;

	// Use this for initialization
	void Start () {
		//insert randomize color here


		myColor = BlockColor.ALL [Random.Range (0, BlockColor.ALL.Length)];
		image = GetComponent<Image>();
		image.color = myColor.MyColor;

	}

	// Update is called once per frame
	void Update () {
		
	}

	public BlockColor MyColor{
		get{
			return myColor;
		}
	}

}
