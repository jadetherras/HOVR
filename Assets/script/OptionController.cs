using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    static bool no_vertigo = false;
    static float volume = 0.2f;
    
    Toggle noVertigo ;
    Slider theVolume ;
    
    // Start is called before the first frame update
    void Start()
    {
        noVertigo = GetComponent<Toggle>();
        noVertigo.isOn = no_vertigo;
        theVolume = GetComponent<Slider>();
        theVolume.value = volume;
    }

    // Update is called once per frame
    void Update()
    {
        no_vertigo = noVertigo.isOn;
        volume = theVolume.value;
    }
}
