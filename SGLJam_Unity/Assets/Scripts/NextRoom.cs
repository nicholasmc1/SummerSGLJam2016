using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoom : MonoBehaviour {
	public bool hit;

	void OnTriggerEnter(Collider col)
	{
		if (!hit && col.tag == "Player") 
		{
			RootSceneManager.Instance.SetupNextLevel ();
			hit = true;
		}
	}
}
