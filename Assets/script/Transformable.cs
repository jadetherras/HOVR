using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformable : MonoBehaviour
{
    public GameObject shape1;
    public GameObject shape2;

    //public List<GameObject> shapes;

    public int which;
    public float interactive_radius;
    // current = 0;

    // Start is called before the first frame update
    void Start()
    {
        shape1.SetActive(true);
        shape2.SetActive(false);
        
        //shapes[0].SetActive(true);
        //for (i == shapes.count()-1) {
        //    shapes[i].SetActive(false)
        //}
    }

    public void Change(int i){
        if(i == which){
            shape2.transform.position = shape1.transform.position;
            shape1.SetActive(false);
            shape2.SetActive(true);
        }

        //if (current+1 <= shapes.count()) {
         //   shapes[current+1].transform.position = shapes[current].transform.position;
         //   shapes[current+1].SetActive(true);
         //   shapes[current].SetActive(false);
         //   curent = current+1;
        //}
    }

    public float get_interactive_radius(){
        return interactive_radius;
    }
}
