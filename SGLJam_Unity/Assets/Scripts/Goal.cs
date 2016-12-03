﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : StateBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public override void UpdatePlaying () {
		
	}

	void OnCollisionEnter (Collision collision)
	{
		if(SceneManager.sceneCount > 1 && collision.gameObject.name == "Player")
			RootSceneManager.Instance.SetupTransitionRoom();
	}
}