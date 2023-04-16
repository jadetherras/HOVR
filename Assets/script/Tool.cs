using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
	public int toolType;

    void OnTriggerEnter(Collider other){
		Transformable ot = other.GetComponent<Transformable>();
		if(ot != null){
			ot.Change(toolType);
		}
	}
}
