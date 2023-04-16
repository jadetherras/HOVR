using UnityEngine;

public class SimpleScenarioController : Controller {

	[Header( "Contolled Items" )]
	public Fence fence;

	[Header( "Inputs" )]
	public FloorSwitch floorSwitch;

	void Start () {
		floorSwitch.on_toggled( 
			( switch_state ) => { 
				Debug.LogWarning("activated");
				if ( switch_state ) {
					fence.Fix();
					fence.SetUpdate(true);
					fence.open();
				}else{
					fence.close(); 
				} 
			}
		);
	}

}