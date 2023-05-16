using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
	public int toolType;
	public bool movespecial = false;

    void OnTriggerEnter(Collider other){
		Transformable ot = other.GetComponent<Transformable>();

		if(ot != null){
			ot.Change(toolType);
		}

		if ( other.tag == "movespecial" && movespecial) {
			Rigidbody rb = other.GetComponent<Rigidbody>();
			if(rb != null){
				rb.isKinematic = false;
			}
		}

	}

	void OnTriggerExit(Collider other){
		if ( other.tag == "movespecial" && movespecial) {
			Rigidbody rb = other.GetComponent<Rigidbody>();
			if(rb != null){
				rb.isKinematic = true;
			}
		}
	}
}
