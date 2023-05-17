using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    protected AudioSource audio;
    protected bool mute = false;
    

    public void SetVolume ( float volume_) {
        audio.volume = volume_;
    }

    protected void set_mute(bool mute_){
        mute = mute_;
    
        

        // Mute or unmute the audio by setting the volume to 0 or 1
        audio.volume = mute_ ? 0f : 1f;
        if (mute_)
        {
            audio.volume = 0f; // Mute the audio
        }
        else
        {
            audio.volume = 1f; // Unmute the audio
        }
         
        
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the Audio
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
