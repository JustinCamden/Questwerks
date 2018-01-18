using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// True if game has ended
	private bool gameOver;

	// True if game is paused
	public bool gamePaused;

	// Use this for initialization
	void Start () {
		gameOver = false;
		gamePaused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (gamePaused || gameOver) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}		
	}

	public void endGame() {
		gameOver = true;
		Time.timeScale = 0;
	}

	public bool isGameOver() {
		return gameOver;
	}

}
