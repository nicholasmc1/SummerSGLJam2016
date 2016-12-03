using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

	void Awake () {
		StartCoroutine (AwakeDelay ());
	}

	IEnumerator AwakeDelay() {
		yield return new WaitForEndOfFrame ();
		PlayerCore._instance.currentRespawnPoint = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
