using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFloor : MonoBehaviour
{
    void OnTriggerStay ( Collider other) {
        if(other.gameObject.GetComponent<ObjectAnchor>() != null && other.GetComponent<ObjectAnchor>().is_available() && other.gameObject.tag != "MoveItem" && other.gameObject.tag != "TowerFloor"){
            other.transform.SetParent(this.transform);
        }

    }

    void OnTriggerExit ( Collider other) {
        if(other.gameObject.GetComponent<ObjectAnchor>() != null && other.gameObject.tag != "MoveItem" && other.gameObject.tag != "TowerFloor"){
            other.transform.SetParent(null);
        }

    }
}
