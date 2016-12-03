﻿using UnityEngine;
using System.Collections;
using InControl;

public class PlayerCore : StateBehaviour {
	private PlayerMovement move;
	private WeaponManagement weapon;
	private BlinkBall blinkBall;
	private RagdollManagement ragdoll;

	public GameObject ragdollPrefab;
	public Transform headDirection;
	public enum inputState{free, ragdoll};
	[HideInInspector]
	public bool paused;
	public inputState playerState;
	public PlayerBindings bindings;
	public static PlayerCore _instance;

	void Awake() {
		_instance = this;
		Cursor.lockState = CursorLockMode.Locked;
		move = GetComponent<PlayerMovement>();
		weapon = GetComponentInChildren<WeaponManagement> ();
		GameObject _ragdoll = Instantiate (ragdollPrefab);
		ragdoll = _ragdoll.GetComponent<RagdollManagement> ();
		//actions = GetComponent<PlayerInteraction>();
		//stats = GetComponent<EntityStats>();
		//stats.UpdateHealth(0,0,0);
		move.head = headDirection;
		move.head.transform.parent = null;
		//move.Init();
		//actions.ChangeHotbarItem(0);
		//actions.cameraDirection = headDirection;
		//move.actions = actions;
	}

	public override void UpdatePlaying() {
		bindings = InputManager._instance.bindings;
		if(bindings.pauseGame.WasPressed) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Globals.gameState = GameState.Paused;
		}
		if (playerState == inputState.free) {
			move.MovePlayer(new Vector3(bindings.move.X, 0, bindings.move.Y));

			if (bindings.fire.WasPressed) {
				blinkBall = weapon.Fire ();
			}
//
			if (bindings.blink.WasPressed) {
				if (blinkBall != null) {
					blinkBall.Teleport (gameObject);
				} else {
					// Play an error sound or something;
				}
			}

			if (Input.GetKeyDown(KeyCode.V)) {
				if(QualitySettings.vSyncCount != 0)
					QualitySettings.vSyncCount = 0;
				else
					QualitySettings.vSyncCount = 1;
			}
		}

		move.CameraMove(new Vector3(-bindings.look.Y, bindings.look.X, 0));

		if (bindings.ragdoll.WasPressed) {
			Debug.Log ("Ragdoll toggle");
			if (playerState == inputState.free) {
				playerState = inputState.ragdoll;
				ragdoll.Activate ();
			} else {
				playerState = inputState.free;
				ragdoll.Deactivate ();
			}
		}
	}

	public override void UpdatePaused() {
		if (bindings.pauseGame.WasPressed) {
			Cursor.lockState = CursorLockMode.Locked;
			Globals.gameState = GameState.Playing;
		}
	}
}