using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button[] buttons;
    private int selectedButton = 0;
    // Start is called before the first frame update
    void Start()
    {
        buttons[selectedButton].Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void OptionsButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OptionsMenu");
    }

    public void QuitButton() {
        Application.Quit();
    }

    public void Up() {
        selectedButton = (selectedButton + buttons.Length) % buttons.Length;
        buttons[selectedButton].Select();
    }
}
