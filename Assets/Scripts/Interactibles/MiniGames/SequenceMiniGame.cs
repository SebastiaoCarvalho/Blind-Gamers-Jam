using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SequenceMiniGame : MiniGame {
    private List<Sound> solution;
    private List<Sound> remainingSounds;
    private float delay_time;

    public SequenceMiniGame(MiniGameInteractable interactable) : base(interactable) {
        solution = new List<Sound>();
        int sequenceLength = 4;
        string[] sounds = {"A", "B", "C", "D"};
        for (int i = 0; i < sequenceLength; i++) {
            string sound = sounds[Random.Range(0, sounds.Length)];
            solution.Add(new Sound(sound, interactable.gameObject.GetComponent<AudioSource>()));
        }
        remainingSounds = new List<Sound>(solution);

    }

    public override async void Play() // TODO : Later should play each sound in sequence
    {
        delay_time = 0;
        foreach (Sound sound in solution) {
            Debug.Log("Playing sound: " + sound.SoundName);
        }
        foreach (Sound sound in solution) {
            await Task.Delay((int) (delay_time * 1000)); // delay_time is in seconds
            Debug.Log("Playing sound: " + sound.SoundName);
            PlayAudio(sound);
        }
    }

    void PlayAudio(Sound sound)
    {     
        sound.Play();
        delay_time = sound.GetLength() + .5f; //1 second is added to cater for the loading delay          
    }

    public bool TrySound(Sound sound) {
        if (sound == remainingSounds[0]) {
            remainingSounds.RemoveAt(0);
            if (remainingSounds.Count == 0) {
                WinGame();
            }
            return true;
        }
        remainingSounds = new List<Sound>(solution);
        EndGame();
        return false;
    }
}