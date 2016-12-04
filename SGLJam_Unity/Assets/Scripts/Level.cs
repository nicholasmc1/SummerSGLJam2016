using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : StateBehaviour {
	public GameObject previousDoor;
	public GameObject nextDoor;
	public bool transition;

	// Use this for initialization
	void Awake () 
	{
        if(SceneManager.sceneCount > 1)
		    DelayAwake ();
	}

	public void SetTransform(Level transitionRoom) {
		transform.position = transitionRoom.nextDoor.GetComponent<TransitionDoors>().initPosition;
		transform.rotation = Quaternion.Inverse(previousDoor.transform.rotation);
	}

	void DelayAwake()
	{
		//yield return new WaitForEndOfFrame ();
		if (transition) {
			Debug.Log ("Transition Level");
			RootSceneManager.Instance.SetTransitionRoom (this);
			nextDoor.SetActive(true);
		} else {
			Debug.Log ("Current Level");
			RootSceneManager.Instance.SetCurrentLevel (this);
			previousDoor.SetActive(false);

		}
	}
}
