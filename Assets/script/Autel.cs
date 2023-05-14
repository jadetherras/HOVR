using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using LightToggler.cs; //added!

public class Autel : Interactable {
    

    // Events to be triggered when the switch is toggled
    //public event System.Action<bool> OnSwitchToggle;

    //private Toggle toggle; // Reference to the Unity UI Toggle component

    //private LightToggler light;
    //component for light and color change in function of toggle

    [Header("Player cameras")]
    public GameObject playerHuman;

    private AudioSource audiosource;

    // Start is called before the first frame update (ev. use Awake instead)
    void Start()
    {
        if ( GetComponent<AudioSource>() != null){
            audiosource = GetComponent<AudioSource>();
        } else {
            audiosource = null;
        }
    }

/*
    public override void interacted_with ( MainPlayerController handcontroller ){
        Debug.LogWarning("lantern triggered");
        IsActive = true;
        _material.color = new Color(0,1,0); //green
        //add to the switch count
        activeSwitchCount++;
        //ev. TEXT => YOU HAVE x lights on y !! 
        //play the sound_effect
        if (audiosource != null){
            audiosource.Play();
        }
    }
    */


    public override void OnInteract(HandController hand){
        
        if (audiosource != null){
            audiosource.Play();
        }

        
		OVRInput.SetControllerVibration(0.3f, 0.05f, OVRInput.Controller.LTouch);
		OVRInput.SetControllerVibration( 0.3f, 0.05f, OVRInput.Controller.RTouch);

        if(playerHuman.GetComponent<MainPlayerController>().GetB4()){
            playerHuman.GetComponent<MainPlayerController>().SetNearAutel(true);
        }
        
    }

    /*

    // Method to be called when the toggle value changes, when the toggle is selected
    private void OnToggleValueChanged(bool isToggled)
    {
        IsActive = isToggled;

       // if (OnSwitchToggle != null) //ch� pas trop ce que �a fait, �a {   OnSwitchToggle.Invoke(isToggled);  }

        // Check if all five switches are active
        if (isToggled)
        {
            //light.SetLightState(LightState.on);
            //light = on
            _material.EnableKeyword(EMISSION);

            //_material.SetColor(EMISSION_COLOR, _onColor);
            _material.color = new Color(0,1,1); //blue

            //add to the switch count
            activeSwitchCount++;
            //ev. TEXT => YOU HAVE x lights on y !! 
            //play the sound_effect
            if (audiosource != null){
                audiosource.Play();
            }
            

        }
        else
        {
            //RMQ: the light shoud perhaps not be removable => once isToggled has been called, no return possible
            //Auquel cas => delete this part
            //light = off
            _material.DisableKeyword(EMISSION);

            _material.SetColor(EMISSION_COLOR, _offColor);
            //remove from the switch count
            activeSwitchCount--;
        }

        if (activeSwitchCount == 5)
        {
            Debug.Log("You regained some of your powers and can access the next level!");
            // Trigger an event to notify that all switches are active
            //if (OnAllSwitchesActive != null){OnAllSwitchesActive.Invoke();}
        }
    }
*/
    // Event to be triggered when all five switches are active
    //public static event System.Action OnAllSwitchesActive;
/*
    void OnTriggerEnter ( Collider other ) {
        //check that the collider is not a handcontroler
        //if (other.GetComponen<HandController>() != null) return;
        if (!IsActive){
            if (other.CompareTag("Player")){
                _material.EnableKeyword(EMISSION);

                //_material.SetColor(EMISSION_COLOR, _onColor);
                _material.color = new Color(0,0,1); 

                //add to the switch count
                activeSwitchCount++;

                IsActive = true;
            } 
        }

    }
    */
}

