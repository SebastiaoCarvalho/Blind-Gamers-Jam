using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    public void PlayButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void OptionsButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OptionsMenu");
    }

    public void QuitButton() {
        Application.Quit();
    }

    public override void Escape() {
        // Do nothing
    }
}
