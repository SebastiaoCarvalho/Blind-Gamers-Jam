using System.Collections.Generic;
using UnityEngine;

public class SequenceMiniGame : MiniGame {
    private List<Sound> solution;

    public SequenceMiniGame(MiniGameInteractable interactable) : base(interactable) {
        solution = new List<Sound>();
        int sequenceLength = 4;
        string[] sounds = {"A", "B", "C", "D"};
        for (int i = 0; i < sequenceLength; i++) {
            string sound = sounds[Random.Range(0, sounds.Length)];
            solution.Add(new Sound(sound));
        }

    }

    public override void Play() // TODO : Later should play each sound in sequence
    {
        foreach (Sound sound in solution) {
            sound.Play();
        }
    }

    public bool TrySound(Sound sound) {
        if (sound == solution[0]) {
            solution.RemoveAt(0);
            if (solution.Count == 0) {
                WinGame();
            }
            return true;
        }
        EndGame();
        return false;
    }
}