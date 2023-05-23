using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class levelManager : MonoBehaviour {

    public GameObject wall;
    private AudioSource Success;
    public GameObject light;
    public int maxSwitchCount = 5;
    public Lantern_switch lantern;

    void Start () {
        
        if (GetComponent<AudioSource>() != null){
            Success = GetComponent<AudioSource>();
        } else {
            Success = null;
        }

        if (wall != null){
            wall.SetActive(true);
        }

        if (lantern != null){
            lantern.restart();
        }

        
    }

    void Update() {
        if (lantern.givecount() == maxSwitchCount) {

            if (wall != null){
                wall.SetActive(false);
            }

            if (light != null){
                light.SetActive(true);
            }

            if (Success != null){
                Success.Play();
            }
        }

    }


}