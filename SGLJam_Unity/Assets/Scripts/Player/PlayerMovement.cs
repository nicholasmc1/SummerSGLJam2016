using UnityEngine;
using System.Collections;

public class PlayerMovement: StateBehaviour 
{
	private bool hasInit = false;
	private Rigidbody move = null;
	private CapsuleCollider myCol;
//	[HideInInspector]
//	public PlayerInteraction actions;

	//states
	public enum movementState { standing, switching, blink };
	public movementState _movState;
	public bool grounded = true;

	//rawmovement variables
	private float moveSpeed = 5.5f;
	private float sprintSpeed = 9.5f;
	public float gravity = 100f;

	[HideInInspector]
	public Vector3 newMove;

	//camera
	private Vector3 newLook;
	[HideInInspector]
	public Transform head;
	private float sens = 5f;

	//crouching info
	private float heightTarget = 2f;
	public float eyeHeight = 1.2f;

	//grounding
	private Vector3 surfaceNormal = new Vector3(0, 1, 0);
	[HideInInspector]
	public float timeSinceGrounded;
	private Vector3 prevVel;

	public void Awake()
	{
		move = GetComponent<Rigidbody>();
		myCol = GetComponentInChildren<CapsuleCollider>();
		hasInit = true;
		newLook = transform.rotation.eulerAngles;

	}

	public override void UpdatePlaying()
	{
		timeSinceGrounded += Time.deltaTime;
		if (timeSinceGrounded > 0.1f)
		{
			grounded = false;
			surfaceNormal = Vector3.up;
		}
		else if(_movState != movementState.blink)
			grounded = true;

		transform.rotation = Quaternion.Euler(0, newLook.y, 0);

		if (PlayerCore._instance.playerState != PlayerCore.inputState.ragdoll) {
			//if (!PlayerCore._instance.peeking) {
				head.position = new Vector3 (transform.position.x, transform.position.y + eyeHeight, transform.position.z);
				head.rotation = Quaternion.Euler(newLook);
			//}

		}

		Vector3 flatMag = move.velocity;
		flatMag.y = 0;
	}

	public void MovePlayer(Vector3 dir)
	{
		if (grounded)
		{
			newMove.x = dir.x;
			newMove.z = dir.z;

			newMove = new Vector3(transform.TransformDirection(newMove).x * moveSpeed, newMove.y, transform.TransformDirection(newMove).z * moveSpeed);
			if(_movState != movementState.blink)
				newMove = Vector3.ProjectOnPlane(newMove, surfaceNormal);

		}
		if (!grounded)
			newMove = newMove + (-surfaceNormal / 2 * gravity * Time.deltaTime);

		else if (_movState == movementState.standing)
			newMove = newMove + Vector3.ProjectOnPlane(new Vector3(0, -0.01f, 0), surfaceNormal);

		//newMove = Vector3.ClampMagnitude(newMove, 30);
	}

	public void CameraMove(Vector3 dir)
	{
		newLook.x += dir.x * sens;
		newLook.x = Mathf.Clamp(newLook.x , -89, 89);
		newLook.y += dir.y * sens;
		newLook.y = newLook.y + 360;
		newLook.y = newLook.y % 360f;

	}
	public override void FixedUpdatePlaying()
	{
//		if (hasInit && !GameController.instance.paused)
//		if (PlayerCore._instance.playerState == PlayerCore.inputState.free && actions._state == PlayerInteraction.grabState.empty)
		if (grounded) {
			move.velocity = newMove;
		}
        if (PlayerCore._instance.playerState == PlayerCore.inputState.free)
            prevVel = move.velocity;
        else
            prevVel = Vector3.zero;
//		else
//			move.velocity = new Vector3(0, 0, 0);

	}

	void OnCollisionStay(Collision col)
	{
		//to improve cresting movement raycast ahead of the player and detect surface normal where they are going not where they are. 

		if (_movState != movementState.blink)
		{
			int counter = 0;
			Vector3 avgNormal = new Vector3(0, 0, 0);
			foreach (ContactPoint con in col.contacts)
				if (con.normal.y > 0.2f && con.point.y < myCol.bounds.center.y)
				{
					timeSinceGrounded = 0;
					counter++;
					avgNormal += con.normal;
				}
			if (counter != 0)
				surfaceNormal = avgNormal / counter;
			else
				surfaceNormal = new Vector3(0, 1, 0);
		}
	}

	void OnCollisionEnter(Collision col)
	{
        if (PlayerCore._instance.playerState != PlayerCore.inputState.ragdoll)
        {
            if (prevVel.magnitude >= 20)
            {
                Vector3 newNormal = new Vector3(0, 0, 0);
                foreach (ContactPoint norm in col.contacts)
                    newNormal += norm.normal;
                newNormal = newNormal / col.contacts.Length;
                if (Vector3.Dot(newNormal, prevVel) < -10)
                {
                    PlayerCore._instance.Die();
                    Debug.Log(prevVel + "VEL");
                }
            }
        }

	}

}