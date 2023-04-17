 using UnityEngine;
using static OVRHand;

public class GodMove : MonoBehaviour {

	[Header( "Hands" )]
	// Bindings with OVR Hands
	public HandController leftHand;
	public HandController rightHand;

	[Header(" Player ")]
	public GameObject player;

	[Header(" marker ")]
	public GameObject marker;
	public GameObject marker2;

	[Header( "Maximum Distance" )]
	[Range( 2f, 30f )]
	// Store the maximum distance the player can teleport
	public float maximumTeleportationDistance = 15f;

	// Store the pointing hand and the non pointing hand
	protected HandController pointing_hand = null;
	protected int moveCommand = 0;
	protected GameObject[] gos;
	protected bool secondHandActivated = false;
	protected GameObject marker_prefab_instanciated = null;
	protected GameObject marker_prefab_instanciated_2 = null;
	protected GameObject closest = null;
	protected Vector3 controllerPos;

	void Start(){
		if(gos == null) gos = GameObject.FindGameObjectsWithTag("MoveItem");
	}

	protected Vector2 previous_b_up = new Vector2(0,0);
	void Update () {
		pointing_hand = rightHand;

		bool b_1 = OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.Touch);
		bool b_2 = OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.Touch);
		Vector2 b_up = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

		// Make sure the pointing hand is still pinched otherwise reset the pointing hand to null
		if (b_up.y < 0.85 && previous_b_up.y < 0.85 && !b_1 && !b_2) {
			pointing_hand = null;
			if ( marker_prefab_instanciated != null ) Destroy( marker_prefab_instanciated );
			if ( marker_prefab_instanciated_2 != null ) Destroy( marker_prefab_instanciated_2 );
			// Remove the cursor
			moveCommand = 0;
			return;
		}

		// Store the position of the targeted point
		Vector3 target_point;
		// Instantiate the marker prefab if it doesn't already exists and place it to the targeted position
		if ( moveCommand == 0 && aim_with( pointing_hand, out target_point )){
			if(player.GetComponent<MainPlayerController>().GetMode() && (b_1 || b_2)){
				
				float distance = 50f;
				Vector3 position = target_point;

				if(closest == null){
					if ( marker_prefab_instanciated_2 == null ) marker_prefab_instanciated_2 = GameObject.Instantiate( marker2, this.transform );
					marker_prefab_instanciated_2.transform.position = position;
					controllerPos = rightHand.transform.position;
					foreach (GameObject go in gos)
					{
						Vector3 diff = go.transform.position - position;
						float curDistance = diff.sqrMagnitude;
						if (curDistance < distance)
						{
							closest = go;
							distance = curDistance;
						}
					}
				}
				
				Vector3 newVect = controllerPos - rightHand.transform.position;

				if(b_1){
					if(newVect.x > 0.5){
						closest.GetComponent<SimpleGodMouv>().translation(0);
						moveCommand = 1;
						closest = null;
						}
					if(newVect.x < -0.5){
						closest.GetComponent<SimpleGodMouv>().translation(1);
						moveCommand = 1;
						closest = null;
						}
					if(newVect.y > 0.5){
						closest.GetComponent<SimpleGodMouv>().translation(4);
						moveCommand = 1;
						closest = null;
						}
					if(newVect.y < -0.5){
						closest.GetComponent<SimpleGodMouv>().translation(5);
						moveCommand = 1;
						closest = null;
						}
					if(newVect.z > 0.5){
						closest.GetComponent<SimpleGodMouv>().translation(2);
						moveCommand = 1;
						closest = null;
						}
					if(newVect.z < -0.5){
						closest.GetComponent<SimpleGodMouv>().translation(3);
						moveCommand = 1;
						closest = null;
						}
					}
					else{
					if(b_2){
						if(newVect.x > 0.5){
							float roll = closest.transform.position.z/90;
							switch(roll){
								case 0 :
									closest.GetComponent<SimpleGodMouv>().rotation(4);
									break;
								case 1 :
									closest.GetComponent<SimpleGodMouv>().rotation(1);
									break;
								case 2 :
									closest.GetComponent<SimpleGodMouv>().rotation(5);
									break;
								case 3 :
									closest.GetComponent<SimpleGodMouv>().rotation(0);
									break;
								default :
									break;
							}
							moveCommand = 1;
							closest = null;
							}
						if(newVect.x < -0.5){
							float roll = closest.transform.position.z/90;
							switch(roll){
								case 0 :
									closest.GetComponent<SimpleGodMouv>().rotation(5);
									break;
								case 1 :
									closest.GetComponent<SimpleGodMouv>().rotation(0);
									break;
								case 2 :
									closest.GetComponent<SimpleGodMouv>().rotation(4);
									break;
								case 3 :
									closest.GetComponent<SimpleGodMouv>().rotation(1);
									break;
								default :
									break;
							}
							moveCommand = 1;
							closest = null;
							}
						if(newVect.y > 0.5){
							float roll = closest.transform.position.z/90;
							switch(roll){
								case 0 :
									closest.GetComponent<SimpleGodMouv>().rotation(1);
									break;
								case 1 :
									closest.GetComponent<SimpleGodMouv>().rotation(5);
									break;
								case 2 :
									closest.GetComponent<SimpleGodMouv>().rotation(0);
									break;
								case 3 :
									closest.GetComponent<SimpleGodMouv>().rotation(4);
									break;
								default :
									break;
							}
							moveCommand = 1;
							closest = null;
							}
						if(newVect.y < -0.5){
							float roll = closest.transform.position.z/90;
							switch(roll){
								case 0 :
									closest.GetComponent<SimpleGodMouv>().rotation(0);
									break;
								case 1 :
									closest.GetComponent<SimpleGodMouv>().rotation(4);
									break;
								case 2 :
									closest.GetComponent<SimpleGodMouv>().rotation(1);
									break;
								case 3 :
									closest.GetComponent<SimpleGodMouv>().rotation(5);
									break;
								default :
									break;
							}
							moveCommand = 1;
							closest = null;
							}
						if(newVect.z > 0.5){
							closest.GetComponent<SimpleGodMouv>().rotation(2);
							moveCommand = 1;
							closest = null;
							}
						if(newVect.z < -0.5){
							closest.GetComponent<SimpleGodMouv>().rotation(3);
							moveCommand = 1;
							closest = null;
							}
						}
					}
				
			}else{
				if(b_up.y >= 0.85 && !player.GetComponent<MainPlayerController>().GetMode()){
					if ( marker_prefab_instanciated == null ) marker_prefab_instanciated = GameObject.Instantiate( marker, this.transform );
					// Place the marker to the targeted position
					marker_prefab_instanciated.transform.position = target_point;
					previous_b_up = b_up;
					this.GetComponent<CurvedLigne>().Setpoint1(rightHand.transform.position);
					this.GetComponent<CurvedLigne>().Setpoint3(target_point);
					this.GetComponent<CurvedLigne>().Draw();
				}else{
					if(b_up.y < 0.85 && previous_b_up.y >= 0.85 && !player.GetComponent<MainPlayerController>().GetMode()){
						if ( marker_prefab_instanciated != null ) Destroy( marker_prefab_instanciated );
						marker_prefab_instanciated = null;
						player.GetComponent<CharacterController>().Move(target_point - player.transform.position);
						previous_b_up = b_up;
						this.GetComponent<CurvedLigne>().Clean();
					}
				}
			}
		}else{
			if ( marker_prefab_instanciated != null ) Destroy( marker_prefab_instanciated );
			if ( marker_prefab_instanciated_2 != null ) Destroy( marker_prefab_instanciated_2 );
			marker_prefab_instanciated = null;
			marker_prefab_instanciated_2 = null;
			this.GetComponent<CurvedLigne>().Clean();
		}
	}

	protected bool aim_with ( HandController a_hand, out Vector3 target_point ) {

		// Default the "output" target point to the null vector
		target_point = new Vector3();

		// Launch the ray cast and leave if it doesn't hit anything
		RaycastHit hit;
		if ( !Physics.Raycast( a_hand.transform.position, a_hand.transform.forward, out hit, Mathf.Infinity ) ) return false;

		if(!player.GetComponent<MainPlayerController>().GetMode()){
			// If the aimed point is out of range (i.e. the raycast distance is above the maximum distance) then prevent the teleportation
			if ( hit.distance > maximumTeleportationDistance ) return false;
		}

		// "Output" the target point
		target_point = hit.point;
		return true;
	}

	void OnDestroy(){
		gos = null;
	} 
}
