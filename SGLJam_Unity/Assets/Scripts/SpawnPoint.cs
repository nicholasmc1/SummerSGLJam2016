using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
	public bool hit;
	public ParticleSystem par;

	void Awake () {
		StartCoroutine (AwakeDelay ());
	}

	IEnumerator AwakeDelay() {
		yield return new WaitForEndOfFrame ();
		PlayerCore._instance.currentRespawnPoint = this.transform;
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider collider) {
		Debug.Log("Ye");
		if(collider.gameObject.tag == "Player" && !hit) {
			hit = true;
			RootSceneManager.Instance.CloseTransitionRoom ();
		}
	}

	public void RespawnFX() {
		if(par != null) {
			par.Play ();
		}
	}
}
