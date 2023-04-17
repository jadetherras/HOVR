using UnityEngine;

public class screen : InteractiveItem {
	
	public GameObject plate;
	public bool Isactive = false;
	//public int i = 0;
	void Start () {
			//plate = GameObject.FindGameObjectWithTag("plate");
			plate.SetActive(Isactive);
	}

	//void Update() {
		//if(i == 25) {
			//Isactive = !Isactive;
			//GameObject.FindGameObjectWithTag("plate").SetActive(Isactive);
			//plate.SetActive(Isactive);
			//i = 0;
		//}
		//i = i+1;
	//}

	public override void interacted_with( MainPlayerController player ) {
		plate.SetActive(true);
	}

	public override void exit( MainPlayerController player ) {
		plate.SetActive(false);
	}

}