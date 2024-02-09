using System.Collections.Generic;
using UnityEngine;

public class MiniGame {

    protected MiniGameInteractable interactable;

    public MiniGame(MiniGameInteractable interactable) {
        this.interactable = interactable;
    }

    public virtual void Play() {

    }

    protected void EndGame() {
        interactable.EndGame();
    }

    protected void WinGame() {
        Debug.Log("Winning game");
        interactable.EndGame();
    }
}