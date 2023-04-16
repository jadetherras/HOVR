using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformable : MonoBehaviour
{
    public GameObject shape1;
    public GameObject shape2;

    public int which;
    public float interactive_radius;

    // Start is called before the first frame update
    void Start()
    {
        shape1.SetActive(true);
        shape2.SetActive(false);
    }

    public void Change(int i){
        if(i == which){
            shape2.transform.position = shape1.transform.position;
            shape1.SetActive(false);
            shape2.SetActive(true);
        }
    }

    public float get_interactive_radius(){
        return interactive_radius;
    }
}
