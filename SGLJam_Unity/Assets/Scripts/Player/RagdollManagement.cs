using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManagement : StateBehaviour {

	public GameObject ragdollPrefab;
	private GameObject ragdollParent;
	private Transform _ragdoll;
	private GameObject player;
	private PlayerMovement move;
	private bool _flopping;

	void Awake() {
		StartCoroutine (SetPlayer ());
	}

	IEnumerator SetPlayer() {
		yield return new WaitForEndOfFrame ();
		player = PlayerCore._instance.gameObject;
		move = player.GetComponent<PlayerMovement> ();
	}

	public void Spawn () {
		ragdollParent = Instantiate(ragdollPrefab) as GameObject;
		_ragdoll = ragdollParent.transform.GetChild (2);
		//ragdollParent.SetActive (false);
	}

	public override void UpdatePlaying() {
		if (_flopping) {
			//move.head.position = new Vector3 (_ragdoll.position.x, _ragdoll.position.y + move.eyeHeight, _ragdoll.position.z);
		}
	}

	public void Activate () {
		//ragdollParent.SetActive (true); 
		Destroy(PlayerCore._instance.weapon._blinkBall);
		PlayerCore._instance.weapon._animator.SetFloat ("charge", 0);
		Spawn();
		ragdollParent.transform.position = player.transform.position;
		foreach (Rigidbody rigidbody in ragdollParent.GetComponentsInChildren<Rigidbody> ()) {
			rigidbody.velocity = player.GetComponentInChildren<Rigidbody> ().velocity;
		}
		move.head.gameObject.GetComponent<RagdollCamera> ().lookAtTarget = _ragdoll;
		move.head.gameObject.GetComponentInChildren <SkinnedMeshRenderer> ().enabled = false;
		_flopping = true;
        player.GetComponent<Rigidbody>().isKinematic = true;
	}

	public void Deactivate () {
		player.transform.position = _ragdoll.transform.position;
		//ragdollParent.SetActive (false);
		_flopping = false;
        move.head.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
		player.GetComponent<Rigidbody> ().velocity = _ragdoll.gameObject.GetComponent<Rigidbody> ().velocity;
		Destroy(ragdollParent);
	}

	public void Die() {
		_flopping = false;
		move.head.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
		player.GetComponent<Rigidbody>().isKinematic = false;
		Destroy(ragdollParent);
	}
}
