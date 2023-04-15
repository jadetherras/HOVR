using UnityEngine;
using System;


public class CollisionGameObjectExample : MonoBehaviour
{

    //Detect collisions between the GameObjects with Colliders attached
    void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning( "COLISION");
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject.name == "object")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            Debug.Log("Do something here");
        }

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "MyGameObjectTag")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
        }
    }
}
