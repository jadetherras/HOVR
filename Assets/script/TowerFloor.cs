using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFloor : MonoBehaviour
{
    void OnTriggerEnter ( Collider other) {
        if(other.gameObject.tag == "Perso"){
            other.transform.SetParent(this.transform);
        }
    }
}
