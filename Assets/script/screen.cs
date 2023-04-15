using UnityEngine;

public class screen : InteractiveItem {
	
	//void Start () {
	//		GameObject.FindGameObjectWithTag("plate").SetActive(false);
	//	}

	public override void interacted_with ( MainPlayerController player ) {
		GameObject.FindGameObjectWithTag("plate").SetActive(true);
	}

	public override void exit ( MainPlayerController player ) {
		GameObject.FindGameObjectWithTag("plate").SetActive(false);
	}

}
