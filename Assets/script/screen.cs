using UnityEngine;
using UnityEngine.Video;

public class screen : InteractiveItem {
	
	public GameObject plate;
	public VideoPlayer V;
	public GameObject video;
	public bool Isactive = false;
	//public int i = 0;
	void Start () {
			//plate = GameObject.FindGameObjectWithTag("plate");
			plate.SetActive(Isactive);
			video.SetActive(Isactive);
			V = video.GetComponent<VideoPlayer>();
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
		video.SetActive(true);
		V.GetComponent<VideoPlayer>().Play();
	}

	public override void exit( MainPlayerController player ) {
		plate.SetActive(false);
		video.SetActive(false);
		V.GetComponent<VideoPlayer>().Stop();
	}

}