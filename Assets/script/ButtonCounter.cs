using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonCounter : MonoBehaviour
{
    protected TextMeshPro mtext;
    protected int count = 0;
    protected int maxcount = 10;

    public Lantern_switch lantern;
    //public levelManager manager;

    // Start is called before the first frame update
    void Start()
    {
        //lantern = GetComponent<Lantern_switch>();
        mtext= GetComponent<TextMeshPro>();
        mtext.text="Score:" + "\n" +count.ToString() + "/" +maxcount.ToString();
        count = lantern.GetComponent<Lantern_switch>().givecount();
        //maxcount = lantern.GetComponent<Lantern_switch>().givemaxcount();
    }

    // Update is called once per frame
    void Update()
    {
        mtext.text="Score: " + "\n" +count.ToString() + "/" +maxcount.ToString();
        //count = count+1;
        count = lantern.GetComponent<Lantern_switch>().givecount();
    }
}
