using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class tutorial : MonoBehaviour {

    protected List<GameObject> StepsList = new List<GameObject>();
    protected int current = 0;
    protected int time = 0;

    void Start () {
        GameObject[] Steps = GameObject.FindGameObjectsWithTag ("tutorial");
        foreach (GameObject step in Steps) {
            StepsList.Add(step);
        }
   //Sorting list and check it count
   if (StepsList.Count > 0) {
        StepsList.Sort(delegate(GameObject a, GameObject b) {
            return (a.GetComponent<Step>().number).CompareTo(b.GetComponent<Step>().number);
        });
        }
    
    passStep();

    }

    void Update() {
        ++time;
        if (time == 250) {
            time = 0;
            ++current;
            passStep();
        }
    }

    void passStep() {
        while ( StepsList.Count != 0 && StepsList[0].GetComponent<Step>().number == current) {
            StepsList[0].SetActive(true);
            StepsList.RemoveAt(0);
        }
    }

}
