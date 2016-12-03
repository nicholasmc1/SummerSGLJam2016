using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagement : StateBehaviour {

	public GameObject projectilePrefab;
	public Transform shootOrigin;
	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Fire() {
		Debug.Log ("Pew!");
		GameObject projectile = Instantiate (projectilePrefab, shootOrigin.position, shootOrigin.rotation) as GameObject;
		projectile.GetComponent<Rigidbody> ().velocity = transform.TransformDirection (new Vector3 (0, speed, 0));
	}
}
