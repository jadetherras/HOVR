using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {
    
    


    public void contain (InteractiveItem collider) {
        collider.transform.SetParent( this.transform );
    }

    public void detach (InteractiveItem collider) {
        collider.transform.SetParent( null );
    }


    void OnTriggerEnter ( Collider other ) {
        //check that the collider is not a handcontroler
        //if (other.GetComponen<HandController>() != null) return;
        //if (other.CompareTag("Player")) return; 
        
		// Retrieve the object to be collected if it exits
		InteractiveItem interactive_item = other.GetComponent<InteractiveItem>();
		if ( interactive_item == null ) return;
         
        if ( !interactive_item.is_available()){
            this.detach(interactive_item);
            return;
        } 

        //RMQ: est-ce que ça pourrait être problématique si le jouer détache l'objet après être entré dans la boîte ? => quand est-ce que ontrigger est activé ?
        //pour pallier à ça => utiliser OnTRiggerStay


        //faire un DETACH quand de setparent quand le item n'est plus available

		// Forward the current player to the object to be collected
		//interactive_item.contain( this );
        this.contain(interactive_item);

	}
    
    //public override void interacted_with ( MainPlayerController player ) { self_toggled_by( player ); }
    // Start is called before the first frame update
    //void Start(){}
    

    // Update is called once per frame
    //void Update(){}
    
}














/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // The list to store objects inside the container
    private List<GameObject> objectsInsideContainer = new List<GameObject>();

    // Reference to the container's transform component
    private Transform containerTransform;

    private void Start()
    {
        // Get the container's transform
        containerTransform = transform;
    }

    // Add an object to the container
    public void AddObjectToContainer(GameObject obj)
    {
        // Set the object's parent to the container's transform
        obj.transform.SetParent(containerTransform);
        // Add the object to the list of objects inside the container
        objectsInsideContainer.Add(obj);
    }

    // Move the container and its contained objects
    public void MoveContainer(Vector3 newPosition)
    {
        // Calculate the position offset from the container's current position to the new position
        Vector3 positionOffset = newPosition - containerTransform.position;
        // Move the container to the new position
        containerTransform.position = newPosition;
        // Move all the contained objects by the same position offset
        foreach (GameObject obj in objectsInsideContainer)
        {
            obj.transform.position += positionOffset;
        }
    }

    // Method to be called when the container is grabbed or moved externally, e.g., by a player controller
    public void OnContainerMoved(Vector3 newPosition)
    {
        MoveContainer(newPosition);
    }
}
*/