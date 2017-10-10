using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {


	[Header("Spawn Details")]
	[SerializeField] int spawnMax;


	[Header("Spawn Reference")]
	[SerializeField] GameObject blockParent;
	[SerializeField] Block blockPrefab;

	[Header("Spawned")]
	[SerializeField] List<GameObject> spawnedEntity;


	// Use this for initialization
	void Start () {


		EventBroadcaster.Instance.AddObserver (BlockEventNames.ON_BLOCK_CLICKED, this.OnBlockClicked);

		for (int i = 0; i < spawnMax; i++) {
			SpawnEntity ();	
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnBlockClicked(Parameters parameter){
		string key = parameter.GetStringExtra (KeyInputHandler.KEY_PRESSED, "");

		if (spawnedEntity.Count <= 0)
			return;

		if (spawnedEntity [spawnedEntity.Count - 1].GetComponent<Block> ().MyColor.Key.ToString () == key) {
			DeSpawnEntity (spawnedEntity [spawnedEntity.Count - 1].gameObject, spawnedEntity.Count - 1);
			EventBroadcaster.Instance.PostEvent (EventNames.ON_CORRECT);
		} else {
			EventBroadcaster.Instance.PostEvent (EventNames.ON_WRONG);
		}

	}

	public void SpawnEntity(){
		GameObject block = Instantiate (blockPrefab.gameObject);
		block.transform.SetParent (blockParent.transform);
		block.transform.localScale = Vector3.one;
		spawnedEntity.Add (block);
	}



	void DeSpawnEntity(GameObject entity, int index){
		GameObject b = entity.gameObject;

		spawnedEntity.RemoveAt (index);
		Destroy (b);
	}
}
