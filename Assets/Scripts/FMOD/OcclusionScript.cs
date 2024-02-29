using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OcclusionScript : MonoBehaviour
{
    private FMODUnity.StudioEventEmitter m_Source;

    public Transform Listener;
    public float OccludedMaxDistance = 0.0f;
    public float OccludedMinDistance = 0.0f;
    public float FadeSpeed = 10.0f;
    private float MaxTarget, MinTarget;
    private float CurrentMaxDist, CurrentMinDist;

    void Start()
    {
        m_Source = gameObject.GetComponent<FMODUnity.StudioEventEmitter>();
    }
    void Update()
    {
        //Retrieving the property values to use in the Mathf function 
        ERRCHECK(m_Source.EventInstance.getProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, out CurrentMinDist), "Failed to retrieve min distance with result:");
        ERRCHECK(m_Source.EventInstance.getProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, out CurrentMaxDist), "Failed to retrieve max distance with result:");

        // Using hitinfo insead of a layer mask. 
        if (Physics.Linecast(transform.position, Listener.position, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.tag == "Listener")
            {
                // We can still use the these varaiables to set our max distances
                MaxTarget = m_Source.OverrideMaxDistance;
                MinTarget = m_Source.OverrideMinDistance;
            }
            else
            {
                MaxTarget = OccludedMaxDistance;
                MinTarget = OccludedMinDistance;
            }
        }
        else
        {
            MaxTarget = OccludedMaxDistance;
            MinTarget = OccludedMinDistance;
        }

        ERRCHECK(m_Source.EventInstance.setProperty(FMOD.Studio.EVENT_PROPERTY.MAXIMUM_DISTANCE, Mathf.MoveTowards(CurrentMaxDist, MaxTarget, Time.deltaTime * FadeSpeed)), "Failed to set max distance with result:");
        ERRCHECK(m_Source.EventInstance.setProperty(FMOD.Studio.EVENT_PROPERTY.MINIMUM_DISTANCE, Mathf.MoveTowards(CurrentMinDist, MinTarget, Time.deltaTime * FadeSpeed)), "Failed to set min distance with result:");

    }


    /// <summary>
    /// Error checking FMOD function calls and printing their results in Editor only 
    /// </summary>
    private void ERRCHECK(FMOD.RESULT result, string failMsg)
    {
#if UNITY_EDITOR
        if (result != FMOD.RESULT.OK)
            Debug.Log(failMsg + " " + result);
#endif
    }
}