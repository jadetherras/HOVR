using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class tutorial : MonoBehaviour {

    public List<GameObject> StepsList;
    public List<Lantern_switch> Pass;
    //public GameObject wall;
    //private AudioSource Success;
    //public GameObject light;
    //protected int i = 0;

    void Start () {

        //if ( GetComponent<AudioSource>() != null){
        //    Success = GetComponent<AudioSource>();
        //} else {
        //    Success = null;
        //}

        foreach (var step in StepsList)  {
            step.SetActive(false);
        }
    }

    void Update() {
        if (StepsList.Count > 0 && Pass.Count > 0 && Pass[0].IsActive) {
            Debug.LogWarning("goo");
            passStep();
        }

        //if (Pass.Count == 0) {

          //  if (wall != null){
          //      wall.SetActive(false);
          //  }

          //  if (light != null){
          //      light.SetActive(true);
          //  }

           // if (Success != null){
           //     Success.Play();
           // }
        }

    

    void passStep() {
        Pass.RemoveAt(0);
        StepsList[0].SetActive(true);
        StepsList.RemoveAt(0);
    }

}

