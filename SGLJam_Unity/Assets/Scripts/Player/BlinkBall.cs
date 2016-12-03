using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkBall : StateBehaviour {

	public float lifetime;
	private PlayerMovement player;

	void Start () {
		Destroy (gameObject, lifetime);
	}

	public void Teleport(GameObject obj) {
		if (obj.GetComponent<PlayerMovement>() != null) {
			player = obj.GetComponent<PlayerMovement> ();
		}
		player._movState = PlayerMovement.movementState.blink;
		player.grounded = false;
		obj.transform.position = transform.position;
		StartCoroutine (SetVelocity (obj));
        foreach(MeshRenderer rend in GetComponentsInChildren<MeshRenderer>())
		    rend.enabled = false;
		GetComponent<Collider> ().enabled = false;
		player.timeSinceGrounded = 0;
	}

	IEnumerator SetVelocity(GameObject obj) {
		yield return new WaitForEndOfFrame ();
		obj.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody> ().velocity * 2 / 3;
		yield return new WaitForSeconds (0.1f);
		player._movState = PlayerMovement.movementState.standing;
		Destroy (gameObject);
	}
}
