using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagement : StateBehaviour {

	public GameObject projectilePrefab;
	public Transform shootOrigin;
	public bool charging;
	public float maxSpeed;
	private float speed;
	private Rigidbody playerMove;

    private GameObject _blinkBall;

	void Awake () {
		StartCoroutine (SetPlayer ());
		speed = 5;
	}

	IEnumerator SetPlayer () {
		yield return new WaitForEndOfFrame ();
		playerMove = PlayerCore._instance.gameObject.GetComponent<Rigidbody> ();
	}

	public override void UpdatePlaying() {
		if (charging && speed < maxSpeed) {
			speed += Time.deltaTime * maxSpeed;
			Debug.Log (speed);
		}
	}

	public BlinkBall Fire() {
		if (_blinkBall != null) {
            Destroy(_blinkBall);
		}
		//Debug.Log ("Pew!");
		charging = false;
		_blinkBall = Instantiate (projectilePrefab, shootOrigin.position, shootOrigin.rotation) as GameObject;
		Vector3 v = transform.parent.forward * speed;
		v += playerMove.velocity;
		v = Vector3.ClampMagnitude (v, 75);
		//Debug.Log (v);
		_blinkBall.GetComponent<Rigidbody> ().velocity = v;
		speed = 5;
        return _blinkBall.GetComponent<BlinkBall>();
	}
}
