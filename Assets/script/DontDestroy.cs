using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    
    void Awake(){
        if(GameObject.FindObjectsOfType<DontDestroy>() != null){
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(transform.gameObject);
        //for canvas, to specify if other objects involved
        //GameObject obj = GetComponent<GameObject>(); 
        //transform.gameObject.SetActive(false);
    }
}
