using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManagement : StateBehaviour {

	public GameObject projectilePrefab;
	public Transform shootOrigin;
	public float speed;

    private GameObject _blinkBall;

	public BlinkBall Fire() {
		if (_blinkBall != null) {
            Destroy(_blinkBall);
		}
		Debug.Log ("Pew!");
		_blinkBall = Instantiate (projectilePrefab, shootOrigin.position, shootOrigin.rotation) as GameObject;
        _blinkBall.GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(0, speed, 0));
        return _blinkBall.GetComponent<BlinkBall>();
	}
}
