using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RootSceneManager : StateBehaviour {
	private static RootSceneManager _instance;

	private string[] _levels;
	private int _currentLevel;
	private const string _transitionRoom = "Scenes/TransitionRoom";

	private RootSceneManager() {}

	public static RootSceneManager GetInstance() {
		if (_instance != null) {

		} else {
			_instance = RootSceneManager ();
		}
	}

	private IEnumerator RemoveLevel(string level, System.Action<int> callback) {
		yield return SceneManager.UnloadSceneAsync (level);
		callback (0);
	}

	private IEnumerator LoadLevel(string level, System.Action<int> callback) {
		yield return SceneManager.LoadSceneAsync (level, LoadSceneMode.Additive);
		callback (0);
	}

	private IEnumerator SetupNextLevel(string nextLevel, string previousLevel) {
		// TODO: Close doors
		StartCoroutine(LoadLevel(nextLevel, levelLoaded => {
			StartCoroutine(RemoveLevel(previousLevel, levelRemoved => {
				// TODO: Open doors
			}));
		}));
	}
}
