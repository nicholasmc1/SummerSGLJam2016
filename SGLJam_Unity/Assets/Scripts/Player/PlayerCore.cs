using UnityEngine;
using System.Collections;
using InControl;

public class PlayerCore : StateBehaviour {
	private PlayerMovement move;
	private WeaponManagement weapon;

	public Transform headDirection;
	public enum inputState{free, inventory};
	[HideInInspector]
	public bool paused;
	public inputState playerState;
	public PlayerBindings bindings;
	public static PlayerCore _instance;

	void Awake() {
		move = GetComponent<PlayerMovement>();
		weapon = GetComponentInChildren<WeaponManagement> ();
		//actions = GetComponent<PlayerInteraction>();
		//stats = GetComponent<EntityStats>();
		//stats.UpdateHealth(0,0,0);
		move.head = headDirection;
		//move.Init();
		//actions.ChangeHotbarItem(0);
		//actions.cameraDirection = headDirection;
		//move.actions = actions;
	}

	public override void UpdatePlaying() {
		bindings = InputManager._instance.bindings;
		if(bindings.pauseGame.WasPressed) {
			Globals.gameState = GameState.Paused;
		}
		if (playerState == inputState.free) {
			move.MovePlayer(new Vector3(bindings.move.X, 0, bindings.move.Y));
			move.CameraMove(new Vector3(-bindings.look.Y, bindings.look.X, 0));

			if (bindings.attack.WasPressed)
				weapon.Fire ();
//
//			if (bindings.attackUtility.WasPressed)
//				actions.SecondaryAction();

			if (Input.GetKeyDown(KeyCode.V)) {
				if(QualitySettings.vSyncCount != 0)
					QualitySettings.vSyncCount = 0;
				else
					QualitySettings.vSyncCount = 1;
			}
		}
	}

	public override void UpdatePaused() {
		if (bindings.pauseGame.WasPressed) {
			Globals.gameState = GameState.Playing;
		}
	}
}