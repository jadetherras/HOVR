using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSaver : MonoBehaviour
{
    protected Vector3 PlayerPos;
    protected List<Vector3> ObjectPos = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Give Player position
    public Vector3 GetPlayerPos()
    {
        return PlayerPos;
    }

     public void SetPlayerPos(Vector3 pos)
    {
        PlayerPos = pos;
    }

    public Vector3 GetObjectPos(int elem)
    {
        return ObjectPos[elem];
    }

     public void SetObjectPos(Vector3 pos)
    {
        ObjectPos.Add(pos);
    }

    public void ClearObjectPos()
    {
        ObjectPos.Clear();
    }
}
