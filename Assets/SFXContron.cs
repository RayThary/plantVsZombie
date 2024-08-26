using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXContron : MonoBehaviour
{
    private AudioSource audioSource;
    private bool StopSound = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StopSound)
        {
            audioSource.Stop();
        }
    }
    public void SetStopSound(bool _value)
    {
        StopSound = _value;
    }

}
