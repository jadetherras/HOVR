using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
   
   [Header( "Interaction Properties" )]
	public float interactionRadius = 0.9f;

   public float get_interaction_radius () { return interactionRadius; }

   public abstract void OnInteract(HandController hand);


   //need to be redefined for the different objects 
 //  
  // public abstract void OnFocus();
   //public abstract void OnLoseFocus();
}
