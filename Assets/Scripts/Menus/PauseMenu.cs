
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : Menu {
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public InputActionReference pauseAction;

    // Update is called once per frame
    void Update() {
        if (pauseAction.action.triggered) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    void Resume() {
        pauseMenuUI.SetActive(false);
        PlayerInput playerInput = GameObject.Find("PlayerInput").GetComponent<PlayerInput>();
        playerInput.SwitchCurrentActionMap("PlayerActions");
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        PlayerInput playerInput = GameObject.Find("PlayerInput").GetComponent<PlayerInput>();
        playerInput.SwitchCurrentActionMap("MainMenu");
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ContinueButton() {
        Resume();
    }

    public void QuitButton() {
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public override void Escape()
    {
        Resume();
    }
}