using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour {

	[SerializeField]
    public BlockColor Color;

	private Image image;

    public void SetColor(BlockColor color)
    {
        this.image = GetComponent<Image>();
        this.Color = color;

        switch (color)
        {
            case BlockColor.RED:
                this.image.color = UnityEngine.Color.red;
                break;
            case BlockColor.GREEN:
                this.image.color = UnityEngine.Color.green;
                break;
            case BlockColor.BLUE:
                this.image.color = UnityEngine.Color.blue;
                break;
        }

        // TODO: Insert sprite banana
    }


}
