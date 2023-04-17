using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedLigne : MonoBehaviour
{
    public Vector3 Point1;
    public Vector3 Point2;
    public Vector3 Point3;
    public LineRenderer linerenderer;
    public float vertexCount = 12;
    public float Point2Ypositio = 104;

    protected bool drawing = false;

    public void Draw(){
        drawing = true;
        linerenderer.enabled = true;
    }

    public void Clean(){
        drawing = false;
        linerenderer.enabled = false;
    }

    public void Setpoint1(Vector3 pos){
        Point1 = pos;
    }

    public void Setpoint3(Vector3 pos){
        Point3 = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if(drawing){
            Point2 = new Vector3((Point1.x + Point3.x)/2, ((Point1.y + Point3.y)/2 + 2), (Point1.z + Point3.z) / 2);
            var pointList = new List<Vector3>();

            for(float ratio = 0;ratio<=1;ratio+= 1/vertexCount)
            {
                var tangent1 = Vector3.Lerp(Point1, Point2, ratio);
                var tangent2 = Vector3.Lerp(Point2, Point3, ratio);
                var curve = Vector3.Lerp(tangent1, tangent2, ratio);

                pointList.Add(curve);
            }

            linerenderer.positionCount = pointList.Count;
            linerenderer.SetPositions(pointList.ToArray());
        }
    }
}