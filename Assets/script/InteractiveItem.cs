using UnityEngine;

//LENA MODIFICATION

public abstract class InteractiveItem : MonoBehaviour {

	protected ObjectAnchor anchor; 

	public abstract void interacted_with ( MainPlayerController player );

	//public abstract void interacted_with ( HandController handcontroller );

	public bool is_available(){
		return anchor.is_available();
	}

	public abstract void exit ( MainPlayerController player );
}