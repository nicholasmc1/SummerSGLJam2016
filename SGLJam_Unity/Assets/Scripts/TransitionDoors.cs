using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionDoors : StateBehaviour 
{
	public bool opening;
	private float _position;
	public Vector3 initPosition;

	void Awake() {
		initPosition = transform.localPosition;
	}

	public override void FixedUpdatePlaying() {
		if(opening && _position <= 1f) {
			_position += 1f;
		} else if (!opening && _position >= 1f) {
			_position -= 1f;
		}
	}

	public override void UpdatePlaying() {
		transform.localPosition = initPosition + new Vector3 (0, _position, 0);
	}
}
