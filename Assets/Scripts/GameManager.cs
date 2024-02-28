using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    GameObject puzz1;
    GameObject puzz2;
    GameObject end;
    GameObject now;
    // Start is called before the first frame update
    void Start()
    {
        puzz1 = GameObject.Find("MiniGame");
        puzz2 = GameObject.Find("MiniGame2");
        end = GameObject.Find("EndGame");
        puzz1.GetComponents<StudioEventEmitter>()[1].Play();
        now = puzz1;
    }

    // Update is called once per frame
    void Update()
    {
        MiniGameInteractable interactable = puzz1.GetComponent<MiniGameInteractable>();
        if (interactable.IsFinished() && now == puzz1) {
            puzz1.GetComponents<StudioEventEmitter>()[1].Stop();
            puzz2.GetComponents<StudioEventEmitter>()[1].Play();
            now = puzz2;
            return;
        }
        interactable = puzz2.GetComponent<MiniGameInteractable>();
        if (interactable.IsFinished() && now == puzz2) {
            puzz2.GetComponents<StudioEventEmitter>()[1].Stop();
            end.GetComponents<StudioEventEmitter>()[1].Play();
            now = end;
            return;
        }
    }
}
