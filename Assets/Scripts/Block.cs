using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour {

	[SerializeField] Image body;
	[SerializeField] BlockColor myColor;


	// Use this for initialization
	void Start () {
		//insert randomize color here


		myColor = BlockColor.ALL [Random.Range (0, BlockColor.ALL.Length)];
		body.color = myColor.MyColor;
		body.sprite = myColor.BodySprite;

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
