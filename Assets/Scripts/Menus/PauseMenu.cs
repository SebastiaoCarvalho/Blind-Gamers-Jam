
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public InputActionReference pauseAction;

    public Button[] buttons;

    private int selectedButton = 0;

    /* void Start() {
        buttons[selectedButton].Select();
    } */

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
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
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
}