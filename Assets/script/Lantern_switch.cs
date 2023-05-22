using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using LightToggler.cs; //added!

public class Lantern_switch : Interactable {
    
    
    
    public bool IsActive = false; // Indicates whether the switch is currently active or not { get; private set; }

    public static int activeSwitchCount = 0; // Static counter to keep track of the number of active switches

    //public static int maxSwitchCount = 5; // Static counter to keep track of the number of active switches

    private const string EMISSION_COLOR = "_EmissionColor";
    private const string EMISSION = "_EMISSION";


    private Material _material;

    
    private AudioSource audiosource;

    public int givecount () {
        return activeSwitchCount;
    }

    public void restart () {
        activeSwitchCount = 0;
    }

    //public int givemaxcount () {
    //    return maxSwitchCount;
    //}
    // Start is called before the first frame update (ev. use Awake instead)
    void Start()
    {
        if ( GetComponent<AudioSource>() != null){
            audiosource = GetComponent<AudioSource>();
        } else {
            audiosource = null;
        }
       
         _material = GetComponent<Renderer>().material;
        _material.color = new Color(1,0,0);
    }



    public override void OnInteract(HandController hand){
        if(!IsActive){
            Debug.LogWarning("lantern triggered");
            IsActive = true;
            _material.color = new Color(0,1,0); //green
            activeSwitchCount++;
            //ev. TEXT => YOU HAVE x lights on y !! 
            //play the sound_effect
            if (audiosource != null){
                audiosource.Play();
            }

            if(hand.is_left_hand()){
			    OVRInput.SetControllerVibration(0.3f, 0.05f, OVRInput.Controller.LTouch);
            } else {
			    OVRInput.SetControllerVibration( 0.3f, 0.05f, OVRInput.Controller.RTouch);
            }

            //if (activeSwitchCount == maxSwitchCount){
            //Debug.Log("You regained some of your powers and can access the next level!");

            // Trigger an event to notify that all switches are active
             //material.color = new Color(1,1,0);
            }
        }
    }

