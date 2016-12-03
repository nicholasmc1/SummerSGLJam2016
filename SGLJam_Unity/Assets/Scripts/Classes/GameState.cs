using UnityEngine;
using System.Collections;

public enum GameState {
	Paused,
	Dialogue,
	Menu,
	Playing,
    Stopped
}

public class StateBehaviour : MonoBehaviour {
	public virtual void UpdateAll () {}
	public virtual void UpdatePaused () {}
	public virtual void UpdatePlaying () {}
    public virtual void UpdateStopped() {}

	public virtual void FixedUpdateAll () {}
	public virtual void FixedUpdatePaused () {}
	public virtual void FixedUpdatePlaying () {}
	public virtual void FixedUpdateStopped() {}

	void Update() {
		UpdateAll ();
		switch (Globals.gameState) {
		case GameState.Paused:
			UpdatePaused ();
			break;
		case GameState.Playing:
			UpdatePlaying ();
			break;
        case GameState.Stopped:
            UpdateStopped();
            break;
		}
	}

	void FixedUpdate() {
		FixedUpdateAll ();
		switch (Globals.gameState) {
		case GameState.Paused:
			FixedUpdatePaused ();
			break;
		case GameState.Playing:
			FixedUpdatePlaying ();
			break;
		case GameState.Stopped:
			FixedUpdateStopped();
			break;
		}
	}
}