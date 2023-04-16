using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGodMouv : MonoBehaviour
{
    [Header( "God move parameters" )]
	public float translationVal;
    [Range( 0.01f, 0.1f )]
	public float animationSpeed = 0.05f;
    public bool testTrans = false;
    public bool testRot = false;
    public int testVal;
    

    protected Vector3 current_position;

    void Start () {
        current_position = transform.position;
    }

    protected float t_x = 0;
    protected float t_y = 0;
    protected float t_z = 0;

    protected float r_x = 0;
    protected float r_y = 0;
    protected float r_z = 0;

    protected float i = 0;
    protected float r = 0;

    protected float inv = 1;

    public void translation (int direction) {
        if(i == 0){
            switch(direction) 
            {
              case 0:
                t_x = 1;
                t_z = 0;
                t_y = 0;
                break;
              case 1:
                t_x = -1;
                t_z = 0;
                t_y = 0;
                break;
              case 2:
                t_z = 1;
                t_x = 0;
                t_y = 0;
                break;
              case 3:
                t_z = -1;
                t_x = 0;
                t_y = 0;
                break;
              case 4:
                t_z = 0;
                t_x = 0;
                t_y = 1;
                break;
              case 5:
                t_z = 0;
                t_x = 0;
                t_y = -1;
                break;
              default:
                break;
            }
        }
    }

    public void rotation (int direction) {
        if(r == 0){
            r = 180;
            switch(direction) 
            {
              case 0:
                r_x = 1;
                r_z = 0;
                r_y = 0;
                break;
              case 1:
                r_x = -1;
                r_z = 0;
                r_y = 0;
                break;
              case 2:
                r_z = 1;
                r_x = 0;
                r_y = 0;
                break;
              case 3:
                r_z = -1;
                r_x = 0;
                r_y = 0;
                break;
              case 4:
                r_z = 0;
                r_x = 0;
                r_y = 1;
                break;
              case 5:
                r_z = 0;
                r_x = 0;
                r_y = -1;
                break;
              default:
                break;
            }
        }
    }

    void Update () {
        if(testTrans){
            translation(testVal);
            testTrans = false;
        }
        if(testRot){
            rotation(testVal);
            testRot = false;
        }
        // Handle transition rate
		i += (t_x*t_x + t_y*t_y + t_z * t_z) * animationSpeed * inv;
        r -= 1;

		// Cap values
		if ( i > 1 ){
            i = 0;
            t_x = 0;
            t_y = 0;
            t_z = 0;
            current_position = this.transform.position;
        }

        if(r < 0){
            r = 0;
            r_z = 0;
            r_x = 0;
            r_y = 0;
        }

        if(i < 0){
            i = 0;
            t_x = 0;
            t_y = 0;
            t_z = 0;
            inv = 1;
        }

        this.transform.position = ( 1 - i ) * current_position + i * (current_position - new Vector3( t_x * translationVal, t_y * translationVal, t_z * translationVal ));
        
        this.transform.Rotate(0.5f * r_x, 0.5f * r_y, 0.5f * r_z);
     }

    public void Inverse() {
        inv = -1;
    }
}
