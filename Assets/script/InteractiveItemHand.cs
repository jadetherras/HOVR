using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveItemHand : MonoBehaviour
{
    protected ObjectAnchor anchor; 

	//public abstract void interacted_with ( MainPlayerController player );

	public abstract void interacted_with ( HandController handcontroller );

	public bool is_available(){
		return anchor.is_available();
	}
}
