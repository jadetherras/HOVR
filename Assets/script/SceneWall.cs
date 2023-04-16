using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWall : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
    {
        try{
            other.GetComponent<SimpleGodMouv>().Inverse();
        }catch{}
    }
}
