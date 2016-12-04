using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour 
{
    public ParticleSystem par;
	void Awake () {
		StartCoroutine (AwakeDelay ());
	}

	IEnumerator AwakeDelay() {
		yield return new WaitForEndOfFrame ();
		PlayerCore._instance.currentRespawnPoint = this.transform;
	}

    public void RespawnFX()
    {
        //PLAY SOUND HERE
        if (par != null)
            par.Play();

    }
	
}
