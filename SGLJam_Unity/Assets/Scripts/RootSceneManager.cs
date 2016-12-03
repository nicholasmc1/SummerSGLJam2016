using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootSceneManager : StateBehaviour {
	// Singleton Crap
	private static RootSceneManager _instance;

	private RootSceneManager() {}

	public static RootSceneManager GetInstance() {
		if (_instance != null) {
			return _instance;
		} else {
			_instance = RootSceneManager ();
			return _instance;
		}
	}

	// Actual Class
	private string[] _levels;
	private string _currentLevelScene;
	private const string _transitionRoomScene = "Scenes/TransitionRoom";

	private string _nextLevel;
	private Level _transitionRoom;
	private Level _currentLevel;

	public void SetCurrentLevel(Level level) {
		_currentLevel = level;
	}

	public void SetTransitionRoom(Level level) {
		_transitionRoom = level;
	}

	private IEnumerator RemoveLevel(string level, System.Action<int> callback) {
		yield return SceneManager.UnloadSceneAsync (level);
		callback (0);
	}

	private IEnumerator LoadLevel(string level, System.Action<int> callback) {
		yield return SceneManager.LoadSceneAsync (level, LoadSceneMode.Additive);
		callback (0);
	}

	// Call this once you are at the end of a level
	public void SetupTransitionRoom(System.Action<int> callback) {
		StartCoroutine (LoadLevel(_transitionRoom, () => {
			// Once transition room is created, open the doors
			_currentLevel.nextDoor.SetActive(false);
			_transitionRoom.previousDoor.SetActive(false);
		}));
	}

	// Call this once inside the beginning of a level
	public void CloseTransitionRoom() {
		// Close door to previous level
		_currentLevel.previousDoor.SetActive (true);

		//Unload transition room
		StartCoroutine(RemoveLevel(_transitionRoomScene, () => {}));
	}

	// Call this once from inside the transition level
	public void SetupNextLevel(string nextLevel, string previousLevel) {
		// Close door to previous level;
		_transitionRoom.previousDoor.SetActive(true);

		// Remove previous level
		StartCoroutine(RemoveLevel(previousLevel, () => {
			// Begin loading the next level once previous room is destroyed
			StartCoroutine(LoadLevel(nextLevel, () => {
				// Once next level is loaded, open the door to the next level
				_transitionRoom.nextDoor.SetActive(true);
				_currentLevel.previousDoor.SetActive(false);
				// TODO: Open doors
			}));
		}));
	}
}
