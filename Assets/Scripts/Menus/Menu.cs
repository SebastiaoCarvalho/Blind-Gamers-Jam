using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Button[] buttons;
    protected int selectedButton = 0;

    public InputActionReference upAction, downAction, enterAction, escapeAction;

    void Start()
    {
        buttons[selectedButton].Select();
        upAction.action.performed += _ => Up();
        downAction.action.performed += _ => Down();
        enterAction.action.performed += _ => Select();
        escapeAction.action.performed += _ => Escape();
    }

    public void Up() {
        buttons[selectedButton].OnDeselect(null);
        selectedButton = (selectedButton -1 + buttons.Length) % buttons.Length;
        buttons[selectedButton].Select();
    }

    public void Down() {
        buttons[selectedButton].OnDeselect(null);
        selectedButton = (selectedButton + 1) % buttons.Length;
        buttons[selectedButton].Select();
    }

    public void Select() {
        buttons[selectedButton].onClick.Invoke();
    }

    public virtual void Escape() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}