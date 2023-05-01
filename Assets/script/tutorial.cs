using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class tutorial : MonoBehaviour {

    public List<GameObject> StepsList = new List<GameObject>();

    void Start () {
        Debug.LogWarning("here the count");
        Debug.LogWarning(StepsList.Count);
        //pass step 0
        passStep();
    }

    void Update() {

        if (StepsList.Count > 0 && StepsList[0].GetComponent<Step>().active()) {
            Debug.LogWarning("goo");
            passStep();
        }
    }

    void passStep() {
        Debug.LogWarning("pass the step");
        StepsList[0].SetActive(true);
        StepsList.RemoveAt(0);
    }

}
