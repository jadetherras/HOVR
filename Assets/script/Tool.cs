using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
	public int toolType;
	public bool movespecial = false;
	public GameObject light = null;

	void Start () {
		if (light != null) {
			light.SetActive(false);
		}
	 }

    void OnTriggerEnter(Collider other){
		Transformable ot = other.GetComponent<Transformable>();

		if(ot != null){
			ot.Change(toolType);
		}

	}

	void OnCollissionEnter(Collider other){
		if (other.tag == "movespecial" && movespecial) {
			Rigidbody rb = other.GetComponent<Rigidbody>();
			if(rb != null){
				rb.isKinematic = false;
			}

			if (light != null) {
				light.SetActive(true);
			}
		}
	}

	void OnCollissionExit(Collider other){
		if (other.tag == "movespecial" && movespecial) {
			Rigidbody rb = other.GetComponent<Rigidbody>();
			if(rb != null){
				rb.isKinematic = true;
			}

			if (light != null) {
				light.SetActive(true);
			}
		}
	}
}
