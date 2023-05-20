using System;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour {

	[SerializeField]
	protected bool godMode = false;

	protected List<Type> list_of_player_upgrades = new List<Type>();
	
	protected bool nearAutel = false;
	protected bool b_4_released = false;

	public void SetB4(bool state){
		b_4_released = state;
	}

	public bool GetB4(){
		return b_4_released;
	}

	public void acquire_item ( CollectibleItem item ) {
		// Check that the upgrade is not already acquired
		if ( is_equiped_with( item.GetType() ) ) return;

		// Add the upgrade in the list of upgrade collected
		list_of_player_upgrades.Add( item.GetType() );
	}

	public bool is_equiped_with ( Type type ) {
		// Check that one element is the right type
		for ( int i = 0; i < list_of_player_upgrades.Count; i++ ) if ( type == list_of_player_upgrades[i] ) return true;
		return false;
	}

	public bool GetMode(){
		return godMode;
	}

	public void SetMode(bool mode){
		godMode = mode;
	}

	public void SetNearAutel(bool state){
		nearAutel = state;
	}

	public bool IsNearAutel(){
		return nearAutel;
	}


	void OnTriggerEnter ( Collider other ) {


		// Retreive the object to be collected if it exits
		InteractiveItem interactive_item = other.GetComponent<InteractiveItem>();
		if ( interactive_item == null ) return;

		// Forward the current player to the object to be collected
		interactive_item.interacted_with( this );

	}

	void OnTriggerExit ( Collider other ) {

		// Retreive the object to be collected if it exits
		InteractiveItem interactive_item = other.GetComponent<InteractiveItem>();
		if ( interactive_item == null ) return;

		// Forward the current player to the object to be collected
		interactive_item.exit( this );

	}
}