using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
	public bool hit;
	public ParticleSystem par;

	void Awake () {
        hit = true;
		StartCoroutine (AwakeDelay ());
	}

	IEnumerator AwakeDelay() {
		yield return new WaitForEndOfFrame ();
		PlayerCore._instance.currentRespawnPoint = this.transform;
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        hit = false;
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider collider) 
    {
        if (UnityEngine.SceneManagement.SceneManager.sceneCount > 1)
        {
            if (collider.gameObject.tag == "Player" && !hit)
            {
                hit = true;
                RootSceneManager.Instance.CloseTransitionRoom();
            }
        }
	}

	public void RespawnFX() {
		if(par != null) {
			par.Play ();
		}
	}
}
