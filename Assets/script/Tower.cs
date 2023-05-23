using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Tower : MonoBehaviour
{
    public MainPlayerController player;
    public List<GameObject> tower;
    public List<GameObject> cristals;
    public List<Lantern_switch> lanterns;
    public int size;
    public List<int> towerOrder;
    public List<float> towerAngle;
    public List<float> floor2Angle;
    public GameObject DeactivateOnLant1;
    public GameObject DeactivateOnAllLants;

    private List<Vector3> posFloor = new List<Vector3>();
    private List<Vector3> rotFloor = new List<Vector3>();
    private List<int> floorOrder = new List<int>(); //give a floor and get it's order
    private List<int> orderFloor = new List<int>(); //give an order and get it's floor
    private List<Vector3> PosLantern = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tower.Count; i++){
            floorOrder.Add(i);
            orderFloor.Add(i);
            PosLantern.Add(new Vector3(lanterns[i].transform.position.x, lanterns[i].transform.position.y - 1.6f, lanterns[i].transform.position.z));
            posFloor.Add(tower[i].transform.position);
            rotFloor.Add(tower[i].transform.rotation.eulerAngles);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(lanterns[0].IsActive){
            DeactivateOnLant1.SetActiveRecursively(false);
            foreach( Transform child in tower[0].transform.Find("FloorColor").transform){
                child.gameObject.GetComponent<Renderer>().material.color = new Color(1,0,0);
            }
        }
        
        if(lanterns[1].IsActive){
            foreach( Transform child in tower[1].transform.Find("FloorColor").transform){
                child.gameObject.GetComponent<Renderer>().material.color = new Color(0,1,0);
            }
        }
        if(lanterns[2].IsActive){
            foreach( Transform child in tower[2].transform.Find("FloorColor").transform){
                child.gameObject.GetComponent<Renderer>().material.color = new Color(0,0,1);
            }
        }

        for (int i = 0; i < tower.Count; i++){ 
            float currentPos = tower[i].transform.position.y;
            int order = floorOrder[i];
            float setPos = posFloor[order].y;

            Vector3 newPos = posFloor[order];
            Vector3 newRot = rotFloor[order];
            newRot.y = tower[i].transform.rotation.eulerAngles.y; // force the new rotation to be the same one as the one created at the start of the game except on the y axis.
            newPos.y = currentPos; // force the new position to be the same one as the one created at the start of the game except on the y axis.

            if(currentPos < (setPos - size)){
                //if the floor is already the lowest one, set a lower position beyond which he can't go.
                if(order == 0){
                    newPos.y = setPos - size;
                }else{
                    // else change the order of the floor and made the floors take their new position
                    int floorToSwitch = orderFloor[order - 1];

                    //update the value of the floorOrder list
                    floorOrder[floorToSwitch] = order;
                    floorOrder[i] = order - 1;

                    //update the value of the orderFloor list and update the position of the floors
                    orderFloor[order] = floorToSwitch;
                    orderFloor[order - 1] = i;
                    SetNearest(false);

                    //reset the position of the current floor to it's new pos (due to the change in order).
                    newPos = posFloor[order - 1];
                    newRot = rotFloor[order - 1];
                    newRot.y = tower[i].transform.rotation.eulerAngles.y;
                    newPos.y = currentPos;
                }
            }else{
                //if the floor is already the highest one, set a higher position beyond which he can't go.
                if(currentPos > (setPos + size)){
                    if(order == (tower.Count - 1)){
                        newPos.y = setPos + size;
                    }else{
                        // else change the order of the floor and made the floors take their new position
                        int floorToSwitch = orderFloor[order + 1];

                        //update the value of the floorOrder list
                        floorOrder[floorToSwitch] = order;
                        floorOrder[i] = order + 1;

                        //update the value of the orderFloor list and update the position of the floors
                        orderFloor[order] = floorToSwitch;
                        orderFloor[order + 1] = i;
                        SetNearest(false);

                        //reset the position of the current floor to it's new pos (due to the change in order).
                        newPos = posFloor[order + 1];
                        newRot = rotFloor[order + 1];
                        newRot.y = tower[i].transform.rotation.eulerAngles.y;
                        newPos.y = currentPos;
                    }
                }
            }

            Quaternion currentRotation = Quaternion.identity;
            currentRotation.eulerAngles = newRot;
            //update the position of the current floor.
            tower[i].transform.position = newPos;
            tower[i].transform.rotation = currentRotation;
        }
    }

    //reset all floor to the original position linked to their position in the order.
    public void SetNearest(bool fromDetach){
        bool allColor = true;
        bool rightOrder = true;
        bool rightAngle = true;

        for (int i = 0; i < tower.Count; i++){
            int order = floorOrder[i];
            tower[i].transform.position = posFloor[order];
            Quaternion currentRotation = Quaternion.identity;
            currentRotation.eulerAngles = new Vector3(0,tower[i].transform.rotation.eulerAngles.y,0);
            tower[i].transform.rotation = currentRotation;

            allColor = allColor && lanterns[i].IsActive;
            rightOrder = rightOrder && (order == towerOrder[i]);
            rightAngle = rightAngle && (Math.Abs((tower[i].transform.rotation.eulerAngles.y % 360) - towerAngle[i]) < 0.1f) && floorOrder[i] == i;

        }

        if(fromDetach){
            bool rightFloor2Angle = true;
            for(int i = 0; i < floor2Angle.Count; i++){
                bool correct = (floorOrder[2] == i && Math.Abs((tower[2].transform.rotation.eulerAngles.y % 360) - floor2Angle[i]) < 0.1f) || (cristals[i].GetComponent<Renderer>().material.color == new Color(0,1,0));
                rightFloor2Angle = rightFloor2Angle && correct;
                if(correct){
                    cristals[i].GetComponent<Renderer>().material.color = new Color(0,1,0);
                }
            }
            
            if(rightAngle){
                lanterns[1].transform.position = PosLantern[1];
                foreach( Transform child in tower[1].transform.Find("FloorColor").transform){
                    child.gameObject.GetComponent<Renderer>().material.color = new Color(1,0,0);
                }
            }

            if(rightFloor2Angle){
                lanterns[2].transform.position = PosLantern[2];
                foreach( Transform child in tower[2].transform.Find("FloorColor").transform){
                    child.gameObject.GetComponent<Renderer>().material.color = new Color(1,0,0);
                }
            }

            if(allColor && rightOrder){
                DeactivateOnAllLants.SetActiveRecursively(false);
                foreach( Transform child in tower[0].transform.Find("FloorColor").transform){
                    child.gameObject.GetComponent<Renderer>().material.color = new Color(0,1,0);
                }
                foreach( Transform child in tower[1].transform.Find("FloorColor").transform){
                    child.gameObject.GetComponent<Renderer>().material.color = new Color(0,1,0);
                }
                foreach( Transform child in tower[2].transform.Find("FloorColor").transform){
                    child.gameObject.GetComponent<Renderer>().material.color = new Color(0,1,0);
                }
            }
        }
    }
}
