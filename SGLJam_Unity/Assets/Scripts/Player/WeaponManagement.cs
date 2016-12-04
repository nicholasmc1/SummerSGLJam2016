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
	private float _chargeAdd;
	private float _scale;
	private Animator _animator;

    private GameObject _blinkBall;
	private GameObject _particleChild;

	void Awake () {
		StartCoroutine (SetPlayer ());
		_animator = GetComponentInChildren<Animator> ();
	}

	IEnumerator SetPlayer () {
		yield return new WaitForEndOfFrame ();
		playerMove = PlayerCore._instance.gameObject.GetComponent<Rigidbody> ();
	}

	public void PrepareToFire() {
		if (_blinkBall != null) {
			Destroy(_blinkBall);
		}
		_animator.SetFloat ("charge", 1);
		charging = true;
		speed = 20;
		_blinkBall = Instantiate (projectilePrefab, shootOrigin.position, shootOrigin.rotation) as GameObject;
		_particleChild = _blinkBall.GetComponentInChildren<ParticleSystem> ().gameObject;
		_blinkBall.transform.parent = shootOrigin;
		_particleChild.SetActive (false);
		_blinkBall.GetComponent<Rigidbody> ().isKinematic = true;
	}

	public override void UpdatePlaying() {
		if (charging && _blinkBall != null) {
			if (speed < maxSpeed) {
				_chargeAdd = Time.deltaTime * chargeSpeed * maxSpeed;
				speed += Time.deltaTime * chargeSpeed * maxSpeed;
				_timeElapsed += Time.deltaTime;
				//Debug.Log ("Speed: " + speed + "  Time: " + _timeElapsed);
			}
		}
	}

	public BlinkBall Fire() {
		//Debug.Log ("Pew!");
		Cursor.lockState = CursorLockMode.Locked;
		charging = false;
		Vector3 v = transform.parent.forward * speed;
		v += playerMove.velocity;
		v = Vector3.ClampMagnitude (v, 75);
		//Debug.Log (v);
		_blinkBall.transform.parent = null;
		_blinkBall.GetComponent<Rigidbody> ().isKinematic = false;
		_particleChild.SetActive (true);
		_animator.SetFloat ("charge", 0);
		_animator.SetTrigger ("shoot");
		_blinkBall.GetComponent<Rigidbody> ().velocity = v;
		_timeElapsed = 0;
        return _blinkBall.GetComponent<BlinkBall>();
	}
}
