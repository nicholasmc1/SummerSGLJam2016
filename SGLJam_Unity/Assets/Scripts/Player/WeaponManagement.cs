using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagement : StateBehaviour {

	public GameObject projectilePrefab;
	public Transform shootOrigin;
	public float speed;
	private Rigidbody playerMove;

    private GameObject _blinkBall;

	void Awake () {
		StartCoroutine (SetPlayer ());
	}

	IEnumerator SetPlayer () {
		yield return new WaitForEndOfFrame ();
		playerMove = PlayerCore._instance.gameObject.GetComponent<Rigidbody> ();
	}

	public BlinkBall Fire() {
		if (_blinkBall != null) {
            Destroy(_blinkBall);
		}
		Debug.Log ("Pew!");
		_blinkBall = Instantiate (projectilePrefab, shootOrigin.position, shootOrigin.rotation) as GameObject;

		_blinkBall.GetComponent<Rigidbody> ().velocity = transform.TransformDirection (new Vector3 (0, 0, speed));
        return _blinkBall.GetComponent<BlinkBall>();
	}
}
