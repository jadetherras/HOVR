using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    protected AudioSource audio;
    public static float volume = 0.2f;
    
    
    public void SetVolume ( float volume_) {
        audio.volume = volume_;
        volume = volume_;
    }
       
    // Start is called before the first frame update
    void Start()
    {
        // Get the Audio
        audio = GetComponent<AudioSource>();
        audio.volume = volume;
    }
}
