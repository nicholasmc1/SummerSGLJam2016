using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
	public GameObject previousDoor;
	public GameObject nextDoor;
	public bool transition;

	// Use this for initialization
	void Start () {
		if (transition) {
			RootSceneManager.GetInstance ().SetTransitionRoom (this);
		} else {
			RootSceneManager.GetInstance ().SetCurrentLevel (this);
		}
	}
}
