using System.Collections;
using System.Threading.Tasks;
using FMODUnity;
using UnityEngine;

public class Sound {
    private string sound;
    public string SoundName { get { return sound; } }
    private StudioEventEmitter source;
    private string eventName;

    public Sound(string sound, StudioEventEmitter source) {
        this.sound = sound;
        this.source = source;
        this.eventName = "event:/UI/" + sound;
        
    }

    public void Play() {
        source.EventInstance.release();
        source.EventReference = FMODUnity.RuntimeManager.PathToEventReference(eventName);
        source.Play();
    }

    public float PlayDelayed(float delay) {
        source.Play();
        return 1 + delay;
    }

    public float GetLength() {
        /* source.EventDescription.getLength(out int length); */
        return 1;
    }

    public static bool operator ==(Sound s1, Sound s2) {
        return s1.sound == s2.sound;
    }

    public static bool operator !=(Sound s1, Sound s2) {
        return s1.sound != s2.sound;
    }
}