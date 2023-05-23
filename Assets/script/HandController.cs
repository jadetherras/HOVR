using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class HandController : MonoBehaviour {

	// Store the hand type to know which button should be pressed
	public enum HandType : int { LeftHand, RightHand };
	[Header( "Hand Properties" )]
	public HandType handType;


	// Store the player controller to forward it to the object
	[Header( "Player Controller" )]
	public MainPlayerController playerController;
  
	[Header( "Marker" )]
	public GameObject marker;

	[Header( "Perso" )]
	public GameObject perso;

	// Store all gameobjects containing an Anchor
	// N.B. This list is static as it is the same list for all hands controller
	// thus there is no need to duplicate it for each instance
	static protected ObjectAnchor[] anchors_in_the_scene;
	static protected List<Transformable> transformable_in_the_scene;
	//LENA
	static protected Interactable[] interactables_in_the_scene;

	protected Vector3 lastPosition;
	protected Vector3 Velocity;
	protected Canvas menu;

	void Start () {
		//find menu canvas
		Canvas menu = FindObjectOfType<Canvas>();
		// Prevent multiple fetch
		perso.SetActive(true);
		if ( anchors_in_the_scene == null ) anchors_in_the_scene = GameObject.FindObjectsOfType<ObjectAnchor>();
		perso.SetActive(false);
		if ( transformable_in_the_scene == null ) transformable_in_the_scene = GameObject.FindObjectsOfType<Transformable>().ToList();
		//LENA
		if ( interactables_in_the_scene == null ) interactables_in_the_scene = GameObject.FindObjectsOfType<Interactable>();
		lastPosition = this.transform.position;
		Velocity = Vector3.zero;
	}



	// This method checks that the hand is closed depending on the hand side
	protected bool is_hand_closed () {
		// Case of a left hand
		if ( handType == HandType.LeftHand ) return
			//OVRInput.Get( OVRInput.Button.Three )                           // Check that the A button is pressed
			//&& OVRInput.Get( OVRInput.Button.Four )                         // Check that the B button is pressed
			OVRInput.Get( OVRInput.Axis1D.PrimaryHandTrigger ) > 0.5     // Check that the middle finger is pressing
			&& OVRInput.Get( OVRInput.Axis1D.PrimaryIndexTrigger ) > 0.5;   // Check that the index finger is pressing


		// Case of a right hand
		else return
			//OVRInput.Get( OVRInput.Button.One )                             // Check that the A button is pressed
			//&& OVRInput.Get( OVRInput.Button.Two )                          // Check that the B button is pressed
			OVRInput.Get( OVRInput.Axis1D.SecondaryHandTrigger ) > 0.5   // Check that the middle finger is pressing
			&& OVRInput.Get( OVRInput.Axis1D.SecondaryIndexTrigger ) > 0.5; // Check that the index finger is pressing
	}

	protected bool is_interacting(){
		return OVRInput.Get(OVRInput.Button.Four);
	}

	public Vector3 velocity () {return Velocity;}
	public Vector3 position () {return lastPosition;}

	// Automatically called at each frame
	void Update () { 
		handle_controller_behavior(); 
		Velocity = (this.transform.position - lastPosition)/Time.deltaTime;
		lastPosition = this.transform.position;
		//LENA


	/*
		handle_interaction_check();
		handle_interaction_input(); 
		*/
	}

/*
	private Raycast hit;
	handle_interaction_check(){
		if(Physics.Raycast(this.transform.position,Vector3.forward, out hit, interactionDistance, interactionLayer)){

		}
	}
	
	
	handle_interaction_input(){
		if(OVRInput.Button.Three && currentInteractable != null && Physics.Raycast(this.transform.position, Vector3.forward, out hit, interactionDistance, interactionLayer)){
			currentInteractable.OnInteract();
		}
	}
*/


	// Store the previous state of triggers to detect edges
	protected bool is_hand_closed_previous_frame = false;

	// Store the object atached to this hand
	// N.B. This can be extended by using a list to attach several objects at the same time
	protected ObjectAnchor object_grasped = null;
	protected GameObject marker_prefab_instanciated = null;


	//LENA
	protected Interactable activated_obj = null;

	/// <summary>
	/// This method handles the linking of object anchors to this hand controller
	/// </summary>
	protected void handle_controller_behavior () {
		
		//==============================================//
		//Make the menu appear or disappear//
		//==============================================//
        if(OVRInput.Get( OVRInput.Button.Start)){
			SceneManager.LoadScene("menu");
            //menu.transform.gameObject.SetActive(!transform.gameObject.activeSelf);
			//ev. need to make it appear in front of the eye
			Debug.LogWarning("Menu should be on");
        }
		
		//==============================================//
		// Define the behavior when the hands wants to interact //
		//==============================================//
		if (is_interacting()){
			// Determine which object available is the closest from the left hand
			int best_object_id = -1;
			float best_object_distance = float.MaxValue;
			float oject_distance;

			// Iterate over objects to determine if we can interact with it
			for ( int i = 0; i < interactables_in_the_scene.Length; i++ ) {

				

				// Compute the distance to the object
				oject_distance = Vector3.Distance( this.transform.position, interactables_in_the_scene[i].transform.position );

				// Keep in memory the closest object
				// N.B. We can extend this selection using priorities
				if ( oject_distance < best_object_distance && oject_distance <= interactables_in_the_scene[i].get_interaction_radius() ) {
					best_object_id = i;
					best_object_distance = oject_distance;
				}
			}

			// If the best object is in range grab it
			if ( best_object_id != -1 ) {

				// Store in memory the object grasped
				activated_obj = interactables_in_the_scene[best_object_id];

				
				// Grab this object
				activated_obj.OnInteract(this);

				//desactivate not implemented yet because not needed yet
			}

		}

		if(playerController.GetMode()){
			//Debug.LogWarning(OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger));
			//if(OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < 0.5){
				int best_object_id = -1;
				float best_object_distance = float.MaxValue;
				float oject_distance;

				// Iterate over objects to determine if we can interact with it
				for ( int i = 0; i < transformable_in_the_scene.Count; i++ ) {

					// Compute the distance to the object
					oject_distance = Vector3.Distance( this.transform.position, transformable_in_the_scene[i].transform.position );

					// Keep in memory the closest object
					// N.B. We can extend this selection using priorities
					if ( oject_distance < best_object_distance && oject_distance <= transformable_in_the_scene[i].get_interactive_radius() ) {
						best_object_id = i;
						best_object_distance = oject_distance;
					}
				}

				// If the best object is in range grab it
				if ( best_object_id != -1 ) {
					transformable_in_the_scene[best_object_id].Change(0);
					transformable_in_the_scene.RemoveAt(best_object_id);
				}
			//}
		}

		// Check if there is a change in the grasping state (i.e. an edge) otherwise do nothing
		bool hand_closed = is_hand_closed();

		Vector3 target = new Vector3(0,0,0);

		bool b_3_1 = (OVRInput.Get(OVRInput.Button.Three, OVRInput.Controller.Touch) && is_left_hand() )|| ((OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.Touch) && !is_left_hand()));
		if(!b_3_1 || playerController.GetMenuMode()){
			if ( marker_prefab_instanciated != null ) Destroy( marker_prefab_instanciated );
			marker_prefab_instanciated = null;
		}else{
			RaycastHit hit;
			if ( !Physics.Raycast( this.transform.position, this.transform.forward, out hit, Mathf.Infinity ) ) return;
			if ( marker_prefab_instanciated == null ) marker_prefab_instanciated = GameObject.Instantiate( marker, this.transform );
			marker_prefab_instanciated.transform.position = hit.point;
			target = hit.point;
		}

		if ( hand_closed == is_hand_closed_previous_frame ) return;
		is_hand_closed_previous_frame = hand_closed;

		//==============================================//
		// Define the behavior when the hand get closed //
		//==============================================//
		if ( hand_closed ) {

			// Log hand action detection
			//Debug.LogWarningFormat( "{0} get closed", this.transform.parent.name );

			// Determine which object available is the closest from the left hand
			int best_object_id = -1;
			float best_object_distance = float.MaxValue;
			float oject_distance;

			// Iterate over objects to determine if we can interact with it
			for ( int i = 0; i < anchors_in_the_scene.Length; i++ ) {

				// Skip object not available
				if ( !anchors_in_the_scene[i].is_available() ) continue;

				// Skip object requiring special upgrades
				if ( !anchors_in_the_scene[i].can_be_grasped_by( playerController ) ) continue;

				if(b_3_1 && !playerController.GetMenuMode()){
					oject_distance = Vector3.Distance( target, anchors_in_the_scene[i].transform.position);
					Debug.LogWarning(oject_distance);
				}else{
					// Compute the distance to the object
					oject_distance = Vector3.Distance( this.transform.position, anchors_in_the_scene[i].transform.position );
				}
				// Keep in memory the closest object
				// N.B. We can extend this selection using priorities
				if ( oject_distance < best_object_distance && oject_distance <= anchors_in_the_scene[i].get_grasping_radius() ) {
					best_object_id = i;
					best_object_distance = oject_distance;
				}
			}

			// If the best object is in range grab it
			if ( best_object_id != -1 ) {

				// Store in memory the object grasped
				object_grasped = anchors_in_the_scene[best_object_id];

				// Log the grasp
				//Debug.LogWarningFormat( "{0} grasped {1}", this.transform.parent.name, object_grasped.name );

				if(!b_3_1){
					// Grab this object
					object_grasped.attach_to( this, playerController.GetMode() );
				}else{
					object_grasped.long_attach_to(this, playerController.GetMode());
				}
			}



		//==============================================//
		// Define the behavior when the hand get opened //
		//==============================================//
		} else if ( object_grasped != null ) {
			// Log the release
			//Debug.LogWarningFormat("{0} released {1}", this.transform.parent.name, object_grasped.name );

			// Release the object
			object_grasped.detach_from( this );
		}


		
		
	}


	//LENA
	//to have also an interaction with the hands, and not only with the mainplayer
	void OnTriggerEnter ( Collider other ) {

		// Retreive the object to be collected if it exits
		InteractiveItemHand interactive_item = other.GetComponent<InteractiveItemHand>();
		if ( interactive_item == null ) return;

		// Forward the current player to the object to be collected
		interactive_item.interacted_with( this );
	}

	void OnDestroy(){
      anchors_in_the_scene = null;
	  transformable_in_the_scene = null;
	  interactables_in_the_scene = null;
	}

	public bool is_left_hand(){
		if (handType == HandType.LeftHand){
			return true;
		} else {
			return false;
		}
	}
}