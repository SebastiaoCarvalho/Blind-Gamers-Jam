using System.Collections.Generic;
using System.Threading.Tasks;
using FMODUnity;
using UnityEngine;

public class SequenceMiniGame : MiniGame {
    private List<Sound> solution;
    private Sound sequenceSound;
    private List<Sound> remainingSounds;

    public SequenceMiniGame(MiniGameInteractable interactable) : base(interactable) {
        solution = new List<Sound>();
        string[] sounds; 
        string path = interactable.GetComponent<StudioEventEmitter>().EventReference.ToString();
        if (path[^2] == '2') {
            path = "Sequences/seq_UpDownLeftRight_2";
            sounds = new string[] {"Up", "Down", "Left", "Right"};
        }
        else {
            path = "Sequences/seq_UpDownLeftRight";
            sounds = new string[] {"Down", "Left", "Up", "Right"};
        }
        for (int i = 0; i < sounds.Length; i++) {
            solution.Add(new Sound("Puzzle_" + sounds[i], interactable.gameObject.GetComponent<StudioEventEmitter>()));
        }
        remainingSounds = new List<Sound>(solution);
        sequenceSound = new Sound(path, interactable.gameObject.GetComponent<StudioEventEmitter>());

    }

    public override void Play()
    {
        Debug.Log("Playing sequence minigame");
        PlayAudio(sequenceSound);
    }

    public override bool IsFinished()
    {
        return remainingSounds.Count == 0;
    }

    void PlayAudio(Sound sound)
    {     
        sound.Play();
    }

    public bool TrySound(Sound sound) {
        if (sound == remainingSounds[0]) {
            remainingSounds.RemoveAt(0);
            if (remainingSounds.Count == 0) {
                WinGame();
            }
            return true;
        }
        LoseGame();
        remainingSounds = new List<Sound>(solution);
        return false;
    }
}