using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkBall : StateBehaviour {

	public float lifetime;
	private PlayerMovement player;
	private Rigidbody _rigid;
	private float timer;
    public ParticleSystem bounceParticle;

	void Awake () {
		_rigid = GetComponent<Rigidbody> ();
		Destroy (gameObject, lifetime);
        
	}

	public void SetPlayer(GameObject obj) {
		if (obj.GetComponent<PlayerMovement>() != null) {
			player = obj.GetComponent<PlayerMovement> ();
		}
	}

	public override void UpdatePlaying() {
		/*if (PlayerCore._instance.peeking) {
			timer += Time.deltaTime;
			if (timer > 0.1f) {
				PlayerCore._instance.weapon.GetComponentInChildren<MeshRenderer> ().enabled = false;
				player.head.position = new Vector3 (transform.position.x, transform.position.y + player.eyeHeight / 2, transform.position.z);
				player.head.rotation = Quaternion.LookRotation (_rigid.velocity.normalized);
			}
		}*/
	}

	public void Teleport(GameObject obj) {
		
		player._movState = PlayerMovement.movementState.blink;
		player.grounded = false;
		player.CameraMove (_rigid.velocity.normalized); 
		obj.transform.position = transform.position;
		StartCoroutine (SetVelocity (obj));
        foreach(MeshRenderer rend in GetComponentsInChildren<MeshRenderer>())
		    rend.enabled = false;
		GetComponent<Collider> ().enabled = false;
		player.timeSinceGrounded = 0;
	}

	IEnumerator SetVelocity(GameObject obj) {
		yield return new WaitForEndOfFrame ();
		obj.GetComponent<Rigidbody>().velocity = _rigid.velocity * 2 / 3;
		yield return new WaitForSeconds (0.1f);
		player._movState = PlayerMovement.movementState.standing;
		Destroy (gameObject);
	}
    void OnCollisionEnter(Collision col)
    {
        PlayBounceEffect(col.contacts[0].point);
    }
    void PlayBounceEffect(Vector3 pos)
    {
        if (bounceParticle != null)
            bounceParticle.Play();

        //play sound here

    }
}
