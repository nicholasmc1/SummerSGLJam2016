using UnityEngine;
using System.Collections;
using InControl;

public class PlayerCore : StateBehaviour {
	[HideInInspector]
	public PlayerMovement move;
	[HideInInspector]
	public WeaponManagement weapon;
	private BlinkBall blinkBall;
	[HideInInspector]
	public RagdollManagement ragdoll;
	[HideInInspector]
	public bool peeking;
	public Transform currentRespawnPoint;
	public GameObject ragdollPrefab;
	public Transform headDirection;
	public enum inputState{free, ragdoll};
	[HideInInspector]
	public bool paused;
	public inputState playerState;
	public PlayerBindings bindings;
	public static PlayerCore _instance;

	public float shootTimer;
	public GameObject HUDPrefab;
	public GameObject PauseMenuPrefab;
	private GameObject _pauseMenu;
    public LayerMask hitMask;

	void Awake() 
	{
        if (PlayerCore._instance == null)
        {
            _instance = this;
            Cursor.lockState = CursorLockMode.Locked;
            move = GetComponent<PlayerMovement>();
            weapon = GetComponentInChildren<WeaponManagement>();
            GameObject _ragdoll = Instantiate(ragdollPrefab);
			Instantiate (HUDPrefab);
			_pauseMenu = Instantiate (PauseMenuPrefab);
			Debug.Log ("pausemenu");
			_pauseMenu.SetActive (false);
            ragdoll = _ragdoll.GetComponent<RagdollManagement>();
            move.head = headDirection;
            move.head.transform.parent = null;
        }
        else
            Destroy(this.gameObject);
	}

    bool CheckWallDistance() {
        RaycastHit wallHit = new RaycastHit();
        Ray newRay = new Ray(headDirection.position, headDirection.forward);
        if (Physics.Raycast(newRay, out wallHit, 1, hitMask) && wallHit.distance <= 0.9f)
        {
            return false;
        }
        return true;
    }

	public override void UpdatePlaying() {
		shootTimer += Time.deltaTime;
		bindings = InputManager._instance.bindings;
		if(bindings.pauseGame.WasPressed) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			_pauseMenu.SetActive (true);
			Globals.gameState = GameState.Paused;
		}
		if (playerState == inputState.free) {
			move.MovePlayer(new Vector3(bindings.move.X, 0, bindings.move.Y));

			if (bindings.fire.IsPressed && shootTimer > 0.25 && !weapon.charging) {
                if (CheckWallDistance()) {
                    weapon.PrepareToFire();
                }
			}

			if (bindings.fire.WasReleased && weapon.charging) {
				
                if (CheckWallDistance()) {
                    blinkBall = weapon.Fire();
                    weapon.charging = false;
                }
			}

			if (bindings.blink.IsPressed) {
				if (blinkBall != null) {
					blinkBall.SetPlayer (gameObject);
				}
				//peeking = true;
			}

			if (bindings.blink.WasReleased) {
				if (blinkBall != null) {
					shootTimer = 0;
					//peeking = false;
					weapon.GetComponentInChildren<SkinnedMeshRenderer> ().enabled = true;
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

        if (bindings.respawn.WasPressed) {
            Die();
        }
	}

	public override void UpdatePaused() {
		if (bindings.pauseGame.WasPressed) {
			_pauseMenu.SetActive (false);
			Cursor.lockState = CursorLockMode.Locked;
			Globals.gameState = GameState.Playing;
		}
	}

	public void Die() {
        //Debug.Log("Die");
		if (playerState == inputState.ragdoll) {
			playerState = inputState.free;
			ragdoll.Die ();
		}
		headDirection.gameObject.GetComponent<Camera>().fieldOfView = 179;
		move._movState = PlayerMovement.movementState.standing;
		GetComponent<Rigidbody> ().velocity = Vector3.zero;
		move.grounded = true;
		move.timeSinceGrounded = 0;
		if (blinkBall != null) {
			Destroy (blinkBall.gameObject);
		}
		transform.position = currentRespawnPoint.position;
        currentRespawnPoint.GetComponent<SpawnPoint>().RespawnFX();
	}

}