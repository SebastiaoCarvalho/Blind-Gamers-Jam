using UnityEngine;
using UnityEngine.InputSystem;

public class MiniGameInteractable : Interactable {

    public MiniGame miniGame;

    protected override void Start() {
        base.Start();
        miniGame = new SequenceMiniGame(this);
    }
    public override void Interact() {
        Debug.Log("Interacting with minigame");
        // activate minigame interaction
        PlayerInput playerInput = GameObject.Find("PlayerInput").GetComponent<PlayerInput>();
        playerInput.SwitchCurrentActionMap("SequenceGameActions"); // FIXME : Each minigame should do this if they have their own input map
        miniGame.Play();
    }

    public void EndGame() {
        PlayerInput playerInput = GameObject.Find("PlayerInput").GetComponent<PlayerInput>();
        playerInput.SwitchCurrentActionMap("PlayerActions");
    }

    public void TrySound(string sound) {
        if (((SequenceMiniGame) miniGame).TrySound(new Sound(sound))) { // FIXME: need to work on a better solution later
            Debug.Log("Correct sound");
        } else {
            Debug.Log("Incorrect sound");
        }
    }
}