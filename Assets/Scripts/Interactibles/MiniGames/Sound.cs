using UnityEngine;

public class Sound {
    private string sound;

    public Sound(string sound) {
        this.sound = sound;
    }

    public void Play() {
        Debug.Log("Playing sound " + sound);
    }

    public static bool operator ==(Sound s1, Sound s2) {
        return s1.sound == s2.sound;
    }

    public static bool operator !=(Sound s1, Sound s2) {
        return s1.sound != s2.sound;
    }
}