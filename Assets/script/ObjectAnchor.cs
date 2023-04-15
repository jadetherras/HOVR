using System;
using UnityEngine;

public class ObjectAnchor : MonoBehaviour {

	[Header( "Grasping Properties" )]
	public float graspingRadius = 0.7f;

	// Store initial transform parent
	protected Transform initial_transform_parent;
	protected Rigidbody rb;
	protected Vector3 lastPosition;
	protected Vector3 Velocity;
	void Start () { initial_transform_parent = transform.parent; 
					rb = GetComponent<Rigidbody>();
					lastPosition = rb.transform.position;
					Velocity = Vector3.zero;
					}

	
	// Store the hand controller this object will be attached to
	protected HandController hand_controller = null;

	void update () { 
		Velocity = (rb.transform.position - lastPosition)/Time.deltaTime;
		lastPosition = rb.transform.position;
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


	public virtual void attach_to ( HandController hand_controller ) {
		// Store the hand controller in memory
		this.hand_controller = hand_controller;

		// Set the object to be placed in the hand controller referential
		transform.SetParent( hand_controller.transform );
		rb.isKinematic = true;
		rb.useGravity = false;
	}

	public virtual void detach_from ( HandController hand_controller ) {
		// Make sure that the right hand controller ask for the release
		if ( this.hand_controller != hand_controller ) return;

		// Detach the hand controller
		this.hand_controller = null;

		// Set the object to be placed in the original transform parent
		transform.SetParent( initial_transform_parent );
		
		// change to gravity mode and give a velocity
		rb.isKinematic = false;
		rb.useGravity = true;

		//Vector3 velocity = OVRInput.GetLocalControllerVelocity();
		rb.velocity = hand_controller.velocity();
	}

	public Vector3 velocity () {return Velocity;}

	public virtual bool is_available () { return hand_controller == null; }

	public virtual float get_grasping_radius () { return graspingRadius; }

	public virtual bool can_be_grasped_by ( MainPlayerController player ) { return true; } //player.is_equiped_with( typeof( BasicGraspUpgrade ) );
}