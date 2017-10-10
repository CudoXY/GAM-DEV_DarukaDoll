using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockColor {
	// The different values
	public static List<BlockColor> Types = new List<BlockColor>();

	public static readonly BlockColor RED = new BlockColor(0, "Red", Color.red, (Sprite)Resources.LoadAll<Sprite>("Sprites/spritesheet")[0], KeyCode.A);
	public static readonly BlockColor BLUE = new BlockColor(1, "Blue", Color.blue, (Sprite)Resources.LoadAll<Sprite>("Sprites/spritesheet")[5], KeyCode.S);
	public static readonly BlockColor GREEN = new BlockColor(2, "Green", Color.green, (Sprite)Resources.LoadAll<Sprite>("Sprites/spritesheet")[8], KeyCode.D);


	// Use readonly to maintain immutability
	private readonly int id;
	private readonly string colorName;
	private readonly Color myColor;
	private readonly KeyCode key;
	private readonly Sprite bodySprite;

	// This can be used to loop through all planets
	public static BlockColor[] ALL = new BlockColor[] {
		RED, BLUE, GREEN
	};

	// Converts the specified id to a BlockColor instance
	public static BlockColor Convert(int id) {
		for(int i = 0; i < ALL.Length; ++i) {
			if(ALL[i].Id == id) {
				return ALL[i];
			}
		}

		return null;
	}

	// We use a private constructor because this should not be instantiated
	// anywhere else.
	private BlockColor(int id, string colorName, Color myColor, Sprite bodySprite, KeyCode key) {
		this.id = id;
		this.colorName = colorName;
		this.myColor = myColor;
		this.bodySprite = bodySprite;
		this.key = key;
	} 

	public int Id {
		get {
			return id;
		}
	}

	public string ColorName {
		get {
			return colorName;
		}
	}

	public Color MyColor {
		get {
			return myColor;
		}
	}

	public Sprite BodySprite{
		get{
			return bodySprite;
		}
	}

	public KeyCode Key {
		get {
			return key;
		}
	}
}
