using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagement : StateBehaviour {

	public GameObject projectilePrefab;
	public Transform shootOrigin;
	public bool charging;
	public float maxSpeed;
	public float speed;
	public float chargeSpeed;
	private Rigidbody playerMove;
	private float _timeElapsed;

    private GameObject _blinkBall;

	void Awake () {
		StartCoroutine (SetPlayer ());
	}

	IEnumerator SetPlayer () {
		yield return new WaitForEndOfFrame ();
		playerMove = PlayerCore._instance.gameObject.GetComponent<Rigidbody> ();
	}

	public void PrepareToFire() {
		if (_blinkBall != null) {
			Destroy(_blinkBall);
		}
		charging = true;
		speed = 20;
		_blinkBall = Instantiate (projectilePrefab, shootOrigin.position, shootOrigin.rotation) as GameObject;
		_blinkBall.transform.parent = shootOrigin;
		_blinkBall.GetComponent<Rigidbody> ().isKinematic = true;
	}

	public override void UpdatePlaying() {
		if (charging && _blinkBall != null) {
			if (speed < maxSpeed) {
				speed += Time.deltaTime * chargeSpeed * maxSpeed;
				_timeElapsed += Time.deltaTime;
				Debug.Log ("Speed: " + speed + "  Time: " + _timeElapsed);
			}

		}
	}

	public override void FixedUpdatePlaying() {
//		if (charging && _blinkBall != null) {
//			_blinkBall.transform.position = shootOrigin.position;
//		}
	}

	public BlinkBall Fire() {
		//Debug.Log ("Pew!");
		charging = false;
		Vector3 v = transform.parent.forward * speed;
		v += playerMove.velocity;
		v = Vector3.ClampMagnitude (v, 75);
		//Debug.Log (v);
		_blinkBall.GetComponent<Rigidbody> ().isKinematic = false;
		_blinkBall.transform.parent = null;
		_blinkBall.GetComponent<Rigidbody> ().velocity = v;
		_timeElapsed = 0;
        return _blinkBall.GetComponent<BlinkBall>();
	}
}
