using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour {
    
    
    public void contain (InteractiveItem collider) {
        GameObject glue = new GameObject("Glue");
        glue.transform.SetParent(collider.transform);
        transform.SetParent(glue.transform);
        collider.transform.SetParent( this.transform );
    }

    //a priori inutile, parce qu'il ne faut pas non plus l'enlever de la main ! A SUPPRIMER
    public void detach (InteractiveItem collider) {
        collider.transform.SetParent( null );
    }


    void OnTriggerStay ( Collider other ) {
       

		// Retrieve the object to be collected if it exits
		InteractiveItem interactive_item = other.GetComponent<InteractiveItem>();
		if ( interactive_item == null ) return;
         
        if ( !interactive_item.is_available()){
            //this.detach(interactive_item);
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







