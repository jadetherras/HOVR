using UnityEngine;
using UnityEngine.Video;

public class screen : InteractiveItem {
	
	public GameObject plate;
	//public VideoPlayer V;
	public GameObject video;
	//public int i = 0;
	void Start () {
			//plate = GameObject.FindGameObjectWithTag("plate");
			plate.SetActive(false);

			if (video != null) {
				video.SetActive(false);
				video.GetComponent<VideoPlayer>().Stop();
			}
			//V = video.GetComponent<VideoPlayer>();
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

		if (video != null) {
			video.SetActive(true);
			//V.Play();
			video.GetComponent<VideoPlayer>().Play();
			Debug.LogWarning("play");
		}
		
	}

	public override void exit( MainPlayerController player ) {
		plate.SetActive(false);

		if (video != null) {
			video.GetComponent<VideoPlayer>().Stop();
			Debug.LogWarning("stop");
			video.SetActive(false);
			//V.Stop();
		}
		
	}

}