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

        var spriteList = Resources.LoadAll<Sprite>("Sprites/spritesheet");
        Sprite sprite = null;

        switch (color)
        {
            case BlockColor.RED:
                sprite = spriteList[5];
                break;
            case BlockColor.GREEN:
                sprite = spriteList[8];
                break;
            case BlockColor.BLUE:
                sprite = spriteList[0];
                break;
        }

        this.image.sprite = sprite;

        // TODO: Insert sprite banana
    }
}
