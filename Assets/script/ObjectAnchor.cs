using System;
using UnityEngine;

public class ObjectAnchor : MonoBehaviour {

	[Header( "Grasping Properties" )]
	public double graspingRadius = 0;

	[Header( "Tool Properties" )]
	public Vector3 paddingRot;

	[Header( "Tower Manager" )]
	public GameObject towerManag;

	//avoid distortion of object when inside container
	private Vector3 originalScale;

	// Store initial transform parent
	protected Transform initial_transform_parent;
	protected Rigidbody rb;
	protected bool moving = false;
	protected float i = 0;
	protected Vector3 current_position;
	protected Vector3 lastPosition;
	protected Vector3 Velocity;
  
	void Start () {
		/*
		if(graspingRadius == 0){
			graspingRadius = Math.Sqrt(this.GetComponent<Collider>().bounds.size.x * this.GetComponent<Collider>().bounds.size.x + this.GetComponent<Collider>().bounds.size.y * this.GetComponent<Collider>().bounds.size.y + this.GetComponent<Collider>().bounds.size.z * this.GetComponent<Collider>().bounds.size.z) + 0.2;
		}
		*/

		initial_transform_parent = transform.parent; 
		rb = GetComponent<Rigidbody>();
		//joint = GetComponent<FixedJoint>();
		//glue = GameObject("Glue");
		originalScale = transform.localScale;
		//restore original scaling
		lastPosition = rb.transform.position;
		Velocity = Vector3.zero;
	}
	
	// Store the hand controller this object will be attached to
	protected HandController hand_controller = null;

	
	public virtual void attach_to ( HandController hand_controller , bool god) {
		if((this.gameObject.tag == "MoveItem" || this.gameObject.tag == "TowerFloor") && !god){
			return;
		}

		// Store the hand controller in memory
		this.hand_controller = hand_controller;

		if(this.gameObject.tag == "Tool"){
			this.transform.eulerAngles = new Vector3(paddingRot.x + hand_controller.transform.eulerAngles.x, paddingRot.y + hand_controller.transform.eulerAngles.y , paddingRot.z + hand_controller.transform.eulerAngles.z);
			this.transform.position = hand_controller.transform.position;
		}else{
			if(this.gameObject.tag == "TowerFloor"){
				this.gameObject.SetActiveRecursively(false);
				this.gameObject.SetActive(true);
				this.gameObject.GetComponent<Collider>().enabled = false;
			}
		}

		// Set the object to be placed in the hand controller referential
		transform.SetParent( hand_controller.transform );
		rb.isKinematic = true;
		rb.useGravity = false;
		is_glued = false;
		Destroy(glue);

		if(hand_controller.is_left_hand()){
			OVRInput.SetControllerVibration(0.05f, 0.05f, OVRInput.Controller.LTouch);
        } else {
			OVRInput.SetControllerVibration( 0.05f, 0.05f, OVRInput.Controller.RTouch);
        }

		//for case of containeroverriding the parenting of a container
		//glued = false; 
	}

	public void Update () {
		Velocity = (rb.transform.position - lastPosition)/Time.deltaTime;
		lastPosition = rb.transform.position;

		if(moving){
			i += 0.05f;

			if(i > 1){
				i = 0;
				moving = false;
				transform.SetParent( hand_controller.transform );
				rb.isKinematic = true;
				rb.useGravity = false;
				is_glued = false;
				Destroy(glue);
				return;
			}

			this.transform.position = ( 1 - i ) * current_position + i * this.hand_controller.transform.position;
		}
	}

	void OnCollissionEnter (Collision collision) { 
		Debug.LogWarning( "COLISION");

		if (!collision.gameObject.GetComponent<ObjectAnchor>()) {
			Debug.LogWarning( "no anchor");
			return;
		}

		if (this.hand_controller == null && collision.gameObject.GetComponent<ObjectAnchor>().is_available()) {
			Debug.LogWarning( "go 2");
			rb.velocity = Velocity/2 + collision.gameObject.GetComponent<ObjectAnchor>().velocity()/2;
		} else if (this.hand_controller == null && !collision.gameObject.GetComponent<ObjectAnchor>().is_available()) {
			Debug.LogWarning( "go 1");
			rb.velocity = Velocity + collision.gameObject.GetComponent<ObjectAnchor>().velocity();
		}
	}

	public virtual void long_attach_to ( HandController hand_controller , bool god) {
		if((this.gameObject.tag == "MoveItem" || this.gameObject.tag == "TowerFloor") && !god){
			return;
		}

		if(this.gameObject.tag == "Tool"){
			this.transform.eulerAngles = new Vector3(paddingRot.x + hand_controller.transform.eulerAngles.x, paddingRot.y + hand_controller.transform.eulerAngles.y , paddingRot.z + hand_controller.transform.eulerAngles.z);
		}
		// Store the hand controller in memory
		this.hand_controller = hand_controller;

		current_position = this.transform.position;

		moving = true;

		if(hand_controller.is_left_hand()){
			OVRInput.SetControllerVibration(0.05f, 0.05f, OVRInput.Controller.LTouch);
        } else {
			OVRInput.SetControllerVibration( 0.05f, 0.05f, OVRInput.Controller.RTouch);
        }
	}

	public virtual void detach_from ( HandController hand_controller ) {
		// Make sure that the right hand controller ask for the release
		if ( this.hand_controller != hand_controller ) return;

		// Detach the hand controller
		this.hand_controller = null;

		// Set the object to be placed in the original transform parent
		transform.SetParent( initial_transform_parent );
		rb.isKinematic = false;
		if(this.gameObject.tag == "MoveItem"){
			float x = Mathf.Round(this.transform.position.x);
			float y = Mathf.Round(this.transform.position.y);
			float z = Mathf.Round(this.transform.position.z);

			float rx = Mathf.Round(this.transform.eulerAngles.x/45f);
			float ry = Mathf.Round(this.transform.eulerAngles.y/45f);
			float rz = Mathf.Round(this.transform.eulerAngles.z/45f);

			this.transform.position = new Vector3(x, y, z);
			this.transform.eulerAngles = new Vector3(45 * rx, 45 * ry, 45 * rz);
		}else{
			if(this.gameObject.tag == "TowerFloor"){
				towerManag.GetComponent<Tower>().SetNearest(true);
				this.gameObject.GetComponent<Collider>().enabled = true;
				this.gameObject.SetActiveRecursively(true);
			}else{
				rb.useGravity = true;
				rb.velocity = hand_controller.velocity();
			}
		}
		i = 0;
		moving = false;
	}

	public virtual bool is_available () { return hand_controller == null; }

	public virtual double get_grasping_radius () { return graspingRadius; }

	public Vector3 velocity () {return Velocity;}

	public virtual bool can_be_grasped_by ( MainPlayerController player ) { return true; } //player.is_equiped_with( typeof( BasicGraspUpgrade ) );

	//LENA MODIF

	private bool is_contained = false;


/*
	void OnTriggerEnter ( Collider other) {
		if (other.gameObject.tag == "Container"){
			is_contained = true;
			
		}
	}
	*/
/*
//Le problÃ¨me, c'est que exit est continuellement appelÃ©e, vu que l'objet s'arrÃªte Ã  la frontiÃ¨re du trigger
	*/

	//to create the object glue only one time,
	//protected bool glued = false;

	//ou mettre un truc dans un Update ? Je ne sais pas oÃ¹ sont appelÃ©e les autres fonctions, mais je crois que c'est mieux
	//avec un if (is_contained){ ...}

	protected GameObject glue; // = GameObject("Glue")	
	protected bool is_glued = false;
	void OnTriggerStay ( Collider other ) {
		if (other.gameObject.tag == "Container"){
			//Debug.Log(glued);
			if (this.is_available() && !is_glued){
				is_contained = true;
				is_glued = true;
				
				//create an intermediary non-scaled object to avoid distortion of the child	
				glue = new GameObject("Glue");
				//glue.gameObject.transform.position = this.transform.position;
				//glue.gameObject.transform.rotation = this.transform.rotation;
				//set object as indirect child of container 		
				glue.gameObject.transform.SetParent(other.gameObject.transform);
				this.transform.SetParent(glue.gameObject.transform);
				//test


				rb.useGravity = false;
				rb.isKinematic = true;
				//transform.localScale = originalScale;
			}
			/*
			if (this.is_available() && is_glued){
				this.transform.position = glue.gameObject.transform.position;
				this.transform.rotation = glue.gameObject.transform.rotation;
			}*/
			// if hand takes it => will change parent, an not be available anymore


			//ATTENTION Ã  bien s'occuper des nulls dans le exit, et remarquer qu'il est constamment en exit aussi ! 
		}
	}	
}
