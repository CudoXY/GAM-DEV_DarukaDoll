using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {


	[Header("Spawn Reference")]
	[SerializeField] GameObject blockParent;
	[SerializeField] Block blockPrefab;

	[Header("Spawned")]
	[SerializeField] List<GameObject> spawnedEntity;

	bool isInitialized = false;

	// Use this for initialization
	void Start () {
		//add observer that notifies ui whenever block is clicked
		EventBroadcaster.Instance.AddObserver (BlockEventNames.ON_BLOCK_CLICKED, this.OnBlockClicked);

		for (int i = 0; i < LevelManager.Instance.GetGoalHitCount(); i++) {
			SpawnEntity ();	
		}



	}

	void OnBlockClicked(Parameters parameter){
		string key = parameter.GetStringExtra (KeyInputHandler.KEY_PRESSED, "");

		if (CheckIfNoBlock()) {
			return;
		}

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

	bool CheckIfNoBlock(){
		if (spawnedEntity.Count <= 0) {
			EventBroadcaster.Instance.PostEvent (EventNames.ON_WIN);
			return true;
		}
		return false;
	}
}
