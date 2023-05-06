using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class tutorial : MonoBehaviour {

    public List<GameObject> StepsList;
    public List<Lantern_switch> Pass;
    //protected int i = 0;

    void Start () {
        Debug.LogWarning("here the count");
        Debug.LogWarning(StepsList.Count);
        //pass step 0
        passStep();
    }

    void Update() {
        if (StepsList.Count > 0 && Pass.Count > 0 && Pass[0].IsActive) {
            Debug.LogWarning("goo");
            passStep();
        }
        //if(StepsList.Count > 0 && i == 300) {
        //    Debug.LogWarning("pass activate");
        //    passStep();
        //}
        //i = i+1;

    }

    void passStep() {
        Debug.LogWarning("pass the step");
        StepsList[0].SetActive(true);
        StepsList.RemoveAt(0);
    }

}
