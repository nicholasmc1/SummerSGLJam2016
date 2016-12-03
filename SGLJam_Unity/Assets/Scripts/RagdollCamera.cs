using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollCamera : StateBehaviour {

	[HideInInspector]
	public Transform lookAtTarget;
	public Vector3 positionOffset;
	public int speed;

	public override void UpdatePlaying () {
		if (PlayerCore._instance.playerState == PlayerCore.inputState.ragdoll) {
			transform.LookAt (lookAtTarget);
		}
	}

	public override void FixedUpdatePlaying () {
		if (PlayerCore._instance.playerState == PlayerCore.inputState.ragdoll) {
			transform.position = Vector3.Lerp (transform.position, lookAtTarget.position + positionOffset, Time.deltaTime * speed);
		}
	}
}
