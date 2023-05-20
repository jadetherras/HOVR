using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFloor : MonoBehaviour
{
    void OnTriggerStay ( Collider other) {
        if(this.GetComponent<ObjectAnchor>().is_available() && other.gameObject.tag != "MoveItem" && other.gameObject.tag != "TowerFloor" && other.gameObject.GetComponent<ObjectAnchor>() != null){
            other.transform.SetParent(this.transform);
            other.transform.localScale = new Vector3(1/other.transform.localScale.x ,1/other.transform.localScale.y ,1/other.transform.localScale.z);
        }

    }

    void OnTriggerExit ( Collider other) {
        if(this.GetComponent<ObjectAnchor>().is_available() && other.gameObject.tag != "MoveItem" && other.gameObject.tag != "TowerFloor" && other.gameObject.GetComponent<ObjectAnchor>() != null){
            other.transform.SetParent(null);
            other.transform.localScale = new Vector3(1,1,1);
        }

    }
}
