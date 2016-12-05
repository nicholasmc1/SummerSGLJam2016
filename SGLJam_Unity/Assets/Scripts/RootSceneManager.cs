using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootSceneManager : MonoBehaviour {
	// Singleton Crap
	public static RootSceneManager Instance;

	private RootSceneManager() {}

	void Awake()
	{
		Instance = this;
		LoadLevel ("TransitionTunnel");
		Instantiate (playerPrefab, new Vector3(0f, 0f, 2f), Quaternion.identity);
		_levels = new string[]{"Level1", "Level2", "Level3", "Level4", "Level5", "Level6", "Level7"};
		//init transition room
	}
	/*public static RootSceneManager GetInstance() {
		if (_instance != null) {
			return _instance;
		} else {
			_instance = new RootSceneManager ();
			Debug.Log ("Singleton Called!");
			return _instance;
		}
	}*/

	// Actual Class
	public GameObject playerPrefab;

	private string[] _levels;
	private int _currentLevelScene;
	private const string _transitionRoomScene = "TransitionTunnel";

	private string _nextLevel;
	private Level _transitionRoom;
	private Level _currentLevel;


	public void SetCurrentLevel(Level level) 
	{
		_currentLevel = level;
		Debug.Log (_currentLevel);
	}

	public void SetTransitionRoom(Level level) {
		_transitionRoom = level;
	}

	private void RemoveLevel(string level) {
		SceneManager.UnloadSceneAsync (level);
	}

	private void LoadLevel(string level) {
		SceneManager.LoadScene (level, LoadSceneMode.Additive);
	}

	// Call this once you are at the end of a level
	public void SetupTransitionRoom() {
        //LoadLevel(_transitionRoomScene);
		_transitionRoom.gameObject.SetActive(true);
        _transitionRoom.transform.position = _currentLevel.nextDoor.transform.position - _transitionRoom.previousDoor.transform.localPosition;
		// Once transition room is active, open the doors
		_currentLevel.nextDoor.SetActive(false);
		_transitionRoom.previousDoor.SetActive(false);
        _transitionRoom.nextDoor.SetActive(true);
        _transitionRoom.GetComponentInChildren<NextRoom>().hit = false;
		AudioManager._instance.doorSource.Play ();
	}

	// Call this once inside the beginning of a level
	public void CloseTransitionRoom() {
		// Close door to previous level
		_currentLevel.previousDoor.SetActive (true);
        //RemoveLevel(_transitionRoomScene);
		_transitionRoom.gameObject.SetActive (false);
		AudioManager._instance.doorSource.Play ();
	}

	// Call this once from inside the transition level
	public void SetupNextLevel() {
		// Close door to previous level;
		_transitionRoom.previousDoor.SetActive(true);
		// Open door to next
		_transitionRoom.nextDoor.SetActive(false);
		// Remove previous level
		if (_currentLevelScene > 0) {
			RemoveLevel (_levels [_currentLevelScene - 1]);
		}
		// Begin loading the next level once previous room is destroyed
		LoadLevel(_levels[_currentLevelScene]);
		_currentLevelScene++;
		// Once next level is loaded, open the door to the next level
		StartCoroutine(MovingNewLevel());
		AudioManager._instance.doorSource.Play ();
	}
	IEnumerator MovingNewLevel()
	{
		yield return new WaitForEndOfFrame ();
		yield return new WaitForEndOfFrame ();
		_currentLevel.SetTransform (_transitionRoom);
		Debug.Log (_currentLevel);

	}
}
