using UnityEngine;
using UnityEngine.EventSystems;

public class SpecialButton : MonoBehaviour, ISelectHandler
{

    [SerializeField] private string textName;

    public void OnSelect(BaseEventData eventData)
    {
        textName = textName != "" ? textName : gameObject.name;
        Debug.Log("Selected " + textName);
        FMODUnity.RuntimeManager.PlayOneShot("event:/TTS/" + textName);
    }
}