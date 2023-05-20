using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    
    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
        //for canvas, to specify if other objects involved
        //GameObject obj = GetComponent<GameObject>(); 
        //transform.gameObject.SetActive(false);
    }
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {


    }
}
