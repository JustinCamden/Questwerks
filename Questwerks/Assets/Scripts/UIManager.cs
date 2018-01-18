using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class UIManager : MonoBehaviour {

	// Reference to GameManager instance
	private GameManager gameManager;

	// Reference to Main Menu object
	public GameObject mainMenu;

	// References to Text objects in menu
	public Text pauseText;
	public Text winText;
	
	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		// Check for game end state
		if (gameManager.isGameOver()) {
			mainMenu.SetActive(true);
			pauseText.enabled = false;
			winText.enabled = true;

			return; // Don't accept further input!
		}

		// Key to open / close pause menu
		if (Input.GetKeyDown(KeyCode.Escape)) {
			gameManager.gamePaused = !gameManager.gamePaused;
			mainMenu.SetActive(!mainMenu.activeInHierarchy);
			pauseText.enabled = true;
			winText.enabled = false;
		}
	}

	public void QuitGame() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	public void RestartGame() {
		EditorSceneManager.LoadScene(0);
	}
}
