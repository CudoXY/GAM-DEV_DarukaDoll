using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BlockSpawner : MonoBehaviour {


	[Header("Spawn Reference")]
	[SerializeField] GameObject blockParent;
	[SerializeField] Block blockPrefab;

	[Header("Spawned")]
	[SerializeField] List<GameObject> spawnedEntity;

	bool isInitialized = false;
    private Random random;

    public static BlockSpawner Instance { get; private set; }

    //Awake is always called before any Start functions
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start ()
	{
        EventBroadcaster.Instance.AddObserver(EventNames.REMOVE_BLOCK, this.DestroyBottom);

	    random = new Random();

		for (var i = 0; i < LevelManager.Instance.GetGoalHitCount(); i++) {
			SpawnEntity ();	
		}

        Debug.Log(GetBottomBlock().Color);
	}

    void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveActionAtObserver(EventNames.REMOVE_BLOCK, this.DestroyBottom);
    }

	public void SpawnEntity(){
		var block = Instantiate (blockPrefab.gameObject);
		block.transform.SetParent (blockParent.transform);
		block.transform.localScale = Vector3.one;

	    var colorList = Enum.GetValues(typeof(BlockColor));
        block.GetComponent<Block>().SetColor((BlockColor) colorList.GetValue(random.Next(0, colorList.Length)));
		spawnedEntity.Add (block);
	}

    public Block GetBottomBlock()
    {
        if (spawnedEntity.Count <= 0)
            return null;

        return spawnedEntity[spawnedEntity.Count - 1].gameObject.GetComponent<Block>();
    }

	void DestroyBottom()
    {
        if (spawnedEntity.Count <= 0)
            return;

	    var block = spawnedEntity[spawnedEntity.Count - 1];
		spawnedEntity.RemoveAt(spawnedEntity.Count - 1);
        Destroy(block);
	}
}
