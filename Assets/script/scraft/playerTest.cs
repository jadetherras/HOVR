using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class playerTest : MonoBehaviour
{
   public Transform controller;

    void Update() {
        Debug.LogWarning(controller.rotation);
    }
}