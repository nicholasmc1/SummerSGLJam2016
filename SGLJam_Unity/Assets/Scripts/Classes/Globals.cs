using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void Command(string[] args);

public class Globals {
	private static GameState _gameState = GameState.Playing;

	public static GameState gameState {
		get {
			return _gameState;
		}
		set {
			if (value == GameState.Paused) {
				Time.timeScale = 0;
				_gameState = value;
			} else {
				Time.timeScale = 1;
				_gameState = value;
			}
		}
	}
}