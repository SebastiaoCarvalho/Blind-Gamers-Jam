using System.Collections;
using UnityEngine;

public class Sound {
    private string sound;
    public string SoundName { get { return sound; } }
    private AudioSource source;

    public Sound(string sound, AudioSource source) {
        this.sound = sound;
        this.source = source;
        
    }

    public void Play() {
        source.clip = Resources.Load<AudioClip>("Sounds/" + sound);
        source.Play();
    }

    public float PlayDelayed(float delay) {
        source.PlayDelayed(delay);
        return source.clip.length;
    }

    public float GetLength() {
        return source.clip.length;
    }

    public static bool operator ==(Sound s1, Sound s2) {
        return s1.sound == s2.sound;
    }

    public static bool operator !=(Sound s1, Sound s2) {
        return s1.sound != s2.sound;
    }
}